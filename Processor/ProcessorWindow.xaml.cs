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
    /// Логика взаимодействия для ProcessorWindow.xaml
    /// </summary>
    public partial class ProcessorWindow : Window
    {
        public ProcessorWindow()
        {
            InitializeComponent();
        }

        private void ProcessorDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProcessorDataGrid.SelectedItem != null)
            {
                var selectedProduct = ProcessorDataGrid.SelectedItem as Processor;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessorCreate processorCreate = new ProcessorCreate();
            processorCreate.Show();
            this.Close();
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
    }
}
