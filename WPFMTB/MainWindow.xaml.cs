using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFMTB.Domain;

namespace WPFMTB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private MountainBikes selectedBike;

        public MainWindow()
        {
            InitializeComponent();
            MountainBikes = new ObservableCollection<MountainBikes>();
            DataContext = this;
            AddCommand = new DelegateCommand(OnAddExecute);
            SaveCommand = new DelegateCommand(OnSaveExecute, CanSaveExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, CanDeleteExecute);
            Load();
        }

        private bool CanDeleteExecute()
        {
            if (SelectedBike != null)
            {
                return SelectedBike.Id > 0;
            }
            return false;
        }

        private void OnDeleteExecute()
        {
            if (MessageBox.Show($"Are you sure you want to delete {SelectedBike.BrandName}", "Confirm delete", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                return;

            using(var c = new MTBContext())
            {
                var entity = c.MountainBikes.FirstOrDefault(x => x.Id== SelectedBike.Id);
                c.MountainBikes.Remove(entity);
                c.SaveChanges();
                Load();
            }
        }

        private bool CanSaveExecute()
        {
            return SelectedBike != null;
        }

        private void OnSaveExecute()
        {
            using (var c = new MTBContext())
            {
                if (SelectedBike.Id == 0)
                {
                    c.MountainBikes.Add(SelectedBike);
                }
                else
                {
                    var entity = c.MountainBikes.FirstOrDefault(x => x.Id == SelectedBike.Id);
                    entity.BrandName = SelectedBike.BrandName;
                    entity.FrameMaterial = SelectedBike.FrameMaterial;
                    entity.FrameSize = SelectedBike.FrameSize;
                    entity.TireSize = SelectedBike.TireSize;
                    entity.HasSuspension = SelectedBike.HasSuspension;
                }

                c.SaveChanges();

            }
        }

        private void OnAddExecute()
        {
            var bike = new MountainBikes() { BrandName = "--New--" };
            MountainBikes.Add(bike);
            SelectedBike = bike;
        }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<MountainBikes> MountainBikes { get; set; }
        public MountainBikes SelectedBike
        {
            get { return selectedBike; }
            set
            {
                selectedBike = value;
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }

        public void Load()
        {
            MountainBikes.Clear();
            using (var context = new MTBContext())
            {
                foreach (var mtb in context.MountainBikes)
                {
                    MountainBikes.Add(mtb);
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

}
