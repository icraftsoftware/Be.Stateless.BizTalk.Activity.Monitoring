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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Be.Stateless.BizTalk.Extensions;

namespace Be.Stateless.BizTalk.Activity.Monitoring.Model
{
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Public EF Model API.")]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public EF Model API.")]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Public EF Model API.")]
	[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public EF Model API.")]
	public class Process : IActivity
	{
		#region IActivity Members

		public long? RecordID { get; set; }

		public string ActivityID { get; set; }

		public DateTime BeginTime { get; set; }

		public string Name { get; set; }

		public string Status { get; set; }

		public DateTime LastModified { get; set; }

		#endregion

		[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
		public string AggregatedStatus => $"{Status} with {FailedStepsCount} failed step{(FailedStepsCount == 1 ? string.Empty : "s")}";

		public DateTime? EndTime { get; set; }

		public int FailedStepsCount => ProcessingSteps.Count(s => s.Status.StartsWith(Model.Status.Failed.ToString(), StringComparison.OrdinalIgnoreCase))
			+ MessagingSteps.Count(s => s.Status.StartsWith(Model.Status.Failed.ToString(), StringComparison.OrdinalIgnoreCase));

		public string FriendlyName => Name.ToFriendlyProcessName();

		public string InterchangeID { get; set; }

		public virtual List<MessagingStep> MessagingSteps { get; set; }

		public virtual List<ProcessingStep> ProcessingSteps { get; set; }

		public string Value1 { get; set; }

		public string Value2 { get; set; }

		public string Value3 { get; set; }
	}
}
