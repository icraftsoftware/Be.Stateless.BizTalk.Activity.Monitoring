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

using System;
using FluentAssertions;
using Xunit;
using static Be.Stateless.Unit.DelegateFactory;

namespace Be.Stateless.BizTalk.Activity.Monitoring.Configuration
{
	public class ValidatorsAndConvertersFixture
	{
		[Fact]
		public void NonEmptyStringValidatorCannotValidate()
		{
			var validator = ValidatorsAndConverters.NonEmptyStringValidator;
			validator.CanValidate(typeof(object)).Should().BeFalse();
		}

		[Fact]
		public void NonEmptyStringValidatorCanValidate()
		{
			var validator = ValidatorsAndConverters.NonEmptyStringValidator;
			validator.CanValidate(typeof(string)).Should().BeTrue();
		}

		[Fact]
		public void NonEmptyStringValidatorIsASingleton()
		{
			var validator1 = ValidatorsAndConverters.NonEmptyStringValidator;
			validator1.Should().NotBeNull();
			var validator2 = ValidatorsAndConverters.NonEmptyStringValidator;
			validator2.Should().NotBeNull();
			validator1.Should().BeSameAs(validator2);
		}

		[Fact]
		public void NonEmptyStringValidatorThrows()
		{
			var validator = ValidatorsAndConverters.NonEmptyStringValidator;
			Action(() => validator.Validate(null!))
				.Should().Throw<ArgumentException>()
				.WithMessage("The string must be at least 1 characters long.");
		}

		[Fact]
		public void NonEmptyStringValidatorValidates()
		{
			var validator = ValidatorsAndConverters.NonEmptyStringValidator;
			Action(() => validator.Validate("a")).Should().NotThrow();
		}
	}
}
