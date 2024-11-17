using System.ComponentModel;

namespace WorkingWithMaps.Models
{
    public class Position : INotifyPropertyChanged
    {
        Location _location;

        public Location LocationINPC
        {
            get => _location;
            set
            {
                if (LocationINPC == null || !_location.Equals(value))
                {
                    _location = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
                }
            }
        }

        public Position(Location location)
        { 
            LocationINPC = location;
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}