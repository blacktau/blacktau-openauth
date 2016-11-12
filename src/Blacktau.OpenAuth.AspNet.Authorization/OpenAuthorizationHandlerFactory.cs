namespace Blacktau.OpenAuth.AspNet.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.AspNet.Authorization.Interfaces.VersionTwo;
    using Blacktau.OpenAuth.Client;

    using Microsoft.Extensions.Logging;

    public class OpenAuthorizationHandlerFactory : IOpenAuthorizationHandlerFactory
    {
        private readonly Dictionary<OpenAuthVersion, Func<IOpenAuthorizationOptions, IOpenAuthorizationHandler>> handlerFactoryMethodsMap;

        private readonly ILogger<OpenAuthorizationHandlerFactory> logger;

        private readonly IOpenAuthorizationOneAHandlerFactory openAuthorizationOneAHandlerFactory;

        private readonly IOpenAuthorizationTwoHandlerFactory openAuthorizationTwoHandlerFactory;

        public OpenAuthorizationHandlerFactory(ILoggerFactory loggerFactory, IOpenAuthorizationTwoHandlerFactory openAuthorizationTwoHandlerFactory, IOpenAuthorizationOneAHandlerFactory openAuthorizationOneAHandlerFactory)
        {
            this.openAuthorizationTwoHandlerFactory = openAuthorizationTwoHandlerFactory;
            this.openAuthorizationOneAHandlerFactory = openAuthorizationOneAHandlerFactory;

            this.logger = loggerFactory.CreateLogger<OpenAuthorizationHandlerFactory>();

            this.handlerFactoryMethodsMap = new Dictionary<OpenAuthVersion, Func<IOpenAuthorizationOptions, IOpenAuthorizationHandler>>();

            this.InitialiseHandlerFactoryMap();
        }

        public IOpenAuthorizationHandler CreateHandler(IOpenAuthorizationOptions options)
        {
            if (!this.handlerFactoryMethodsMap.ContainsKey(options.OpenAuthVersion))
            {
                throw new Exception(string.Format(CultureInfo.CurrentCulture, Client.Resources.Exception_Invalid, options.OpenAuthVersion));
            }

            var handlerFactory = this.handlerFactoryMethodsMap[options.OpenAuthVersion];
            return handlerFactory.Invoke(options);
        }

        private void InitialiseHandlerFactoryMap()
        {
            this.handlerFactoryMethodsMap.Add(OpenAuthVersion.OneA, this.openAuthorizationOneAHandlerFactory.CreateHandler);
            this.handlerFactoryMethodsMap.Add(OpenAuthVersion.Two, this.openAuthorizationTwoHandlerFactory.CreateHandler);
        }
    }
}