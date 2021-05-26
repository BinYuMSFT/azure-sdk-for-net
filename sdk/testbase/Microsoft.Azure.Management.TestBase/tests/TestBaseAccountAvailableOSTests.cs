// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Azure.Management.Network.Models;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using System;
using Xunit;

namespace TestBase.Tests
{
    public class TestBaseAccountAvailableOSTests : TestbaseBase
    {
        /*
         * The available OSResourceName obtained through the ListWithHttpMessagesAsync method
Windows-10-20H2 , Windows-10-2004 , Windows-10-1903 , 
Windows-Server-2019---Server-Core , Windows-Server-2019 , 
Windows-Server-2016---Server-Core , Windows-Server-2016 , 
Windows-7.0-SP1 , Windows-10-1909 , Windows-10-1809 , 
Windows-10-1803 , All-Future-OS-Updates
         */

        string availableOSResourceName = "Windows-10-1803";
        string nextPageLink = null;

        [Fact]
        public void TestAvailableOS()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                EnsureClientInitialized(context);

                try
                {
                    var result = t_TestBaseClient.TestBaseAccountAvailableOSs.ListWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, OsUpdateType.SecurityUpdate).GetAwaiter().GetResult();
                    Assert.NotNull(result);
                    Assert.NotNull(result.Body);
                }
                catch (Exception ex)
                {
                    Assert.Contains("NotFound", ex.Message);
                }

                Assert.ThrowsAsync<ErrorResponseException>(() => t_TestBaseClient.TestBaseAccountAvailableOSs.ListNextWithHttpMessagesAsync(nextPageLink));


                Assert.ThrowsAsync<ErrorResponseException>(() => t_TestBaseClient.TestBaseAccountAvailableOS.GetWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, ErrorValue));

                try
                {
                    var availableOS = t_TestBaseClient.TestBaseAccountAvailableOS.GetWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, availableOSResourceName).GetAwaiter().GetResult();
                    Assert.Equal(availableOSResourceName, availableOS.Body.Name);
                }
                catch (Exception ex)
                {
                    Assert.Contains("NotFound", ex.Message);
                }
            }
        }
    }
}
