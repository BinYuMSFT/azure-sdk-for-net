// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Microsoft.TestBase.Models;
using System;
using Xunit;

namespace TestBase.Tests
{
    public class OSUpdateTests : TestbaseBase
    {
        string nextPageLink = null;

        string osUpdateResourceName = "Windows-10-1803";

        [Fact]
        public void TestOSUpdateOperations()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                EnsureClientInitialized(context);

                //Get OSUpdates List
                try
                {
                    var response = t_TestBaseClient.OSUpdates.ListWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, t_PackageNameVer, OsUpdateType.SecurityUpdate).GetAwaiter().GetResult();
                    Assert.NotNull(response);
                    Assert.NotNull(response.Body);
                }
                catch (Exception ex)
                {
                    Assert.Contains("NotFound", ex.Message);
                }

                Assert.ThrowsAsync<ErrorResponseException>(() => t_TestBaseClient.OSUpdates.ListWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, ErrorValue, OsUpdateType.SecurityUpdate));

                Assert.ThrowsAsync<ErrorResponseException>(() => t_TestBaseClient.OSUpdates.ListNextWithHttpMessagesAsync(nextPageLink));

                try
                {
                    var osUpdateResult = t_TestBaseClient.OSUpdate.GetWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, t_PackageNameVer, osUpdateResourceName).GetAwaiter().GetResult();
                    Assert.NotNull(osUpdateResult);
                    Assert.NotNull(osUpdateResult.Body);
                    Assert.Equal(osUpdateResourceName, osUpdateResult.Body.OsName);
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex.Message);
                }
            }
        }
    }
}
