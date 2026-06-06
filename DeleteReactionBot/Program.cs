using DeleteReactionBot.Extensions;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) => services.RegisterAppDependencies(context.Configuration))
    .Build();
    
await host.RunAsync();