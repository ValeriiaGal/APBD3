namespace APBD2;

/// <summary>
/// Loads devices from a source.
/// </summary>
public interface IDeviceLoader
{
    List<object> LoadDevices();
}