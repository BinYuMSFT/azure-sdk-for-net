// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.AI.MetricsAdvisor.Models
{
    internal partial class WebhookHookInfoPatch : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(HookParameter))
            {
                writer.WritePropertyName("hookParameter");
                writer.WriteObjectValue(HookParameter);
            }
            writer.WritePropertyName("hookType");
            writer.WriteStringValue(HookType.ToString());
            if (Optional.IsDefined(HookName))
            {
                writer.WritePropertyName("hookName");
                writer.WriteStringValue(HookName);
            }
            if (Optional.IsDefined(Description))
            {
                writer.WritePropertyName("description");
                writer.WriteStringValue(Description);
            }
            if (Optional.IsDefined(ExternalLink))
            {
                writer.WritePropertyName("externalLink");
                writer.WriteStringValue(ExternalLink);
            }
            writer.WriteEndObject();
        }
    }
}