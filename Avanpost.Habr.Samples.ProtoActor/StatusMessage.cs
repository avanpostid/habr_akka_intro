namespace Avanpost.Habr.Samples.ProtoActor;

public class StatusMessage
{
    public static StatusMessage Instance { get; } = new StatusMessage();

    private StatusMessage()
    {
    }
}