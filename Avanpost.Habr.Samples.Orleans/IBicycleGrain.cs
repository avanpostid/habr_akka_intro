namespace Avanpost.Habr.Samples.Orleans;

public interface IBicycleGrain : IGrainWithStringKey
{
    Task Accelerate(int increment);
    Task GearUp();
    Task GearDown();
    Task Brake(int decrement);
    Task<string> GetStatus();
}