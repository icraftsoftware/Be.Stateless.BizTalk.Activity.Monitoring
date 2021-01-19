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

using FluentAssertions;
using Xunit;

namespace Be.Stateless.BizTalk.Extensions
{
	public class StringExtensionsFixture
	{
		[Theory]
		[MemberData(nameof(ActualProcessNames))]
		public void ToFriendlyProcessName(string processName, string expected)
		{
			processName.ToFriendlyProcessName().Should().Be(expected);
		}

		[Fact]
		public void ToFriendlyProcessNameDoesNotReturnEmpty()
		{
			"Be.Stateless.BizTalk.Process.Archiving.Upload".ToFriendlyProcessName().Should().NotBeEmpty();
		}

		public static readonly object[] ActualProcessNames = {
			new object[] { "Be.Stateless.BizTalk.Factory.Areas.Default.Failed", "Factory/Failed" },
			new object[] { "Be.Stateless.BizTalk.Factory.Areas.Default.Unidentified", "Factory/Unidentified" },
			new object[] { "Be.Stateless.BizTalk.Factory.Areas.Batch.Aggregate", "Factory/Batch/Aggregate" },
			new object[] { "Be.Stateless.BizTalk.Factory.Areas.Batch.Release", "Factory/Batch/Release" },
			new object[] { "Be.Stateless.BizTalk.Factory.Areas.Claim.Check", "Factory/Claim/Check" },
			new object[] { "Be.Stateless.Accounting.Orchestrations.Invoicing.UpdateMasterData", "Accounting/Invoicing/UpdateMasterData" },
			new object[] { "Be.Stateless.Accounting.Orchestrations.UpdateMasterData", "Accounting/UpdateMasterData" }
		};
	}
}
