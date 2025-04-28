namespace Avanpost.Habr.Samples.Orleans;

public class BicycleGrain : Grain, IBicycleGrain
{
    private int _speed = 0;
    private int _gear = 1;
    private bool _isMoving = false;
    
    public async Task Accelerate(int increment)
    {
        _speed += increment;
        _isMoving = _speed > 0;
    }

    public async Task GearUp()
    {
        _gear--;
    }

    public async Task GearDown()
    {
        _gear++;
    }

    public async Task Brake(int decrement)
    {
        _speed = Math.Max(0, _speed - decrement);
        _isMoving = _speed > 0;
    }
    
    public async Task<string> GetStatus()
    {
        return ($"Велосипед {this.GetPrimaryKeyString()}: " +
                               $"Скорость: {_speed} км/ч, " +
                               $"Передача: {_gear}, " +
                               $"Состояние: {(_isMoving ? "движется" : "стоит")}");
    }
}