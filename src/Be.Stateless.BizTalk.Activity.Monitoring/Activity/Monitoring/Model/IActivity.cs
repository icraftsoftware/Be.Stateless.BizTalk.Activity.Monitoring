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

namespace Be.Stateless.BizTalk.Activity.Monitoring.Model
{
	public interface IActivity
	{
		string ActivityID { get; set; }

		DateTime BeginTime { get; set; }

		DateTime LastModified { get; set; }

		string Name { get; set; }

		long? RecordID { get; set; }

		// TODO change to enum but enum mapping is only supported in EF >= 5.0 on .NET >= 4.5
		string Status { get; set; }
	}
}
