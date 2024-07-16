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
        List<Processor> processors;
        public ProcessorWindow()
        {
            InitializeComponent();
            ProcessorViewModel processorViewModel = new ProcessorViewModel();
            processors = processorViewModel.Processors.ToList();
            ProcessorDataGrid.ItemsSource = processors;
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
            ProcessorViewModel DataContext = new ProcessorViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedProcessor = (Processor)ProcessorDataGrid.SelectedItem;

            if (selectedProcessor != null)
            {
                // Удаляем запись из базы данных
                DataContext.Delete(selectedProcessor);
                MessageBox.Show("Процессор успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                ProcessorDataGrid.ItemsSource = DataContext.Processors;
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessorViewModel DataContext = new ProcessorViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedProcessor = ProcessorDataGrid.SelectedItem as Processor;

            if (selectedProcessor != null)
            {
                Processor processor = DataContext.GetById(selectedProcessor.Id);
                ProcessorCreate processorUpdate = new ProcessorCreate(processor);
                processorUpdate.Show();
                this.Close();
            }
        }
    }
}
