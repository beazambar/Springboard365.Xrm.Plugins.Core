﻿namespace DSmall.DynamicsCrm.Plugins.Core.UnitTest
{
    using System;
    using DSmall.DynamicsCrm.UnitTest.Core;
    using Microsoft.Xrm.Sdk;

    /// <summary>The add item campaign plugin specification fixture.</summary>
    public class AddItemCampaignPluginSpecificationFixture : SpecificationFixture<DummyAddItemCampaignPlugin>
    {
        /// <summary>The perform test setup.</summary>
        public override void PerformTestSetup()
        {
            ServiceProvider = ServiceProviderInitializer.Setup().WithInputParameters(GetDummyEntityCollection());
        }

        private static ParameterCollection GetDummyEntityCollection()
        {
            return new ParameterCollection
            {
                { "CampaignId", Guid.NewGuid() },
                { "EntityId", Guid.NewGuid() },
                { "EntityName", "Contact" },
            };
        }
    }
}
