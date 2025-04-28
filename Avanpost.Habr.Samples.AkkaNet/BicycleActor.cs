using Akka.Actor;

namespace Avanpost.Habr.Samples.AkkaNet;

public class BikeActor : ReceiveActor
{
    private readonly string _bikeId;
    private int _speed = 0;
    private int _gear = 1;
    private bool _isMoving = false;

    public BikeActor(string bikeId)
    {
        _bikeId = bikeId;

        Receive<AccelerateMessage>(msg =>
        {
            _speed += msg.Increment;
            _isMoving = _speed > 0;
        });

        Receive<BrakeMessage>(msg =>
        {
            _speed = Math.Max(0, _speed - msg.Decrement);
            _isMoving = _speed > 0;
        });

        Receive<StatusMessage>(_ =>
        {
            Sender.Tell(new BicycleStatus(
                _bikeId, 
                _speed, 
                _gear, 
                _isMoving));
        });

        Receive<GearChangeMessage>(_ =>
        {
            switch (_.Action)
            {
                case GearChangeAction.Down:
                {
                    _gear--;
                    break;
                }
                case GearChangeAction.Up:
                {
                    _gear++;
                    break;
                }
            }
            
        });
    }
}