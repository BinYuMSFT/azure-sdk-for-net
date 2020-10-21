// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Datadog
{
    using Microsoft.Rest;
    using Microsoft.Rest.Azure;
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// SingleSignOnConfigurationsOperations operations.
    /// </summary>
    public partial interface ISingleSignOnConfigurationsOperations
    {
        /// <summary>
        /// List the single sign-on configurations for a given monitor
        /// resource.
        /// </summary>
        /// <param name='resourceGroupName'>
        /// The name of the resource group to which the Datadog resource
        /// belongs.
        /// </param>
        /// <param name='monitorName'>
        /// Monitor resource name
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="ResourceProviderDefaultErrorResponseException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        Task<AzureOperationResponse<IPage<DatadogSingleSignOnResource>>> ListWithHttpMessagesAsync(string resourceGroupName, string monitorName, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Configures single-sign-on for this resource.
        /// </summary>
        /// <param name='resourceGroupName'>
        /// The name of the resource group to which the Datadog resource
        /// belongs.
        /// </param>
        /// <param name='monitorName'>
        /// Monitor resource name
        /// </param>
        /// <param name='configurationName'>
        /// </param>
        /// <param name='properties'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="ResourceProviderDefaultErrorResponseException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        Task<AzureOperationResponse<DatadogSingleSignOnResource>> CreateOrUpdateWithHttpMessagesAsync(string resourceGroupName, string monitorName, string configurationName, DatadogSingleSignOnProperties properties = default(DatadogSingleSignOnProperties), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Gets the datadog single sign-on resource for the given Monitor.
        /// </summary>
        /// <param name='resourceGroupName'>
        /// The name of the resource group to which the Datadog resource
        /// belongs.
        /// </param>
        /// <param name='monitorName'>
        /// Monitor resource name
        /// </param>
        /// <param name='configurationName'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="ResourceProviderDefaultErrorResponseException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        Task<AzureOperationResponse<DatadogSingleSignOnResource>> GetWithHttpMessagesAsync(string resourceGroupName, string monitorName, string configurationName, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Configures single-sign-on for this resource.
        /// </summary>
        /// <param name='resourceGroupName'>
        /// The name of the resource group to which the Datadog resource
        /// belongs.
        /// </param>
        /// <param name='monitorName'>
        /// Monitor resource name
        /// </param>
        /// <param name='configurationName'>
        /// </param>
        /// <param name='properties'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="ResourceProviderDefaultErrorResponseException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        Task<AzureOperationResponse<DatadogSingleSignOnResource>> BeginCreateOrUpdateWithHttpMessagesAsync(string resourceGroupName, string monitorName, string configurationName, DatadogSingleSignOnProperties properties = default(DatadogSingleSignOnProperties), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// List the single sign-on configurations for a given monitor
        /// resource.
        /// </summary>
        /// <param name='nextPageLink'>
        /// The NextLink from the previous successful call to List operation.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="ResourceProviderDefaultErrorResponseException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="Microsoft.Rest.SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        Task<AzureOperationResponse<IPage<DatadogSingleSignOnResource>>> ListNextWithHttpMessagesAsync(string nextPageLink, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}