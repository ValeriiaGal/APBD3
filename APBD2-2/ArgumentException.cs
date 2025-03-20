namespace APBD2;

class InvalidArgumentException : ArgumentException
{
    public InvalidArgumentException(string message) : base(message) {}
}