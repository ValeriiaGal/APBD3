using Xunit.Abstractions;

namespace Tests;
using APBD2;
using Xunit;

public class DeviceManagerTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DeviceManagerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void addDevice()
    {
        var absolutePath = @"APBD2-2\APBD2-2\input.txt";
        if (!File.Exists(absolutePath))
        {
            throw new FileNotFoundException($"The file was not found: {absolutePath}");
        }

        var manager = new DeviceManager(absolutePath);
        var device = new Smartwatch { Id = 1, Name = "Test Smartwatch", Battery = 50 };

        manager.AddDevice(device);

        manager.ShowDevices();
    }
    
    
    [Fact]
    public void removeDevice()
    {

        var manager = new DeviceManager(@"APBD2-2\APBD2-2\input.txt");
        var device = new Smartwatch { Id = 1, Name = "Test Smartwatch", Battery = 50 };
        manager.AddDevice(device);
        
        manager.RemoveDevice(1);
        
        manager.ShowDevices();  
    }
    
    [Fact]
    public void turnOnDevice()
    {
        var manager = new DeviceManager(@"APBD2-2\APBD2-2\input.txt");
        var device = new Smartwatch { Id = 1, Name = "Test Smartwatch", Battery = 50 };
        manager.AddDevice(device);
        
        manager.TurnOnDevice(1);
        
        var deviceAfterTurnOn = manager.GetDeviceById(1);  
        Assert.True(deviceAfterTurnOn.IsTurnedOn, "Device should be turned on.");
    }
    
    [Fact]
    public void editedDevice()
    {
        var manager = new DeviceManager(@"APBD2-2\APBD2-2\input.txt");
        var device = new Smartwatch { Id = 1, Name = "Test Smartwatch", Battery = 50 };
        manager.AddDevice(device);
        
        manager.EditDeviceData(1, "Battery", 80);
        
        var updatedDevice = (Smartwatch)manager.GetDeviceById(1);
        Assert.Equal(80, updatedDevice.Battery);
    }
    
    [Fact]
    public void turnOffDevice()
    {
        var manager = new DeviceManager(@"APBD2-2\APBD2-2\input.txt");
        var device = new Smartwatch { Id = 1, Name = "Test Smartwatch", Battery = 50 };
        manager.AddDevice(device);
        manager.TurnOnDevice(1);  
        
        
        manager.TurnOffDevice(1);
        
        var deviceAfterTurnOff = manager.GetDeviceById(1);
        Assert.False(deviceAfterTurnOff.IsTurnedOn, "Device should be turned off.");
    }
}