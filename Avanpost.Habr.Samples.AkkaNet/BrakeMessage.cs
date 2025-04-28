namespace Avanpost.Habr.Samples.AkkaNet;

public class BrakeMessage(int decrement)
{
    public int Decrement { get; } = decrement;
}