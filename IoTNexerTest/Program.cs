using IoTNexerTest.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(worker =>
    {
        worker.UseNewtonsoftJson();
    })
    .ConfigureServices(services =>
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigurationManager configuration = builder.Configuration;
        IWebHostEnvironment environment = builder.Environment;
        var serviceProvider = services.BuildServiceProvider();
        builder.Configuration.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true);
        IoCExtensions.ConfigureServices(services);
        IoCMapper.ConfigureServices(services);
    })
    .ConfigureOpenApi()
    .Build();
host.Run();
