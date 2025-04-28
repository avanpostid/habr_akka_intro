using Avanpost.Habr.Samples.ProtoActor;
using Proto;
using Proto.Cluster;

var system = new ActorSystem(new ActorSystemConfig());

var props = Props.FromProducer(() => new BicycleActor("Bike1"));
var bikePid = system.Root.Spawn(props);

Console.WriteLine("Симулятор велосипеда на Proto.Actor");
Console.WriteLine("Команды: a - ускориться, gu - повысить передачу, gd -понизить передачу, b - тормозить, s - статус, r - сброс, q - выход");

while (true)
{
    var input = Console.ReadLine()?.ToLower();
    
    switch (input)
    {
        case "a":
            system.Root.Send(bikePid, new AccelerateMessage(5));
            Console.WriteLine("Ускорились на 5 км/ч");
            break;
        case "b":
            system.Root.Send(bikePid, new BrakeMessage(5));
            Console.WriteLine("Затормозили на 5 км/ч");
            break;
        case "gu":
            system.Root.Send(bikePid, new GearChangeMessage(GearChangeAction.Up));
            Console.WriteLine("Повысили передачу на 1");
            break;
        case "gd":
            system.Root.Send(bikePid, new GearChangeMessage(GearChangeAction.Down));
            Console.WriteLine("Понизили передачу на 1");
            break;
        case "s":
            var status = await system.Root.RequestAsync<BicycleStatus>(bikePid, StatusMessage.Instance);
            Console.WriteLine($"Велосипед {status.BikeId}: " +
                              $"Скорость: {status.Speed} км/ч, " +
                              $"Передача: {status.Gear}, " +
                              $"Состояние: {(status.IsMoving ? "движется" : "стоит")}");
            break;
        case "q":
            await system.Cluster().ShutdownAsync();
            return;
        default:
            Console.WriteLine("Неизвестная команда");
            break;
    }
}