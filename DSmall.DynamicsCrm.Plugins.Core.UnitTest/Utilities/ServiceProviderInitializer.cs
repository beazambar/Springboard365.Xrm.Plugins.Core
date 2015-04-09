﻿namespace DSmall.DynamicsCrm.Plugins.Core.UnitTest
{
    using System;
    using Microsoft.Xrm.Sdk;
    using Moq;

    /// <summary>The service provider initializer.</summary>
    public class ServiceProviderInitializer
    {
        /// <summary>The setup.</summary>
        /// <returns>The <see cref="IServiceProvider"/>.</returns>
        public Mock<IServiceProvider> Setup()
        {
            var mockPluginContext = SetupPluginContext();
            var mockOrganizationServiceFactory = SetupOrganizationServiceFactory();
            var mockTracingService = new Mock<ITracingService>();

            return SetupServiceProvider(mockPluginContext, mockOrganizationServiceFactory, mockTracingService);
        }

        /// <summary>The setup organization service factory.</summary>
        /// <returns>The <see cref="Mock"/>.</returns>
        public Mock<IOrganizationServiceFactory> SetupOrganizationServiceFactory()
        {
            var mockOrganizationService = new Mock<IOrganizationService>();
            var mockOrganizationServiceFactory = new Mock<IOrganizationServiceFactory>();

            mockOrganizationServiceFactory
                .Setup(factory => factory.CreateOrganizationService(It.IsAny<Guid?>()))
                .Returns(mockOrganizationService.Object);

            return mockOrganizationServiceFactory;
        }

        /// <summary>The setup plugin context.</summary>
        /// <returns>The <see cref="Mock"/>.</returns>
        public Mock<IPluginExecutionContext> SetupPluginContext()
        {
            var mockPluginContext = new Mock<IPluginExecutionContext>();
            mockPluginContext
                .Setup(context => context.UserId)
                .Returns(Guid.NewGuid);

            return mockPluginContext;
        }

        private static Mock<IServiceProvider> SetupServiceProvider(Mock<IPluginExecutionContext> mockPluginContext, Mock<IOrganizationServiceFactory> mockOrganizationServiceFactory, Mock<ITracingService> mockTracingService)
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(provider => provider.GetService(typeof(IPluginExecutionContext)))
                           .Returns(mockPluginContext.Object);
            serviceProvider.Setup(provider => provider.GetService(typeof(IOrganizationServiceFactory)))
                           .Returns(mockOrganizationServiceFactory.Object);
            serviceProvider.Setup(provider => provider.GetService(typeof(ITracingService))).Returns(mockTracingService.Object);

            return serviceProvider;
        }
    }
}