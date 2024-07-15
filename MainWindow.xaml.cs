using PracticalWork.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticalWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                var computers = context.Computers.ToList();
                InventoryDataGrid.ItemsSource = computers;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessorWindow processorWindow = new ProcessorWindow();
            processorWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GraphicCardWindow graphicCardWindow = new GraphicCardWindow();
            graphicCardWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PowerSupplyWindow power = new PowerSupplyWindow();
            power.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            HardDriveWindow hardDriveWindow = new HardDriveWindow();
            hardDriveWindow.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            RamWindow ramWindow = new RamWindow();
            ramWindow.Show();
            this.Close();
        }
    }
}