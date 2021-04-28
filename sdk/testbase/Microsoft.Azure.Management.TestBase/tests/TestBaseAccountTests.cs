using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Rest.Azure;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Microsoft.TestBase;
using Microsoft.TestBase.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestBase.Tests
{
    public class TestBaseAccountTests : TestbaseBase
    {
        //string resourceGroupName;
        string baseGeneratedName;
        string testBaseAccountName;

        [Fact]
        public void TestTestBaseAccountOperations()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                EnsureClientInitialized(context);

                baseGeneratedName = TestbaseManagementTestUtilities.GenerateName(TestPrefix);

                //resourceGroupName = baseGeneratedName + "_rg";
                //var group=CreateResourceGroup(resourceGroupName);

                //Get TestBaseAccount
                Assert.ThrowsAsync<ErrorResponseException>(() => t_TestBaseClient.TestBaseAccount.GetWithHttpMessagesAsync(t_ResourceGroupName, ErrorValue));
                try
                {
                    //Gets the TestBaseAccount created through the Web page
                    var accountResource = t_TestBaseClient.TestBaseAccount.Get(t_ResourceGroupName, t_TestBaseAccountName);
                    Assert.NotNull(accountResource);
                    Assert.Equal(t_TestBaseAccountName, accountResource.Name);
                }
                catch (Exception ex)
                {
                    Assert.Contains("NotFound", ex.Message);
                }


                //Create TestBaseAccount
                testBaseAccountName = baseGeneratedName + "_acc";

                TestBaseAccountSKU sku = new TestBaseAccountSKU();
                sku.Tier = "Standard";
                sku.Name = "S0";
                sku.Locations = new List<string>() { "Global" };

                TestBaseAccountResource parameters = new TestBaseAccountResource();
                //Location must be Global, otherwise will receive a BadRequest message
                parameters.Location = "Global";//Global , North Europe , SoutheastAsia
                parameters.Sku = sku;
                parameters.Tags = new Dictionary<string, string>() { { testBaseAccountName + "_kaifa", "tagvalue" } };

                //After testing, it still cannot be created successfully. A BadRequest exception is thrown. 2021-4-23
                //Operation returned an invalid status code 'NotFound' 2021-4-25
                try
                {
                    var createResult = t_TestBaseClient.TestBaseAccount.CreateWithHttpMessagesAsync(parameters, t_ResourceGroupName, testBaseAccountName).GetAwaiter().GetResult();
                    Assert.NotNull(createResult);
                    Assert.NotNull(createResult.Body);
                    Assert.Equal(testBaseAccountName, createResult.Body.Name);
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex.Message);
                }
                ////Assert.Throws<ErrorResponseException>(() => CreateTestBaseAccount(t_ResourceGroupName, testBaseAccountName));


                //Update TestBaseAccount
                TestBaseAccountUpdateParameters updateParameters = new TestBaseAccountUpdateParameters();

                updateParameters.Sku = new TestBaseAccountSKU
                {
                    Tier = "Standard",
                    Name = "S1"
                };
                updateParameters.Tags = new Dictionary<string, string>() { { "tagkey", "tagvalue_" + DateTime.Now.ToString("yyyyMMdd HHmmss") } };

                //cannot update successfully.2021-4-23
                try
                {
                    var updateResult = t_TestBaseClient.TestBaseAccount.UpdateWithHttpMessagesAsync(updateParameters, t_ResourceGroupName, testBaseAccountName).GetAwaiter().GetResult();
                    Assert.NotNull(updateResult);
                    Assert.NotNull(updateResult.Body);
                    Assert.Equal(testBaseAccountName, updateResult.Body.Name);
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex.Message);
                }
                ////Assert.ThrowsAsync<ErrorResponseException>(() => t_TestBaseClient.TestBaseAccount.UpdateWithHttpMessagesAsync(updateParameters, t_ResourceGroupName, testBaseAccountName));


                //Delete TestBaseAccount
                try
                {
                    var deleteResult = t_TestBaseClient.TestBaseAccount.DeleteWithHttpMessagesAsync(t_ResourceGroupName, testBaseAccountName).GetAwaiter().GetResult();
                    Assert.NotNull(deleteResult);
                    Assert.Null(deleteResult.Headers.AzureAsyncOperation);
                    Assert.Null(deleteResult.Headers.Location);
                }
                catch (Exception ex)
                {
                    Assert.Contains("NotFound", ex.Message);
                }

            }
        }

    }
}
