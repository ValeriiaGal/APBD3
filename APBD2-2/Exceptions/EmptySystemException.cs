namespace APBD2;
/// <summary>
/// Personal computer does not have an operating system 
/// </summary>
class EmptySystemException : Exception
{
    public EmptySystemException(string message) : base(message) {}
}