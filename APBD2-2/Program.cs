using System;
namespace APBD2;

class Program
{
    static void Main()
    {
        try
        {
            IDeviceMaker factory = new DeviceMaker();
            IDeviceLoader loader = new FileDeviceLoader("info/input.txt", factory);
            IDeviceSaver saver = new FileDeviceSaver("info/input.txt");

            DeviceManager manager = new DeviceManager(loader);

            Console.WriteLine("All devices after loading from file:");
            manager.ShowDevices();

            Console.WriteLine("\nRemoving Apple Watch SE2");
            manager.RemoveDevice(1); 

            Console.WriteLine("\nAdding new devices");
            PersonalComputer pc = new PersonalComputer
            {
                Id = 3, Name = "Lenovo slim", OperatingSystem = "windows"
            };

            Smartwatch sw = new Smartwatch
            {
                Id = 2, Name = "Xiaomi band", Battery = 88
            };

            manager.AddDevice(pc);
            manager.AddDevice(sw);

            Console.WriteLine("\nEditing device data");
            manager.EditDeviceData(2, "IsTurnedOn", false);  

            Console.WriteLine("\nAll devices after modifications:");
            manager.ShowDevices();

            Console.WriteLine("\nTurning on devices");
            manager.TurnOnDevice(2); 
            manager.TurnOnDevice(3); 
            manager.TurnOnDevice(4); 
            manager.TurnOnDevice(5); 

            Console.WriteLine("\nSaving devices");
            saver.SaveDevices(new List<object>(manager.GetAllDevices()));
 

            Console.WriteLine("\nFinal list of devices:");
            manager.ShowDevices();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}