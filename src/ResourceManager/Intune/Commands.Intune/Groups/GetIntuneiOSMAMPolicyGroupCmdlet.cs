﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.Azure.Commands.Intune
{
    using Management.Intune;
    using Management.Intune.Models;
    using Microsoft.Azure.Commands.Intune.Properties;
    using System.Management.Automation;

    /// <summary>
    /// Cmdlet to get groups for iOS platform.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmIntuneiOSMAMPolicyGroup"), OutputType(typeof(PSObject))]
    public sealed class GetIntuneiOSMAMPolicyGroupCmdlet : IntuneBaseCmdlet
    {
        /// <summary>
        /// Gets the policy Name
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The policy name to fetch for the groups.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Contains the cmdlet's execution logic.
        /// </summary>
        protected override void ProcessRecord()
        {
            MultiPageGetter<GroupItem> mpg = new MultiPageGetter<GroupItem>();
            var items = mpg.GetAllResources(
                this.IntuneClient.Ios.GetGroupsForMAMPolicy,
                this.IntuneClient.Ios.GetGroupsForMAMPolicyNext,
                this.AsuHostName,
                filter: null);

            if (items.Count > 0)
            {
                this.WriteObject(items, enumerateCollection: true);
            }
            else
            {
                this.WriteObject(Resources.NoItemsReturned);
            }
        }
    }
}
