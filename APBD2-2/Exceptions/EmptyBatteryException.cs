namespace APBD2;
/// <summary>
/// Smartwatch battery is too low 
/// </summary>
class EmptyBatteryException : Exception
{
    public EmptyBatteryException(string message) : base(message) {}
}