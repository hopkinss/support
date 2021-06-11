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
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        private MountainBikes selectedBike;

        public MainWindow()
        {
            InitializeComponent();
            MountainBikes = new ObservableCollection<MountainBikes>();
            DataContext = this;
            load();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<MountainBikes> MountainBikes { get; set; }
        public MountainBikes SelectedBike
        {
            get { return selectedBike; }
            set
            {
                selectedBike = value;
                OnPropertyChanged();
            }
        }

        public void load()
        {
            using (var context = new MTBContext())
            {
                foreach(var mtb in context.MountainBikes)
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
