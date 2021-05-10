// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Azure.Management.ManagedServiceIdentity;
using Microsoft.Azure.Management.Network;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Azure.Management.Storage;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Microsoft.TestBase;
using Microsoft.TestBase.Models;
using System;
using System.Collections.Generic;
using System.Net;


namespace TestBase.Tests
{
    public class TestbaseBase
    {
        protected const string TestPrefix = "testbase";
        protected string t_ResourceGroupName= "testbase_rg";
        protected string t_TestBaseAccountName= "testBaseAccount_kaifa";
        /// <summary>
        /// package name + "-" + Version , e.g. package1-1.0.0
        /// For List and Get methods
        /// </summary>
        protected string t_PackageNameVer = "package1_kaifa-1.0";//Package name + "-" + Version
        /// <summary>
        /// package name , e.g. package1
        /// For Create,Update,Delete methods
        /// </summary>
        protected string t_PackageName = "package1_kaifa";

        protected RESTAPIforTestBaseClient t_TestBaseClient;
        protected ResourceManagementClient t_ResourceClient;
        protected StorageManagementClient t_StorageClient;
        protected NetworkManagementClient t_NetworkClient;
        protected ManagedServiceIdentityClient t_ManagedSvcClient;

        protected ResourceGroup t_ResourceGroup;
        protected TestBaseAccountResource t_TestBaseAccount;

        protected bool t_initialized = false;
        protected object t_lock = new object();

        protected string t_subId;
        protected string t_location;

        protected void EnsureClientInitialized(MockContext context)
        {
            if (!t_initialized)
            {
                lock (t_lock)
                {
                    if (!t_initialized)
                    {
                        t_TestBaseClient = TestbaseManagementTestUtilities.GetTestbaseManagementClient(context, new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK });
                        t_ResourceClient = TestbaseManagementTestUtilities.GetResourceManagementClient(context, new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK });
                        t_StorageClient = TestbaseManagementTestUtilities.GetStorageManagementClient(context, new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK });
                        t_NetworkClient = TestbaseManagementTestUtilities.GetNetworkManagementClient(context, new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK });
                        t_ManagedSvcClient = TestbaseManagementTestUtilities.GetManagedServiceIdentityClient(context, new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK });

                        t_subId = t_TestBaseClient.SubscriptionId;
                        t_location = TestbaseManagementTestUtilities.DefaultLocation;
                    }
                }
            }
        }

        protected ResourceGroup CreateResourceGroup(string resourceGroupName)
        {
            t_ResourceGroup = t_ResourceClient.ResourceGroups.CreateOrUpdate(
                resourceGroupName,
                new ResourceGroup
                {
                    Location = t_location,
                    Tags = new Dictionary<string, string>() { { resourceGroupName+"_kaifa", DateTime.UtcNow.ToString("u") } }
                });
            return t_ResourceGroup;
        }

        protected void DeleteResourceGroup(string resourceGroupName)
        {
            try
            {
                t_ResourceClient.ResourceGroups.Delete(resourceGroupName);
            }
            catch (Exception ex)
            { }
        }

        protected TestBaseAccountResource CreateTestBaseAccount(string resourceGroupName, string testBaseAccountName)
        {
            TestBaseAccountSKU sku = new TestBaseAccountSKU();
            sku.Tier = "Basic";
            sku.Name = "B0";

            TestBaseAccountResource testBaseAccountResource = new TestBaseAccountResource();
            //Location must be Global, otherwise will receive a BadRequest message
            testBaseAccountResource.Location = "Global";//Global , North Europe , SoutheastAsia
            testBaseAccountResource.Sku = sku;
            testBaseAccountResource.Tags = new Dictionary<string, string>() { { testBaseAccountName+"_kaifa", DateTime.UtcNow.ToString("u") } };

            //When received the message : Microsoft.Rest.Azure.CloudException : Long running operation failed with status 'Failed'.
            //Refresh the Portal page to see the TestBaseAccount created, but in fact the TestBaseAccount is not available, can't upload/create package.
            try
            {
                t_TestBaseAccount = t_TestBaseClient.TestBaseAccount.Create(testBaseAccountResource, resourceGroupName, testBaseAccountName);
            }
            catch (Exception ex)
            {
                t_TestBaseAccount = t_TestBaseClient.TestBaseAccount.Get(resourceGroupName, testBaseAccountName);
            }
            return t_TestBaseAccount;
        }

        protected void DeleteTestBaseAccount(string resourceGroupName, string testBaseAccountName)
        {
            try
            {
                t_TestBaseClient.TestBaseAccount.Delete(resourceGroupName, testBaseAccountName);
            }
            catch (Exception ex)
            { }
        }

        protected void CreatePackage(PackageResource packageResource, string resourceGroupName, string testBaseAccountName, string packageName)
        {
            try
            {
                //The package could not be created correctly due to a problem with the TestBaseAccount created through the code
                t_TestBaseClient.Package.Create(packageResource, resourceGroupName, testBaseAccountName, packageName);
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// The type of the OS Update
        /// </summary>
        protected class OsUpdateType
        {
            public static string SecurityUpdate = "SecurityUpdate";
            public static string FeatureUpdate = "FeatureUpdate";
        }

        /// <summary>
        /// The type of Analysis Result
        /// </summary>
        protected class AnalysisResultType
        {
            public static string ScriptExecution = "scriptExecution";
            public static string Reliability = "reliability";
            public static string MemoryUtilization = "memoryUtilization";
            public static string CPUUtilization = "CPUUtilization";
            public static string MemoryRegression = "memoryRegression";
            public static string CPURegression = "CPURegression";
        }

        /// <summary>
        /// Gets a formatted string for the current time, yyyyMMddHHmmssfff
        /// </summary>
        protected string ErrorValue { get { return DateTime.Now.ToString("yyyyMMddHHmmssfff"); } }
    }
}
