using PracticalWork.Data;
using PracticalWork.Models;
using PracticalWork.ViewModels;
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
        List<Computer> computers;
        public MainWindow()
        {
            InitializeComponent();

            ComputerViewModel computerViewModel = new ComputerViewModel();
            computers = computerViewModel.Computers.ToList();
            ComputerDataGrid.ItemsSource = computers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessorWindow processor = new ProcessorWindow();
            processor.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GraphicsCardWindow graphicCardWindow = new GraphicsCardWindow();
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

        private void ComputerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComputerDataGrid.SelectedItem != null)
            {
                var selectedProduct = ComputerDataGrid.SelectedItem as Computer;
                if (selectedProduct != null)
                {
                    UpdateBtn.IsEnabled = true;
                    DeleteBtn.IsEnabled = true;
                }
                else
                {
                    UpdateBtn.IsEnabled = false;
                    DeleteBtn.IsEnabled = false;
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            ComputerViewModel DataContext = new ComputerViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedComputer = (Computer)ComputerDataGrid.SelectedItem;

            if (selectedComputer != null)
            {
                // Удаляем запись из базы данных
                DataContext.Delete(selectedComputer);
                MessageBox.Show("Компьютер успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                ComputerDataGrid.ItemsSource = DataContext.Computers;
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            ComputerViewModel DataContext = new ComputerViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedComputer = ComputerDataGrid.SelectedItem as Computer;

            if (selectedComputer != null)
            {
                Computer comp = DataContext.GetById(selectedComputer.Id);
                ComputerCreate compUpdate = new ComputerCreate(comp);
                compUpdate.Show();
                this.Close();
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ComputerCreate compCreate = new ComputerCreate();
            compCreate.Show();
            this.Close();
        }
    }
}