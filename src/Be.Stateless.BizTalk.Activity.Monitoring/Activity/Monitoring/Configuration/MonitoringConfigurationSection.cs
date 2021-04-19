#region Copyright & License

// Copyright © 2012 - 2021 François Chabot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Configuration;
using System.Threading;
using System.Web.Configuration;

namespace Be.Stateless.BizTalk.Activity.Monitoring.Configuration
{
	public class MonitoringConfigurationSection : ConfigurationSection
	{
		static MonitoringConfigurationSection()
		{
			_properties.Add(_claimStoreDirectory);
		}

		#region Base Class Member Overrides

		/// <summary>
		/// Gets the collection of properties.
		/// </summary>
		/// <returns>
		/// The <see cref="ConfigurationPropertyCollection"/> collection of properties for the element.
		/// </returns>
		protected override ConfigurationPropertyCollection Properties => _properties;

		#endregion

		[ConfigurationProperty(CLAIM_STORE_DIRECTORY_PROPERTY_NAME)]
		[StringValidator(MinLength = 1)]
		public string ClaimStoreDirectory => (string) base[_claimStoreDirectory];

		private const string CLAIM_STORE_DIRECTORY_PROPERTY_NAME = "claimStoreDirectory";

		private const string DEFAULT_SECTION_NAME = "be.stateless/biztalk/monitoring";

		private static MonitoringConfigurationSection _defaultSection;
		private static readonly ConfigurationPropertyCollection _properties = new();

		private static readonly ConfigurationProperty _claimStoreDirectory = new(
			CLAIM_STORE_DIRECTORY_PROPERTY_NAME,
			typeof(string),
			null,
			null,
			ValidatorsAndConverters.NonEmptyStringValidator,
			ConfigurationPropertyOptions.None);

		#region Factory Helpers

		public static MonitoringConfigurationSection Current
		{
			get
			{
				var section = (MonitoringConfigurationSection) WebConfigurationManager.GetWebApplicationSection(DEFAULT_SECTION_NAME) ?? Default;
				return section;
			}
		}

		internal static MonitoringConfigurationSection Default
		{
			get
			{
				if (_defaultSection == null)
				{
					Interlocked.CompareExchange(ref _defaultSection, new MonitoringConfigurationSection(), null);
				}
				return _defaultSection;
			}
		}

		#endregion
	}
}
