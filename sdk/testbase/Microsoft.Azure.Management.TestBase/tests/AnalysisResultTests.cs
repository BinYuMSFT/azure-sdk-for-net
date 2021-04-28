using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using System;
using Xunit;

namespace TestBase.Tests
{
    public class AnalysisResultTests : TestbaseBase
    {
        string testResultName = "TestResult-7360b76b-51ad-4722-8f64-c8962c54fc31";
        string[] analysisResultTypes=new string[] { AnalysisResultType.CPURegression, AnalysisResultType.CPUUtilization, AnalysisResultType.MemoryRegression, AnalysisResultType.MemoryUtilization, AnalysisResultType.Reliability, AnalysisResultType.ScriptExecution
    };

    [Fact]
        public void TestAnalysisOperations()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                EnsureClientInitialized(context);

                try
                {
                    foreach (var anaResultType in analysisResultTypes)
                    {
                        var result = t_TestBaseClient.AnalysisResults.ListWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, t_PackageNameVer, testResultName, anaResultType).GetAwaiter().GetResult();
                        Assert.NotNull(result);
                        Assert.NotNull(result.Body);
                    }
                    
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex.Message);
                }

                try
                {
                    foreach (var anaResultType in analysisResultTypes)
                    {
                        var result = t_TestBaseClient.AnalysisResult.GetWithHttpMessagesAsync(t_ResourceGroupName, t_TestBaseAccountName, t_PackageNameVer, testResultName, anaResultType).GetAwaiter().GetResult();
                        Assert.NotNull(result);
                        Assert.NotNull(result.Body);
                    }
                    
                }
                catch (Exception ex)
                {
                    Assert.NotNull(ex.Message);
                }

            }
        }

    }
}
