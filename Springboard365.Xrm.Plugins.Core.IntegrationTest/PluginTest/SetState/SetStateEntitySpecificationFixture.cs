﻿namespace Springboard365.Xrm.Plugins.Core.IntegrationTest
{
    using System;
    using Microsoft.Crm.Sdk.Messages;
    using Microsoft.Xrm.Sdk;

    public class SetStateEntitySpecificationFixture : SpecificationFixtureBase
    {
        public SetStateRequest SetStateRequest { get; private set; }

        public string MessageName { get; private set; }

        public void PerformTestSetup()
        {
            MessageName = "SetStateDynamicEntity";

            var targetEntity = new Entity("contact")
            {
                Id = Guid.NewGuid()
            };
            targetEntity["firstname"] = "DummyFirstName";
            OrganizationService.Create(targetEntity);
            
            SetStateRequest = new SetStateRequest
            {
                EntityMoniker = targetEntity.ToEntityReference(),
                State = new OptionSetValue(1),
                Status = new OptionSetValue(2)
            };
        }
    }
}