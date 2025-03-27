

namespace APBD2
{
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

        public void AddDevice(Device device)
        {
            if (_devices.Count >= MaxDevices)
                throw new Exception("Storage limit reached.");

            device.Id = _devices.Count + 1;
            _devices.Add(device);
        }
        
        public Device GetDeviceById(int id)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
            if (device == null)
                throw new Exception($"Device with Id {id} not found.");
            return device;
        }


        public void RemoveDevice(int id) => _devices.RemoveAll(d => d.Id == id);

        public void EditDeviceData(int id, string propertyName, object newValue)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
            if (device == null) return;

            if (propertyName == nameof(Device.IsTurnedOn))
            {
                if (Convert.ToBoolean(newValue)) device.TurnOn();
                else device.TurnOff();
            }
            else
            {
                var prop = device.GetType().GetProperty(propertyName);
                prop?.SetValue(device, Convert.ChangeType(newValue, prop.PropertyType));
            }
        }

        public void TurnOnDevice(int id) => _devices.FirstOrDefault(d => d.Id == id)?.TurnOn();

        public void TurnOffDevice(int id) => _devices.FirstOrDefault(d => d.Id == id)?.TurnOff();

        public void ShowDevices()
        {
            foreach (var device in _devices)
                Console.WriteLine(device);
        }

        public void SaveToFile(IDeviceSaver saver)
        {
            var objList = new List<object>(_devices);
            saver.SaveDevices(objList);
        }
    }
}
 