﻿namespace DSmall.DynamicsCrm.Plugins.Core.IntegrationTest
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>The crm reader.</summary>
    public class CrmReader : ICrmReader
    {
        private readonly IOrganizationService organizationService;

        /// <summary>Initialises a new instance of the <see cref="CrmReader"/> class.</summary>
        /// <param name="organizationService">The organization service.</param>
        public CrmReader(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        /// <summary>The get system user.</summary>
        /// <returns>The <see cref="EntityReference"/>.</returns>
        public EntityReference GetSystemUser()
        {
            var query = new QueryByAttribute
            {
                EntityName = "systemuser",
                ColumnSet = new ColumnSet("systemuserid")
            };
            query.AddAttributeValue("firstname", "David");

            var entityCollection = organizationService.RetrieveMultiple(query);

            return entityCollection.Entities.First().ToEntityReference();
        }

        /// <summary>The get or create queue.</summary>
        /// <param name="queueName">The queue name.</param>
        /// <returns>The <see cref="Guid"/>.</returns>
        public Guid GetOrCreateQueue(string queueName)
        {
            var query = new QueryByAttribute
            {
                EntityName = "queue",
                ColumnSet = new ColumnSet("queueid")
            };
            query.AddAttributeValue("name", queueName);

            var entityCollection = organizationService.RetrieveMultiple(query);

            if (entityCollection.Entities.Count > 0)
            {
                return entityCollection.Entities.First().Id;
            }

            var queueEntity = new Entity("queue");
            queueEntity["name"] = queueName;
            return organizationService.Create(queueEntity);
        }

        /// <summary>The get currency id.</summary>
        /// <returns>The <see cref="EntityReference"/>.</returns>
        public EntityReference GetCurrencyId()
        {
            var query = new QueryExpression
            {
                EntityName = "transactioncurrency",
                ColumnSet = new ColumnSet("transactioncurrencyid")
            };

            var entityCollection = organizationService.RetrieveMultiple(query);

            if (entityCollection.Entities.Count == 0)
            {
                throw new InvalidDataException("No currency records exist.");
            }

            return entityCollection.Entities.First().ToEntityReference();
        }

        /// <summary>The get contract template id.</summary>
        /// <returns>The <see cref="EntityReference"/>.</returns>
        public EntityReference GetContractTemplateId()
        {
            var query = new QueryExpression
            {
                EntityName = "contracttemplate",
                ColumnSet = new ColumnSet("contracttemplateid")
            };

            var entityCollection = organizationService.RetrieveMultiple(query);

            if (entityCollection.Entities.Count == 0)
            {
                throw new InvalidDataException("No contract template records exist.");
            }

            return entityCollection.Entities.First().ToEntityReference();
        }
    }
}