namespace APBD2;

class PersonalComputer : Device
{
    public string OperatingSystem { get; set; }
    public override void TurnOn()
    {
        if (string.IsNullOrEmpty(OperatingSystem))
            throw new Exception("No OS installed.");
        base.TurnOn();
    }
    public override string ToString()
    {
        return base.ToString() + $", OS={(string.IsNullOrEmpty(OperatingSystem) ? "Not installed" : OperatingSystem)}";
    }
}