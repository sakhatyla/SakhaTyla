﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Cynosura.Web.Infrastructure;

namespace SakhaTyla.Web.Routes
{
    public class BookAuthorshipModule : IConfigurationModule<IEndpointRouteBuilder>
    {
        public void Configure(IEndpointRouteBuilder configuration)
        {
            configuration.MapGrpcService<Services.BookAuthorshipService>();
        }
    }
}