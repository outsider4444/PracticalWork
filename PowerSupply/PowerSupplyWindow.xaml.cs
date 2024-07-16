using PracticalWork.Models;
using PracticalWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticalWork
{
    /// <summary>
    /// Логика взаимодействия для PowerSupplyWindow.xaml
    /// </summary>
    public partial class PowerSupplyWindow : Window
    {
        List<PowerSupply> powerSupplies;
        public PowerSupplyWindow()
        {
            InitializeComponent();

            InitializeComponent();
            PowerSupplyViewModel powerSupplyViewModel = new PowerSupplyViewModel();
            powerSupplies = powerSupplyViewModel.PowerSupplies.ToList();
            PowerSupplyDataGrid.ItemsSource = powerSupplies;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PowerSupplyCreate powerSupplyCreate = new PowerSupplyCreate();
            powerSupplyCreate.Show();
            this.Close();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerSupplyViewModel DataContext = new PowerSupplyViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedPowerSupply = (PowerSupply)PowerSupplyDataGrid.SelectedItem;

            if (selectedPowerSupply != null)
            {
                // Удаляем запись из базы данных
                DataContext.Delete(selectedPowerSupply);
                MessageBox.Show("Блок питания успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                PowerSupplyDataGrid.ItemsSource = DataContext.PowerSupplies;
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerSupplyViewModel DataContext = new PowerSupplyViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedPowerSupply = PowerSupplyDataGrid.SelectedItem as PowerSupply;

            if (selectedPowerSupply != null)
            {
                PowerSupply powerSupply = DataContext.GetById(selectedPowerSupply.Id);
                PowerSupplyCreate powerSupplyUpdate = new PowerSupplyCreate(powerSupply);
                powerSupplyUpdate.Show();
                this.Close();
            }
        }

        private void PowerSupplyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PowerSupplyDataGrid.SelectedItem != null)
            {
                var selectedProduct = PowerSupplyDataGrid.SelectedItem as PowerSupply;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ProcessorWindow processorWindow = new ProcessorWindow();
            processorWindow.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GraphicsCardWindow graphicsCardWindow = new GraphicsCardWindow();
            graphicsCardWindow.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            HardDriveWindow hardwareDriveWindow = new HardDriveWindow();
            hardwareDriveWindow.Show();
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            RamWindow ramWindow = new RamWindow();
            ramWindow.Show();
            this.Close();
        }
    }
}
