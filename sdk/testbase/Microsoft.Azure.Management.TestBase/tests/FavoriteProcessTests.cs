using Microsoft.Rest;
using Microsoft.Rest.Azure;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Microsoft.TestBase.Models;
using System;
using Xunit;

namespace TestBase.Tests
{
    public class FavoriteProcessTests : TestbaseBase
    {
        AzureOperationResponse<IPage<FavoriteProcessResource>> favoriteResponse;

        string nextPageLink = null;
        string favResourceName = "AM_Base.exe";
        

        [Fact]
        public void TestFavoriteOperations()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                EnsureClientInitialized(context);

                try
                {
                    favoriteResponse = t_TestBaseClient.FavoriteProcesses.ListWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, t_PackageNameVer).GetAwaiter().GetResult();
                    Assert.NotNull(favoriteResponse);
                    Assert.NotNull(favoriteResponse.Body);
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex.Message);
                }

                Assert.ThrowsAsync<System.UriFormatException>(() => t_TestBaseClient.FavoriteProcesses.ListNextWithHttpMessagesAsync(nextPageLink));

                try
                {
                    var getResult=t_TestBaseClient.FavoriteProcess.GetWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, t_PackageNameVer, favResourceName).GetAwaiter().GetResult();
                    Assert.NotNull(getResult);
                    Assert.NotNull(getResult.Body);
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex.Message);
                }
                

                FavoriteProcessResource parameters = new FavoriteProcessResource();
                //parameters.ActualProcessName="Resource groups";
                //var createResult=t_TestBaseClient.FavoriteProcess.CreateWithHttpMessagesAsync(parameters, t_ResourceGroupName, t_TestBaseAccountName, t_PackageName, favResourceName).GetAwaiter().GetResult();

                Assert.ThrowsAsync<ValidationException>(() => t_TestBaseClient.FavoriteProcess.CreateWithHttpMessagesAsync(parameters, t_ResourceGroupName, t_TestBaseAccountName, t_PackageName, favResourceName));

                Assert.ThrowsAsync<ErrorResponseException>(() => t_TestBaseClient.FavoriteProcess.DeleteWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, t_PackageName, favResourceName));
            }
        }
    }
}
