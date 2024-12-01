using ClearDrive.shared.Models;
using Microsoft.Maui.Controls.Maps;
using ClearDrive.shared.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using ClearDrive.mobil.ViewModels.Base;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using ClearDrive.shared.Responses;

namespace ClearDrive.mobil.ViewModels
{
    public partial class PinItemsSourcePageViewModel : BaseViewModelWithAsyncInitialization
    {
        private readonly IClearDriveService? _clearDriveService;

        [ObservableProperty]
        private ObservableCollection<Position> _locations = new();

        public PinItemsSourcePageViewModel()
        {
            _clearDriveService = new ClearDriveService("http://10.0.2.2:7090/");

        }

        [RelayCommand]
        public async Task DoSave(Position newPosition)
        {
            if (_clearDriveService is not null)
            {
                ControllerResponse result = await _clearDriveService.InsertAsync(newPosition);
                if (!result.HasError)
                {
                    await UpdateView();
                }
                else
                {
                    Debug.WriteLine($"{result.ToString()}");
                }
            }
            else
            {
                Debug.WriteLine($"{nameof(_clearDriveService)} is null.");
            }
        }

        [RelayCommand]
        public async Task DoUpdate(Position positionToUpdate)
        {
            if (_clearDriveService is not null)
            {

                ControllerResponse result = await _clearDriveService.UpdateAsync(positionToUpdate);

                if (!result.HasError)
                {
                    await UpdateView();
                }
                else
                {
                    Debug.WriteLine($"{result.ToString()}");
                }
            }
            else
            {
                Debug.WriteLine($"{nameof(_clearDriveService)} is null.");
            }
        }

        [RelayCommand]
        public async Task DoRemove(Position positionToDelete)
        {
            if (_clearDriveService is not null)
            {
                ControllerResponse result = await _clearDriveService.DeleteAsync(positionToDelete.Id);
                if (!result.HasError)
                {
                    await UpdateView();
                }
                else
                {
                    Debug.WriteLine($"{result.ToString()}");
                }
            }
            else
            {
                Debug.WriteLine($"{nameof(_clearDriveService)} is null.");
            }
        }

        public override async Task InitializeAsync()
        {
            await UpdateView();
        }

        public async Task UpdateView()
        {
            if (_clearDriveService is not null)
            {
                List<Position> positions = await _clearDriveService.SelectAll();
                Locations = new ObservableCollection<Position>(positions);
            }
            else
            {
                Debug.WriteLine($"{nameof(_clearDriveService)} is null.");
            }
        }

        public Pin CreatePin(Location temp)
        {
            return new Pin
            {
                Location = temp,
                Label = temp.Timestamp.Year.ToString() + "." + temp.Timestamp.Month.ToString() + "." + temp.Timestamp.Day.ToString() + "  " + temp.Timestamp.Hour.ToString() + ":" + temp.Timestamp.Minute.ToString() + ":" + temp.Timestamp.Second.ToString(),
                Address = ((float)temp.Longitude).ToString() + ", " + ((float)temp.Latitude).ToString(),
                Type = PinType.SavedPin
            };
        }

        public bool GetDistance( Location location1, Location location2)
        {
            var R = 6371; // Earth radius in km
            var dLat = (location2.Latitude - location1.Latitude) * Math.PI / 180;
            var dLon = (location2.Longitude - location1.Longitude) * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(location1.Latitude * Math.PI / 180) * Math.Cos(location2.Latitude * Math.PI / 180) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c * 1000;
            return distance<=10 ? true : false;
        }
    }
}
