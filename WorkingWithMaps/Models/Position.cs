using System.ComponentModel;

namespace WorkingWithMaps.Models
{
    public class Position : INotifyPropertyChanged
    {
       private Location _location;

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

        public Position()
        {
            LocationINPC = new Location();
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}