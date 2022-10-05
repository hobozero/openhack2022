using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenHack2022.Application;
using OpenHack2022.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;


[assembly: FunctionsStartup(typeof(OpenHack2022.Startup))]

namespace OpenHack2022
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            //TODO: Get Connection string and creds from key store
            var connectionString = string.Empty;

            builder.Services.AddSingleton<IRatingRepository>((s) => {
                return new RatingRepository(connectionString);
            });

            builder.Services.AddTransient<RatingService>();

        }
    }
}
