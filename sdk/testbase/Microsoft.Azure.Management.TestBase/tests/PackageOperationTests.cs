// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Rest.Azure;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Microsoft.TestBase.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestBase.Tests
{
    public class PackageOperationTests : TestbaseBase
    {
        string baseGeneratedName;
        string packageName;
        string nextPageLink = null;
        string downloadUrl = "";


        [Fact]
        public void TestPackageOperations()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                EnsureClientInitialized(context);
                baseGeneratedName = TestbaseManagementTestUtilities.GenerateName(TestPrefix);


                //Get Package List
                try
                {
                    AzureOperationResponse<IPage<PackageResource>> packageResources = t_TestBaseClient.Packages.ListByTestBaseAccountWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName).GetAwaiter().GetResult();
                    Assert.NotNull(packageResources);
                    Assert.NotNull(packageResources.Body);

                }
                catch (Exception ex)
                {
                    Assert.Contains("NotFound", ex.Message);
                }

                Assert.ThrowsAsync<ErrorResponseException>(() => t_TestBaseClient.Packages.ListByTestBaseAccountNextWithHttpMessagesAsync(nextPageLink));

                //Get a Package that does not exist
                Assert.ThrowsAsync<ErrorResponseException>(() => t_TestBaseClient.Package.GetWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, ErrorValue));

                //Gets the Package uploaded via the Web page
                try
                {
                    var packageResponse = t_TestBaseClient.Package.GetWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, t_PackageNameVer).GetAwaiter().GetResult();
                    Assert.NotNull(packageResponse);
                    Assert.NotNull(packageResponse.Body);
                    Assert.Equal(t_PackageNameVer, packageResponse.Body.Name);
                }
                catch (Exception ex)
                {
                    Assert.Contains("NotFound", ex.Message);
                }


                //Create Package
                #region Create Package
                packageName = baseGeneratedName + "_pkg";
                PackageResource packageResource = new PackageResource();
                packageResource.Location = "Global";
                packageResource.Tags = new Dictionary<string, string>()
                {
                    { "package_create",DateTime.Now.ToString("yyyyMMdd HHmmss")}
                };
                packageResource.ApplicationName = packageName;
                packageResource.Version = "1.0.0";
                packageResource.TargetOSList = new List<TargetOSInfo>()
                {
                    new TargetOSInfo(
                        OsUpdateType.FeatureUpdate,new List<string>()
                        {
                            "Windows 10 20H2",
                            "Windows 10 2004"
                        }),
                    new TargetOSInfo(
                        OsUpdateType.SecurityUpdate,new List<string>()
                        {
                            "Windows 10 20H2",
                            "Windows 10 2004"
                        })
                };
                packageResource.FlightingRing = "Insider-Beta-Channel";
                //The url is a one-time use,so need to retrieve it each time
                try
                {
                    var downloadUrlResponse = t_TestBaseClient.PackageGetDownloadURLWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, t_PackageNameVer).GetAwaiter().GetResult();
                    downloadUrl = downloadUrlResponse.Body.DownloadUrl;
                    //downloadUrl=downloadUrlResponse.Body.DownloadUrl.ToLower().Split(".zip")[0] + ".zip";
                }
                catch (Exception ex)
                {
                    downloadUrl = "https://tbwestusppesa.blob.core.windows.net/c0097881-16f2-4c2a-b6f1-1e2c7d7cb8e7/prod/83107fe9-8cf4-4e77-9cd4-e79acb7ee52b/689c6f6a-bb9b-416f-8963-400af5bf1981/1/test.zip?sv=2019-07-07&sr=b&sig=%2FCo6pf5tnVghr0zvx0q0l6HCMWzP0tP5OcMhKJGPXT0%3D&se=2021-05-26T03%3A17%3A29Z&sp=rl";
                    Assert.NotNull(ex.Message);
                }
                packageResource.BlobPath = downloadUrl;//After the test, Neither the full body.downloadURL nor the truncated one is valid
                packageResource.Tests = new List<Test>()
                {
                    new Test()
                    {
                        TestType="OutOfBoxTest",
                        IsActive=true,
                        Commands=new List<Command>()
                        {
                            new Command()
                            {
                                Name="Install",
                                Action="Install",
                                ContentType="Path",
                                Content="test/scripts/install/job.ps1",
                                RunElevated=true,
                                RestartAfter=true,
                                MaxRunTime=1800,
                                RunAsInteractive=true,
                                AlwaysRun=true,
                                ApplyUpdateBefore=false
                            },
                            new Command()
                            {
                                Name="Launch",
                                Action="Launch",
                                ContentType="Path",
                                Content="test/scripts/launch/job.ps1",
                                RunElevated=true,
                                RestartAfter=false,
                                MaxRunTime=1800,
                                RunAsInteractive=true,
                                AlwaysRun=false,
                                ApplyUpdateBefore=true
                            },
                            new Command()
                            {
                                Name="Close",
                                Action="Close",
                                ContentType="Path",
                                Content="test/scripts/close/job.ps1",
                                RunElevated=true,
                                RestartAfter=false,
                                MaxRunTime=1800,
                                RunAsInteractive=true,
                                AlwaysRun=false,
                                ApplyUpdateBefore=false
                            },
                            new Command()
                            {
                                Name="Uninstall",
                                Action="Uninstall",
                                ContentType="Path",
                                Content="test/scripts/uninstall/job.ps1",
                                RunElevated=true,
                                RestartAfter=false,
                                MaxRunTime=1800,
                                RunAsInteractive=true,
                                AlwaysRun=true,
                                ApplyUpdateBefore=false
                            }
                        }
                    }
                };
                try
                {
                    //CreateWithHttpMessagesAsync
                    var createResult = t_TestBaseClient.Package.CreateWithHttpMessagesAsync(packageResource, t_ResourceGroupName, t_TestBaseAccountName, packageName).GetAwaiter().GetResult();
                    Assert.NotNull(createResult);
                    Assert.NotNull(createResult.Body);
                    Assert.Equal(packageName, createResult.Body.Name);
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex.Message);
                }
                #endregion


                //Update Package
                Dictionary<string, string> tags = new Dictionary<string, string>();
                tags.Add("tagkey", "tagvalue_" + DateTime.Now.ToString("yyyyMMdd HHmmss"));
                PackageUpdateParameters packageUpdateParameters = new PackageUpdateParameters();
                packageUpdateParameters.Tags = tags;

                try
                {
                    var updateResult = t_TestBaseClient.Package.UpdateWithHttpMessagesAsync(packageUpdateParameters, t_ResourceGroupName, t_TestBaseAccountName, packageName).GetAwaiter().GetResult();

                    Assert.NotNull(updateResult);
                    Assert.NotNull(updateResult.Body);
                    Assert.Equal(packageName, updateResult.Body.Name);
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex.Message);
                }


                //Delete Package
                //Verify with PythonSDK that delete package using the Package Name without the version number
                //The package removal feature in the.NET version is still not available. 2021-5-26
                try
                {
                    var deleteResponse = t_TestBaseClient.Package.DeleteWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, packageName).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex.Message);
                }

            }
        }
    }
}
