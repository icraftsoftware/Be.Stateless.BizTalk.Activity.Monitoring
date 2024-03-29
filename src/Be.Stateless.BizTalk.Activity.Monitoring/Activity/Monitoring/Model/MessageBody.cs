﻿#region Copyright & License

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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Be.Stateless.BizTalk.Activity.Monitoring.Configuration;
using Be.Stateless.BizTalk.Activity.Tracking;
using Be.Stateless.BizTalk.ContextProperties;
using Be.Stateless.Extensions;
using Be.Stateless.IO;
using Be.Stateless.IO.Extensions;
using Path = System.IO.Path;

namespace Be.Stateless.BizTalk.Activity.Monitoring.Model
{
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Public EF Model API.")]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Public EF Model API.")]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Public EF Model API.")]
	public class MessageBody
	{
		public string Body
		{
			get
			{
				using (var stream = new StreamReader(Stream))
				{
					if (!HasBeenClaimed) return stream.ReadToEnd();
					var length = stream.Read(_buffer, 0, _buffer.Length);
					return new(_buffer, 0, length);
				}
			}
		}

		public bool ClaimAvailable => HasBeenClaimed && File.Exists(Path.Combine(MonitoringConfigurationSection.Current.ClaimStoreDirectory, EncodedBody));

		public string EncodedBody { get; set; }

		public string EncodedBodyType { get; set; }

		public bool HasBeenClaimed => EncodedBodyType.IfNotNull(value => value == MessageBodyCaptureMode.Claimed.ToString());

		public bool HasContent => HasBeenClaimed
			? !MonitoringConfigurationSection.Current.ClaimStoreDirectory.IsNullOrEmpty()
			: !EncodedBody.IsNullOrEmpty();

		public MessagingStep MessagingStep { get; set; }

		public string MessagingStepActivityID { get; set; }

		public string MimeType
		{
			get
			{
				if (!HasContent) return null;
				using (var stream = Stream)
				{
					return stream.GetMimeType();
				}
			}
		}

		[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
		public string ReceivedFileName
		{
			get
			{
				var receivedFileNameProperty = MessagingStep.Context.GetProperty(FileProperties.ReceivedFileName);
				return receivedFileNameProperty.IfNotNull(f => Path.GetFileName(f.Value))
					?? (MimeType.StartsWith("application/", StringComparison.OrdinalIgnoreCase)
						? "message.bin"
						: MimeType.Equals("text/html", StringComparison.OrdinalIgnoreCase)
							? "message.html"
							: MimeType.StartsWith("text/", StringComparison.OrdinalIgnoreCase)
								? Body[0] == '<' ? "message.xml" : "message.txt"
								: "message.unknown");
			}
		}

		public Stream Stream => !HasContent
			? new MemoryStream()
			: HasBeenClaimed
				? ClaimedStream
				: EncodedBody.DecompressFromBase64String();

		private Stream ClaimedStream => ClaimAvailable
			? File.OpenRead(Path.Combine(MonitoringConfigurationSection.Current.ClaimStoreDirectory, EncodedBody))
			: new StringStream($"The captured payload entry '{EncodedBody}' is not yet available in the central store.");

		// buffer used to read the first preview characters of large message bodies
		private static readonly char[] _buffer = new char[1024];
	}
}
