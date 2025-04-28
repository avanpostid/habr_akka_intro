namespace Avanpost.Habr.Samples.ProtoActor;

public class BrakeMessage(int decrement)
{
    public int Decrement { get; } = decrement;
}