using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IServiceCenter, ServiceCenter>();
services.AddSingleton<ParkService>();
services.AddSingleton<ConsoleApp>();

using var provider = services.BuildServiceProvider();
var app = provider.GetRequiredService<ConsoleApp>();
app.Run();
