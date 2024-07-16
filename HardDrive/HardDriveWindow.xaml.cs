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
    /// Логика взаимодействия для HardDriveWindow.xaml
    /// </summary>
    public partial class HardDriveWindow : Window
    {
        List<HardDrive> hardDrives;
        public HardDriveWindow()
        {
            InitializeComponent();
            HardDriveViewModel hardDriveViewModel = new HardDriveViewModel();
            hardDrives = hardDriveViewModel.HardDrives.ToList();
            HardDriveDataGrid.ItemsSource = hardDrives;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HardDriveCreate hardDriveCreate = new HardDriveCreate();
            hardDriveCreate.Show();
            this.Close();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            HardDriveViewModel DataContext = new HardDriveViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedHardDrive = (HardDrive)HardDriveDataGrid.SelectedItem;

            if (selectedHardDrive != null)
            {
                // Удаляем запись из базы данных
                DataContext.Delete(selectedHardDrive);
                MessageBox.Show("Жесткий диск успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                HardDriveDataGrid.ItemsSource = DataContext.HardDrives;
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            HardDriveViewModel DataContext = new HardDriveViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedHardDrive = HardDriveDataGrid.SelectedItem as HardDrive;

            if (selectedHardDrive != null)
            {
                HardDrive hardDrive = DataContext.GetById(selectedHardDrive.Id);
                HardDriveCreate hardDriveUpdate = new HardDriveCreate(hardDrive);
                hardDriveUpdate.Show();
                this.Close();
            }
        }

        private void HardDriveDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HardDriveDataGrid.SelectedItem != null)
            {
                var selectedProduct = HardDriveDataGrid.SelectedItem as HardDrive;
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
    }
}
