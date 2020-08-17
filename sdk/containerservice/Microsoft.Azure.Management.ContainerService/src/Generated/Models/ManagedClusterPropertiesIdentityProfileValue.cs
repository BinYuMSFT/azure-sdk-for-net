// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.ContainerService.Models
{
    using System.Linq;

    public partial class ManagedClusterPropertiesIdentityProfileValue : UserAssignedIdentity
    {
        /// <summary>
        /// Initializes a new instance of the
        /// ManagedClusterPropertiesIdentityProfileValue class.
        /// </summary>
        public ManagedClusterPropertiesIdentityProfileValue()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// ManagedClusterPropertiesIdentityProfileValue class.
        /// </summary>
        /// <param name="resourceId">The resource id of the user assigned
        /// identity.</param>
        /// <param name="clientId">The client id of the user assigned
        /// identity.</param>
        /// <param name="objectId">The object id of the user assigned
        /// identity.</param>
        public ManagedClusterPropertiesIdentityProfileValue(string resourceId = default(string), string clientId = default(string), string objectId = default(string))
            : base(resourceId, clientId, objectId)
        {
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

    }
}