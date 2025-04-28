using Proto;

namespace Avanpost.Habr.Samples.ProtoActor;

public class BicycleActor : IActor
{
    private readonly string _bikeId;
    private int _speed = 0;
    private int _gear = 1;
    private bool _isMoving = false;

    public BicycleActor(string bikeId)
    {
        _bikeId = bikeId;
    }

    public async Task ReceiveAsync(IContext context)
    {
        switch (context.Message)
        {
            case AccelerateMessage msg:
            {
                _speed += msg.Increment;
                _isMoving = _speed > 0;
                break;
            }

            case BrakeMessage msg:
            {
                _speed = Math.Max(0, _speed - msg.Decrement);
                _isMoving = _speed > 0;
                break;
            }

            case StatusMessage:
            {
                context.Respond(new BicycleStatus(
                    _bikeId,
                    _speed,
                    _gear,
                    _isMoving));
                break;
            }
            case GearChangeMessage msg:
            {
                switch (msg.Action)
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

                break;
            }
        }
    }
}