namespace APBD2;

public abstract class Device
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsTurnedOn { get; private set; }

    public virtual void TurnOn()
    {
        IsTurnedOn = true;
    }

    public void TurnOff()
    {
        IsTurnedOn = false;
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}, Name={Name}, IsTurnedOn={IsTurnedOn}]";
    }
}