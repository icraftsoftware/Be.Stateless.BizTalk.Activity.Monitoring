﻿#region Copyright & License

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
using System.Diagnostics.CodeAnalysis;

namespace Be.Stateless.BizTalk.Activity.Monitoring.Model
{
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Public EF Model API.")]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Public EF Model API.")]
	[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public EF Model API.")]
	public class ProcessingStep : IActivity
	{
		#region IActivity Members

		public long? RecordID { get; set; }

		public string ActivityID { get; set; }

		public DateTime BeginTime { get; set; }

		public string Name { get; set; }

		public string Status { get; set; }

		public DateTime LastModified { get; set; }

		#endregion

		public DateTime? EndTime { get; set; }

		public string ErrorDescription { get; set; }

		public string MachineName { get; set; }

		public virtual Process Process { get; set; }
	}
}
