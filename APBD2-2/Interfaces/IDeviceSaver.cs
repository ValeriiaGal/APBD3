namespace APBD2;

/// <summary>
/// Saves devices to a target.
/// </summary>
public interface IDeviceSaver
{
    void SaveDevices(List<object> devices);
}