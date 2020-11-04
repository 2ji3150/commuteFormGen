using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace commuteFormGen {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {



        public ViewModel VM { get; set; }
        public MainWindow() {

            InitializeComponent();
            VM = new ViewModel();
            DataContext = VM;

            if (!File.Exists("teiki.txt")) return;
            var lines = File.ReadAllLines("teiki.txt");

            foreach (var line in lines) {

                var contents = line.Split('\t');
                if (contents.Length != 7) continue;

                if (!DateTime.TryParse(contents[0], out DateTime date)) continue;
                if (contents[1] != "入" && contents[3] != "出") continue;
                if (!int.TryParse(contents[6], out int money)) continue;

                if (money >= 0) continue;
                if (date.Month != DateTime.Now.Month - 1) continue;

                string enter = contents[2];
                string leave = contents[4];
                Trip trip = new Trip() { Date = date, Enter = enter, Leave = leave, Coast = (ushort)Math.Abs(money) };

                VM.Trips_right.Add(trip);
            }
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e) {
            if (VM.SelectedTrip_left != null) {
                VM.Trips_right.Add(VM.SelectedTrip_left);
                VM.Trips_left.Remove(VM.SelectedTrip_left);
            }
            else if (VM.SelectedTrip_right != null) {
                VM.Trips_left.Add(VM.SelectedTrip_right);
                VM.Trips_right.Remove(VM.SelectedTrip_right);
            }
        }

        public void RemoveButton_Click(object sender, RoutedEventArgs e) {
            if (VM.SelectedTrip_left != null) {
                VM.Trips_left.Remove(VM.SelectedTrip_left);
            }
            else if (VM.SelectedTrip_right != null) {
                VM.Trips_right.Remove(VM.SelectedTrip_right);
            }
        }

        public void CopyButton_Click(object sender, RoutedEventArgs e) {
            IEnumerable<string> tripLines = null;
            static string func(Trip t) => string.Join('\t', t.Date, t.Enter.Trim(), t.Leave.Trim(), t.Coast);
            if (VM.SelectedTrip_left != null) tripLines = VM.Trips_left.OrderBy(trip => trip.Date).ThenBy(trip => trip.Enter).Select(func);
            else if (VM.SelectedTrip_right != null) tripLines = VM.Trips_right.OrderBy(trip => trip.Date).ThenBy(trip => trip.Enter).Select(func);
            else return;

            Clipboard.SetText(string.Join(Environment.NewLine, tripLines));
        }
    }
}