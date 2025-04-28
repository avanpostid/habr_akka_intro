namespace Avanpost.Habr.Samples.AkkaNet;

public class StatusMessage
{
    public static StatusMessage Instance { get; } = new StatusMessage();

    private StatusMessage()
    {
    }
}