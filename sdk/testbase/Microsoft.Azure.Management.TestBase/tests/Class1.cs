using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Microsoft.TestBase;
using Microsoft.TestBase.Models;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace TestBase.Tests
{
    public class Class1:TestbaseBase
    {
        PackageResource packageResource;

        string resourceGroupName = "kaifa-test-rg";
        string testBaseAccountName = "testbaseaccount2_kaifa";
        string packageName = "package1_kaifa-1.0";//Package name + "-" + Version

        [Fact]
        public void Test()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                EnsureClientInitialized(context);

                //not found
                //var result = t_TestBaseClient.TestBaseAccountUsage.ListWithHttpMessagesAsync(resourceGroupName, testBaseAccountName).GetAwaiter().GetResult();

                //Get Package Successfully
                //packageResource = t_TestBaseClient.Package.Get(t_ResourceGroupName, t_TestBaseAccountName, "testpackage2");

                var deleteHeaders = t_TestBaseClient.Package.DeleteWithHttpMessagesAsync("testbase_rg", "testBaseAccount_kaifa", "testpackage1-1.0.0").GetAwaiter().GetResult();

                return;
                //var deleteHeaders=t_TestBaseClient.Package.Delete("testbase_rg", "testBaseAccount_kaifa", "testpackage1-1.0.0");
                //t_ResourceClient.ResourceGroups.Delete("testbase4802rg");
                return;


                RecordedDelegatingHandler handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };
                handler.IsPassThrough = true;

                ResourceManagementClient resourceMngClient = context.GetServiceClient<ResourceManagementClient>(handlers: handler);
                RESTAPIforTestBaseClient testBaseClient = context.GetServiceClient<RESTAPIforTestBaseClient>(handlers: handler);

                packageResource= testBaseClient.Package.Get(resourceGroupName, testBaseAccountName, packageName);

                //Deleting a package takes a very, very long time
                //PackageDeleteHeaders delteHeaders =testBaseClient.Package.Delete(resourceGroupName, testBaseAccountName, packageName);

                return;
                try
                {
                    #region Create ResourceGroup
                    ResourceGroup resourceGroup = resourceMngClient.ResourceGroups.CreateOrUpdate(
                        resourceGroupName,
                        new ResourceGroup
                        {
                            Location = "North Europe",
                            Tags = new Dictionary<string, string>() { { resourceGroupName, DateTime.UtcNow.ToString("u") } }
                        });
                    #endregion

                    #region Create TestBaseAccount
                    TestBaseAccountSKU sku = new TestBaseAccountSKU();
                    sku.Tier = "Basic";
                    sku.Name = "B0";

                    TestBaseAccountResource parameters = new TestBaseAccountResource();
                    parameters.Location = "Global";
                    parameters.Sku = sku;

                    testBaseClient.TestBaseAccount.Create(parameters, resourceGroupName, testBaseAccountName);
                    #endregion
                }
                catch (Exception ex)
                { }

                try
                {
                    //testBaseClient.TestBaseAccount.Delete(resourceGroupName, testBaseAccountName);
                    resourceMngClient.ResourceGroups.Delete(resourceGroupName);
                }
                catch (Exception ex)
                { }
            }
        }
    }
}
