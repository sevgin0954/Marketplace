using Marketplace.Domain.Sales.SellerAggregate;
using Marketplace.Domain.Sales.SellerAggregate.Dtos;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System;
using System.Linq;
using Xunit;

namespace Marketplace.Tests.Sales.SellerSpecs
{
	public class AcceptOfferSpecs
	{
		[Fact]
		public void Accept_offer_should_raise_an_event()
		{
			// Arrange
			var seller = new Seller();

			var productDto = this.CreateProductDto();
			seller.PublishProductForSale(productDto);

			var offer = this.CreateOffer(seller, productDto.Id, 1);
			seller.ReceiveOffer(offer);

			// Act
			seller.AcceptOffer(offer);

			// Assert
			Assert.Contains(seller.DomainEvents, e => e.GetType() == typeof(OfferAcceptedEvent));
		}

		[Fact]
		public void Accept_offer_should_substract_the_offered_quantity_from_the_availible_quantity()
		{
			// Arrange
			var seller = new Seller();

			var availibleQuantity = 2;
			var productDto = this.PublishProductForSale(seller, availibleQuantity);

			var offeredQuantity = 1;
			var offer = this.CreateOffer(seller, productDto.Id, offeredQuantity);

			// Act
			seller.AcceptOffer(offer);

			// Assert
			Assert.Equal(1, seller.ProductsForSale[0].Quantity);
		}

		[Fact]
		public void Accept_offer_for_quantity_equal_to_the_product_availible_quantity_should_move_the_product_to_sold_out_products_collection()
		{
			// Arrange
			var seller = new Seller();

			var availibleQuantity = 1;
			var productDto = this.PublishProductForSale(seller, availibleQuantity);

			var offeredQuantity = 1;
			var offer = this.CreateOffer(seller, productDto.Id, offeredQuantity);

			// Act
			seller.AcceptOffer(offer);

			// Assert
			Assert.Empty(seller.ProductsForSale);
			Assert.Contains(seller.SoldOutProduct, p => p.Id == productDto.Id);
		}

		[Fact]
		public void Accept_offer_for_quantity_equal_to_the_product_availible_quantity_should_change_the_product_status_to_sold_out()
		{
			// Arrange
			var seller = new Seller();

			var availibleQuantity = 1;
			var productDto = this.PublishProductForSale(seller, availibleQuantity);

			var offeredQuantity = 1;
			var offer = this.CreateOffer(seller, productDto.Id, offeredQuantity);

			// Act
			seller.AcceptOffer(offer);

			// Assert
			var product = seller.SoldOutProduct[0];
			Assert.Equal(ProductStatus.Sold, product.Status);
		}

		[Fact]
		public void Accept_offer_for_quantity_less_than_the_product_availible_quantity_should_not_move_the_product_to_sold_out_products_collection()
		{
			// Arrange
			var seller = new Seller();

			var availibleQuantity = 2;
			var productDto = this.PublishProductForSale(seller, availibleQuantity);

			var offeredQuantity = 1;
			var offer = this.CreateOffer(seller, productDto.Id, offeredQuantity);

			// Act
			seller.AcceptOffer(offer);

			// Assert
			Assert.Empty(seller.SoldOutProduct);
			Assert.Contains(seller.ProductsForSale, p => p.Id == productDto.Id);
		}

		[Fact]
		public void Accept_offer_from_banned_user_should_throw_an_exception()
		{
			// Arrange
			var seller = new Seller();

			var productDto = this.PublishProductForSale(seller);
			var offer = this.CreateOffer(seller, productDto.Id);

			var buyer = new TestableBuyer("123");
			seller.BanBuyerFromOffering(buyer.Id);

			// Act

			// Assert
			Assert.Throws<InvalidOperationException>(() => seller.AcceptOffer(offer));
		}

		[Fact]
		public void Accept_offer_for_non_existent_product_should_throw_an_exception()
		{
			// Arrange
			var seller = new Seller();

			var fakeProductId = "123";
			var offer = new Offer("234", fakeProductId, 1);

			// Act

			// Assert
			Assert.Throws<InvalidOperationException>(() => seller.AcceptOffer(offer));
		}

		[Fact]
		public void Accept_offer_for_larger_quantity_than_availible_should_throw_an_exception()
		{
			// Arrange
			var seller = new Seller();

			var availibleQuantity = 2;
			var productDto = this.PublishProductForSale(seller, availibleQuantity);

			var offeredQuantity = 1;
			var offer = this.CreateOffer(seller, productDto.Id, offeredQuantity);

			seller.AcceptOffer(offer);

			// Act

			// Assert
			Assert.Throws<InvalidOperationException>(() => seller.AcceptOffer(offer));
		}

		private ProductDto PublishProductForSale(Seller seller, int quantity = 1)
		{
			var productDto = this.CreateProductDto(quantity);
			seller.PublishProductForSale(productDto);

			return productDto;
		}

		private Offer CreateOffer(Seller seller, string productId, int quantity = 1)
		{
			var offer = new Offer("123", productId, quantity);
			seller.ReceiveOffer(offer);

			return offer;
		}

		private ProductDto CreateProductDto(int quantity = 1)
		{
			var productDto = new ProductDto();
			productDto.Id = "123";
			productDto.Quantity = quantity;

			return productDto;
		}
	}
}
