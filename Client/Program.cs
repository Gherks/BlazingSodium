using BlazingSodium.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BlazingSodium.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient<EmployeeDataServiceInterface, EmployeeDataService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)) // Local
            //builder.Services.AddHttpClient<EmployeeDataServiceInterface, EmployeeDataService>(client => client.BaseAddress = new Uri("https://blazingsodiumserver.azurewebsites.net/")) // Azure
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("api://286149a1-2fd8-4831-82a0-29bb45f01f67/API.Access");
            });

            await builder.Build().RunAsync();
        }
    }
}
