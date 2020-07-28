using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace commuteFormGen {
    public class ViewModel : INotifyPropertyChanged {
        public ObservableCollection<Trip> Trips_left { get; set; } = new ObservableCollection<Trip>();
        public ObservableCollection<Trip> Trips_right { get; set; } = new ObservableCollection<Trip>();

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private Trip _selectedTrip_left;
        private Trip _selectedTrip_right;
        public Trip SelectedTrip_left {
            get => _selectedTrip_left;
            set {
                if (value == _selectedTrip_left) return;
                _selectedTrip_left = value;
                if (value != null) SelectedTrip_right = null;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(SelectedTrip_right));
            }
        }

        public Trip SelectedTrip_right {
            get => _selectedTrip_right;
            set {
                if (value == _selectedTrip_right) return;
                _selectedTrip_right = value;
                if (value != null) SelectedTrip_left = null;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(SelectedTrip_left));
            }
        }
    }
}
