namespace Avanpost.Habr.Samples.ProtoActor;

public class GearChangeMessage(GearChangeAction action)
{
    public readonly GearChangeAction Action = action;
}

public enum GearChangeAction
{
    Up,
    Down
}