#region Copyright & License

// Copyright © 2012 - 2020 François Chabot
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
using FluentAssertions;
using Xunit;

namespace Be.Stateless.BizTalk.Activity.Monitoring.Configuration
{
	public class MonitoringConfigurationSectionFixture
	{
		[Fact]
		public void ConfigurationSectionIsDeclaredButEmpty()
		{
			var monitoringConfiguration = (MonitoringConfigurationSection) ConfigurationManager.GetSection("be.stateless.tests/biztalk/monitoring");
			monitoringConfiguration.Should().NotBeNull();
			monitoringConfiguration.Should().BeOfType<MonitoringConfigurationSection>();
			monitoringConfiguration.Should().NotBeSameAs(MonitoringConfigurationSection.Default);
			monitoringConfiguration!.ClaimStoreDirectory.Should().BeNull();
		}

		[Fact]
		public void ConfigurationSectionIsDeclaredWithDefaultElementNames()
		{
			var monitoringConfiguration = MonitoringConfigurationSection.Current;
			monitoringConfiguration.Should().NotBeNull();
			monitoringConfiguration.Should().BeOfType<MonitoringConfigurationSection>();
			monitoringConfiguration.Should().NotBeSameAs(MonitoringConfigurationSection.Default);
			monitoringConfiguration.ClaimStoreDirectory.Should().NotBeNull();
		}

		[Fact]
		public void ConfigurationSectionIsNotDeclared()
		{
			var monitoringConfiguration = (MonitoringConfigurationSection) ConfigurationManager.GetSection("be.stateless.tests/biztalk/undeclaredMonitoringSection");
			monitoringConfiguration.Should().BeNull();
		}
	}
}
