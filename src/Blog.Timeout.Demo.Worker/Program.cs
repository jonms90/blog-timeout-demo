using Blog.Timeout.Demo.Worker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient<IDefaultService, DefaultService>(client =>
        {
            client.BaseAddress = new Uri(hostContext.Configuration["BaseUrl"]);
        });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
