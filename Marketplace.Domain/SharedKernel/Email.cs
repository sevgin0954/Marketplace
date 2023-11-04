using System;
using System.Text.RegularExpressions;

namespace Marketplace.Domain.SharedKernel
{
	public record Email
	{
		public Email(string emailAddress)
		{
			this.Initialize(emailAddress);
		}

		public string Name { get; private set; }

		public string DomainName { get; private set; }

		private void Initialize(string emailAddress)
		{
			var nameRegexGroup = "nameGroup";
			var domainRegexGroup = "domainGroup";
			var regexPattern =
				$"^(?<{nameRegexGroup}>[\\w-\\.\\d]{{2,}})" + 
				"@" + 
				$"(?<{domainRegexGroup}>\\w+\\.\\w{{2,}}$)";
			var regex = new Regex(regexPattern, RegexOptions.Singleline);

			var isMatch = regex.IsMatch(emailAddress);
			if (isMatch == false)
				throw new ArgumentException("Invalid email!");

			var nameGroupNumber = regex.GroupNumberFromName(nameRegexGroup);
			var domainGroupNumber = regex.GroupNumberFromName(domainRegexGroup);

			this.Name = regex.GroupNameFromNumber(nameGroupNumber);
			this.DomainName = regex.GroupNameFromNumber(domainGroupNumber);
		}
	}
}
