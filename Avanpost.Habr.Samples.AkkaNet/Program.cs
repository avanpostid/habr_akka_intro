using Akka.Actor;
using Akka.Configuration;
using Avanpost.Habr.Samples.AkkaNet;

using var system = ActorSystem.Create("BikeSystem", Config.Empty);
        
var bikeActor = system.ActorOf(Props.Create(() => new BikeActor("Bike1")), "bike");

Console.WriteLine("Симулятор велосипеда на Akka.NET");
Console.WriteLine("Команды: a - ускориться, b - тормозить, s - статус, gu - повысить передачу, gd -понизить передачу, q - выход");

while (true)
{
    var input = Console.ReadLine()?.ToLower();

    switch (input)
    {
        case "a":
            bikeActor.Tell(new AccelerateMessage(5));
            Console.WriteLine("Ускорились на 5 км/ч");
            break;
        case "b":
            bikeActor.Tell(new BrakeMessage(5));
            Console.WriteLine("Затормозили на 5 км/ч");
            break;
        case "gu":
            bikeActor.Tell(new GearChangeMessage(GearChangeAction.Up));
            Console.WriteLine("Повысили передачу на 1");
            break;
        case "gd":
            bikeActor.Tell(new GearChangeMessage(GearChangeAction.Down));
            Console.WriteLine("Понизили передачу на 1");
            break;
        case "s":
            var status = await bikeActor.Ask<BicycleStatus>(StatusMessage.Instance);
            Console.WriteLine($"Велосипед {status.BikeId}: " +
                              $"Скорость: {status.Speed} км/ч, " +
                              $"Передача: {status.Gear}, " +
                              $"Состояние: {(status.IsMoving ? "движется" : "стоит")}");
            break;
        case "q":
            await system.Terminate();
            return;
        default:
            Console.WriteLine("Неизвестная команда");
            break;
    }
}