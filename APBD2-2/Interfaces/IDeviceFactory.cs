namespace APBD2;

/// <summary>
/// Responsible for creating Device instances based on raw data.
/// </summary>
public interface IDeviceFactory
{
    object CreateDevice(string line);
}