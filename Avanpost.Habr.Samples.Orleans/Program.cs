using Avanpost.Habr.Samples.Orleans;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .UseOrleans(builder =>
    {
        builder.UseLocalhostClustering();
        
    })
    .Build();

await host.StartAsync();

var client = host.Services.GetRequiredService<IClusterClient>();

var bike = client.GetGrain<IBicycleGrain>("Bike1");

Console.WriteLine("Симулятор велосипеда на Orleans");
Console.WriteLine("Команды: a - ускориться, gu - повысить передачу, gd -понизить передачу, b - тормозить, s - статус, r - сброс, q - выход");

while (true)
{
    var input = Console.ReadLine()?.ToLower();
    
    switch (input)
    {
        case "a":
            await bike.Accelerate(5);
            Console.WriteLine("Ускорились на 5 км/ч");
            break;
        case "b":
            await bike.Brake(5);
            Console.WriteLine("Затормозили на 5 км/ч");
            break;
        case "gu":
            await bike.GearUp();
            Console.WriteLine("Повысили передачу на 1");
            break;
        case "gd":
            await bike.GearDown();
            Console.WriteLine("Понизили передачу на 1");
            break;
        case "s":
            var status = await bike.GetStatus();
            Console.WriteLine(status);
            break;
        case "q":
            await host.StopAsync();
            return;
        default:
            Console.WriteLine("Неизвестная команда");
            break;
    }
}