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
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Be.Stateless.BizTalk.Extensions;

namespace Be.Stateless.BizTalk.Activity.Monitoring.Model
{
	public class ActivityContext : DbContext
	{
		static ActivityContext()
		{
			// database is deployed along with the BAM activity model and never needs to be initialized by EF
			Database.SetInitializer<ActivityContext>(null);
		}

		public static IEnumerable<ProcessDescriptor> ProcessDescriptors
		{
			get
			{
				return RegisteredProcessNames
					.OrderBy(n => n.ToFriendlyProcessName())
					.Select(name => new ProcessDescriptor { Name = name });
			}
		}

		[SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Done by SqlCommand.")]
		private static IEnumerable<string> RegisteredProcessNames
		{
			get
			{
				var cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["BizTalkFactoryMgmtDb"].ConnectionString);
				cnx.Open();
				using (var cmd = new SqlCommand("SELECT Name FROM monitoring_ProcessDescriptors", cnx))
				using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
				{
					return reader.Cast<IDataRecord>().Select(row => (string) row["Name"]).ToArray();
				}
			}
		}

		#region Base Class Member Overrides

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
			modelBuilder.Configurations.Add(new ProcessEntityConfiguration());
			modelBuilder.Configurations.Add(new ProcessingStepEntityConfiguration());
			modelBuilder.Configurations.Add(new MessagingStepEntityConfiguration());
			modelBuilder.Configurations.Add(new MessageBodyEntityConfiguration());
			modelBuilder.Configurations.Add(new MessageContextEntityConfiguration());
		}

		#endregion

		public DbSet<MessagingStep> MessagingSteps { get; set; }

		public DbSet<Process> Processes { get; set; }

		public DbSet<ProcessingStep> ProcessingSteps { get; set; }
	}
}
