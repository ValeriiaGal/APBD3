namespace APBD2;
/// <summary>
/// Invalid argument is provided 
/// </summary>
class InvalidArgumentException : ArgumentException
{
    public InvalidArgumentException(string message) : base(message) {}
}