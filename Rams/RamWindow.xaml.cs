using PracticalWork;
using PracticalWork.Data;
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
    /// Логика взаимодействия для RamWindow.xaml
    /// </summary>
    public partial class RamWindow : Window
    {
        List<Ram> rams;
        public RamWindow()
        {
            InitializeComponent();
            RamViewModel ramViewModel = new RamViewModel();
            rams = ramViewModel.Rams.ToList();
            RamDataGrid.ItemsSource = rams;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RamCreate ramCreate = new RamCreate();
            ramCreate.Show();
            this.Close();
        }

        private void RamDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RamDataGrid.SelectedItem != null)
            {
                var selectedProduct = RamDataGrid.SelectedItem as Ram;
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
            RamViewModel DataContext = new RamViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedRam = (Ram)RamDataGrid.SelectedItem;

            if (selectedRam != null)
            {
                // Удаляем запись из базы данных
                DataContext.Delete(selectedRam);
                MessageBox.Show("ОЗУ успешно удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                RamDataGrid.ItemsSource = DataContext.Rams;
            }

        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            RamViewModel DataContext = new RamViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedRam = RamDataGrid.SelectedItem as Ram;

            if (selectedRam != null)
            {
                Ram ram = DataContext.GetById(selectedRam.Id);
                RamCreate ramUpdate = new RamCreate(ram);
                ramUpdate.Show();
                this.Close();
            }
        }
    }
}
