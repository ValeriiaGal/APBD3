namespace APBD2;

class EmptyBatteryException : Exception
{
    public EmptyBatteryException(string message) : base(message) {}
}