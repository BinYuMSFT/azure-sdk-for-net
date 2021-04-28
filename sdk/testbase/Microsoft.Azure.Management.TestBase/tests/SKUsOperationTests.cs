using Microsoft.Rest.Azure;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Microsoft.TestBase.Models;
using Xunit;

namespace TestBase.Tests
{
    public class SKUsOperationTests : TestbaseBase
    {
        [Fact]
        public void TestSKUs()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                EnsureClientInitialized(context);

                AzureOperationResponse<IPage<TestBaseAccountSKU>> result = t_TestBaseClient.SKUs.ListWithHttpMessagesAsync().GetAwaiter().GetResult();
                Assert.NotNull(result);
                Assert.NotNull(result.Body);// B0/testBaseAccounts/Basic , S0/testBaseAccounts/Standard

                var nextList=t_TestBaseClient.SKUs.ListNextWithHttpMessagesAsync(null);

                Assert.True(nextList.Exception.InnerExceptions.Count == 1);
            }
        }

    }
}
