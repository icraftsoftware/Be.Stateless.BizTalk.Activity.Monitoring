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

using Be.Stateless.BizTalk.ContextProperties;
using FluentAssertions;
using Xunit;

namespace Be.Stateless.BizTalk.Activity.Monitoring.Model
{
	public class MessageContextFixture
	{
		[Fact]
		public void GetBoolean()
		{
			var sut = new MessageContext { EncodedContext = ENCODED_CONTEXT };

			sut.GetProperty(BtsProperties.RouteMessageOnFailure).Value.Should().BeTrue();
			sut.GetProperty(BtsProperties.RouteMessageOnFailure).IsPromoted.Should().BeFalse();
		}

		[Fact]
		public void GetDateTime()
		{
			var sut = new MessageContext { EncodedContext = ENCODED_CONTEXT };

			sut.GetProperty(FileProperties.FileCreationTime).Value.Should().Be(new(2018, 10, 7, 13, 38, 40));
			sut.GetProperty(FileProperties.FileCreationTime).IsPromoted.Should().BeFalse();
		}

		[Fact]
		public void GetInteger()
		{
			var sut = new MessageContext { EncodedContext = ENCODED_CONTEXT };

			sut.GetProperty(BtsProperties.ActualRetryCount).Value.Should().Be(1);
			sut.GetProperty(BtsProperties.ActualRetryCount).IsPromoted.Should().BeFalse();
		}

		[Fact]
		public void GetMissingProperty()
		{
			var sut = new MessageContext { EncodedContext = ENCODED_CONTEXT };

			sut.GetProperty(FileProperties.Username).Should().BeNull();
		}

		[Fact]
		public void GetString()
		{
			var sut = new MessageContext { EncodedContext = ENCODED_CONTEXT };

			sut.GetProperty(FileProperties.ReceivedFileName).Value.Should().Be("received-file-name.xml");
		}

		private const string ENCODED_CONTEXT =
			"<context xmlns:s0='http://schemas.microsoft.com/BizTalk/2003/file-properties' xmlns:s6='http://schemas.microsoft.com/BizTalk/2003/system-properties'>"
			+ "<s0:p n='FileCreationTime'>2018-10-07T13:38:40</s0:p>"
			+ "<s0:p n='ReceivedFileName'>received-file-name.xml</s0:p>"
			+ "<s6:p n='ActualRetryCount'>1</s6:p>"
			+ "<s6:p n='RouteMessageOnFailure'>true</s6:p>"
			+ "</context>";
	}
}
