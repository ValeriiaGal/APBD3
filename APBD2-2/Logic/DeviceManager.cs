

namespace APBD2
{
    /// <summary>
    /// Manages devices and provides operations to modify them
    /// </summary>
    public class DeviceManager
    {
        private readonly List<Device> _devices;
        private const int MaxDevices = 15;

        public DeviceManager(IDeviceLoader loader)
        {
            _devices = new List<Device>();
            var rawDevices = loader.LoadDevices();
            foreach (var obj in rawDevices)
            {
                if (obj is Device d)
                    _devices.Add(d);
            }

            for (int i = 0; i < _devices.Count; i++)
                _devices[i].Id = i + 1;
        }
        /// <summary>
        /// Adds a device to the manager
        /// </summary>
        public void AddDevice(Device device)
        {
            if (_devices.Count >= MaxDevices)
            {
                Console.WriteLine("Storage limit reached.");
                return;
            }

            _devices.Add(device);
        }
        /// <summary>
        /// Retrieves a device by its ID.
        /// </summary>

        public Device GetDeviceById(int id)
        {
            var device = _devices.Find(d => d.Id == id);
            if (device == null)
            {
                Console.WriteLine($"Device with Id {id} not found.");
                throw new Exception("Device not found");
            }

            return device;
        }
        /// <summary>
        /// Removes a device with the ID.
        /// </summary>
        public void RemoveDevice(int id)
        {
            var deviceToRemove = _devices.Find(d => d.Id == id);
            if (deviceToRemove != null)
            {
                _devices.Remove(deviceToRemove);
            }
            else
            {
                Console.WriteLine($"Device with Id {id} not found.");
            }
        }
        /// <summary>
        /// Edits a property of some device.
        /// </summary>
         public void EditDeviceData(int id, string propertyName, object newValue)
    {
        var device = _devices.Find(d => d.Id == id);
        if (device != null)
        {
            switch (propertyName)
            {
                case "Name":
                    device.Name = newValue.ToString();
                    Console.WriteLine($"Device {device.Id} name updated to {device.Name}.");
                    break;
                case "Battery":
                    if (device is Smartwatch sw && int.TryParse(newValue.ToString(), out int battery))
                    {
                        sw.Battery = battery;
                        Console.WriteLine($"Smartwatch {sw.Id} battery updated to {sw.Battery}%.");
                    }
                    break;
                case "OperatingSystem":
                    if (device is PersonalComputer pc)
                    {
                        pc.OperatingSystem = newValue.ToString();
                        Console.WriteLine($"PC {pc.Id} OS updated to {pc.OperatingSystem}.");
                    }
                    break;
                case "IpAddress":
                    if (device is EmbeddedDevice ed)
                    {
                        ed.IpAddress = newValue.ToString();
                        Console.WriteLine($"Embedded device {ed.Id} IP updated to {ed.IpAddress}.");
                    }
                    break;
                case "NetworkName":
                    if (device is EmbeddedDevice ed2)
                    {
                        ed2.NetworkName = newValue.ToString();
                        Console.WriteLine($"Embedded device {ed2.Id} network updated to {ed2.NetworkName}.");
                    }
                    break;
                case "IsTurnedOn":
                    bool newState = Convert.ToBoolean(newValue);
                    if (newState)
                    {
                        device.TurnOn();
                    }
                    else
                    {
                        device.TurnOff();
                    }
                    Console.WriteLine($"Device {device.Name} turned {(newState ? "on" : "off")}.");
                    break;
                default:
                    Console.WriteLine($"Unknown property: {propertyName}");
                    break;
            }
        }
        else
        {
            Console.WriteLine($"Device with Id {id} not found.");
        }
    }
        
        /// <summary>
        /// Displays all devices to the console.
        /// </summary>

        public void ShowDevices()
        {
            if (_devices.Count == 0)
            {
                Console.WriteLine("No devices to display.");
            }
            else
            {
                foreach (var device in _devices)
                {
                    Console.WriteLine(device);
                }
            }
        }
        /// <summary>
        /// Returns a copy of all devices.
        /// </summary>
        
        public List<Device> GetAllDevices()
        {
            return new List<Device>(_devices);
        }
        
        /// <summary>
        /// Turns on a device with the provided ID.
        /// </summary>
        public void TurnOnDevice(int id)
        {
            var device = _devices.Find(d => d.Id == id);
            if (device != null)
            {
                try
                {
                    device.TurnOn();
                    Console.WriteLine($"Device {device.Name} is now turned on.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error turning on device: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Device with Id {id} not found.");
            }
        }

        /// <summary>
        /// Turns off a device with the provided ID.
        /// </summary>
        public void TurnOffDevice(int id)
        {
            var device = _devices.Find(d => d.Id == id);
            if (device != null)
            {
                device.TurnOff();
                Console.WriteLine($"Device {device.Name} is now turned off.");
            }
            else
            {
                Console.WriteLine($"Device with Id {id} not found.");
            }
        }
    }
}

 