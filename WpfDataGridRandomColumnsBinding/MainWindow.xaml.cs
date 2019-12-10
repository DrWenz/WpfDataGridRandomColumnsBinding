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

namespace WpfDataGridRandomColumnsBinding
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<DataGridColumn> PropRandomColumns;
        private ObservableCollection<TestClass> PropTestDataSource;
        private Random rnd;


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<DataGridColumn> RandomColumns
        {
            get => PropRandomColumns;
            set
            {
                PropRandomColumns = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<TestClass> TestDataSource
        {
            get => PropTestDataSource;
            set
            {
                PropTestDataSource = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CreateRandomColumnsCommand => new RelayCommand(CreateRandomColumns);

        public MainWindow()
        {
            InitializeComponent();

            rnd = new Random();


            this.DataContext = this;
        }

        private void CreateRandomColumns()
        {
            ObservableCollection<TestClass> data = new ObservableCollection<TestClass>();
            for (int i = 0; i < rnd.Next(20,30); i++)
            {
                data.Add(new TestClass("hallo " + i, "irgendwas" + i, 
                    new List<SubTestClass>()
                    {
                        new SubTestClass("ListProperty 1", "ajasjdsdjd"),
                        new SubTestClass("ListProperty 2", "2424324"),
                        new SubTestClass("ListProperty 3", "1x3x2x/5"),
                    }));
            }

            ObservableCollection<DataGridColumn> columns = new ObservableCollection<DataGridColumn>();
            for (int i = 0; i < data[0].Property3.Count; i++)
            {
                columns.Add(new DataGridTextColumn()
                {
                    Header = data[0].Property3[i].Name,
                    Binding = new Binding(String.Format("Property3[{0}].Value",i))
                });
            }


            RandomColumns = columns;
            TestDataSource = data;
        }
    }
}
