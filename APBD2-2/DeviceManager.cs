

namespace APBD2
{
    public class DeviceManager
    {
        private List<Device> devices = new List<Device>();
        private const int MaxDevices = 15;

        public DeviceManager(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.");

            foreach (var line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split(',');
                if (parts.Length < 2) continue;

                try
                {
                    Device device = null;
                    string[] identifierParts = parts[0].Split('-');
                    if (identifierParts.Length < 2) continue;

                    string deviceType = identifierParts[0];
                    string deviceId = identifierParts[1];

                    switch (deviceType)
                    {
                        case "SW":
                            if (parts.Length < 4 || !int.TryParse(parts[3].TrimEnd('%'), out int battery))
                                continue;
                            device = new Smartwatch { Id = devices.Count + 1, Name = parts[1], Battery = battery };
                            break;
                        case "P":
                            device = new PersonalComputer { Id = devices.Count + 1, Name = parts[1], OperatingSystem = parts.Length > 2 ? parts[2] : null };
                            break;
                        case "ED":
                            if (parts.Length < 4) continue;
                            device = new EmbeddedDevice { Id = devices.Count + 1, Name = parts[1], IpAddress = parts[2], NetworkName = parts[3] };
                            break;
                    }
                    if (device != null && devices.Count < MaxDevices)
                        devices.Add(device);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Skipping invalid entry: {line}");
                }
            }
        }

        public void AddDevice(Device device)
        {
            if (devices.Count >= MaxDevices)
                throw new Exception("Storage limit reached.");
            devices.Add(device);
        }
        
        public Device GetDeviceById(int id)
        {
            return devices.FirstOrDefault(d => d.Id == id);
        }

        public void RemoveDevice(int id)
        {
            var deviceToRemove = devices.Find(d => d.Id == id);
            if (deviceToRemove != null)
            {
                devices.Remove(deviceToRemove);
            }
            else
            {
                Console.WriteLine($"Device with Id {id} not found.");
            }
        }

        public void EditDeviceData(int id, string propertyName, object newValue)
        {
            var device = devices.Find(d => d.Id == id);
            if (device != null)
            {
                if (propertyName == "IsTurnedOn")
                {
                    bool newState = Convert.ToBoolean(newValue);
                    if (newState)
                    {
                        device.TurnOn();
                    }
                    else
                    {
                        device.TurnOff();
                    }
                    Console.WriteLine($"Device {device.Name} turned {(newState ? "on" : "off")}");
                }
                else
                {
                    var propertyInfo = device.GetType().GetProperty(propertyName);
                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        try
                        {
                            propertyInfo.SetValue(device, Convert.ChangeType(newValue, propertyInfo.PropertyType));
                            Console.WriteLine($"Device {device.Name} property {propertyName} updated to {newValue}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error updating property: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Property {propertyName} does not exist or cannot be written.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Device with Id {id} not found.");
            }
        }


        public void TurnOnDevice(int id)
        {
            var device = devices.Find(d => d.Id == id);
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

        public void TurnOffDevice(int id)
        {
            var device = devices.Find(d => d.Id == id);
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

        public void ShowDevices()
        {
            if (devices.Count == 0)
            {
                Console.WriteLine("No devices to display.");
            }
            else
            {
                foreach (var device in devices)
                {
                    Console.WriteLine(device);
                }
            }
        }

        public void SaveToFile(string filePath)
        {
            List<string> lines = new List<string>();
            foreach (var device in devices)
            {
                lines.Add(device.ToString());
            }
            try
            {
                File.WriteAllLines(filePath, lines);
                Console.WriteLine("Devices saved to file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
    }
}
