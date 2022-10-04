using Microsoft.Azure.Functions.Extensions.DependencyInjection;
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
            //builder.Services.AddHttpClient();

            //builder.Services.AddTransient((s) => {
            //    return new RatingRepository();
            //});

        }
    }
}
