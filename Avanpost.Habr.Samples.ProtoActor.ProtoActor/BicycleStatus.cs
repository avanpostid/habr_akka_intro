namespace Avanpost.Habr.Samples.ProtoActor;

public class BicycleStatus(string bikeId, int speed, int gear, bool isMoving)
{
    public string BikeId { get; } = bikeId;
    public int Speed { get; } = speed;
    public int Gear { get; } = gear;
    public bool IsMoving { get; } = isMoving;
}