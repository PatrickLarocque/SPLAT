using SPLAT.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPLAT.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        public RelayCommand DashboardViewCommand { get; set; }
        public RelayCommand TicketsViewCommand { get; set; }
        public DashboardViewModel DashboardViewModel { get; set; }

        public TicketsViewModel TicketsViewModel { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            DashboardViewModel = new DashboardViewModel();
            TicketsViewModel = new TicketsViewModel();
            CurrentView = DashboardViewModel;

            DashboardViewCommand = new RelayCommand(o =>
            {
                CurrentView = DashboardViewModel;
            });

            TicketsViewCommand = new RelayCommand(o =>
            {
                CurrentView = TicketsViewModel;
            });

        }
    }
}
