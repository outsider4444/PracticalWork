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
    /// Логика взаимодействия для ProcessorCreate.xaml
    /// </summary>
    public partial class ProcessorCreate : Window
    {
        bool isChange = false;
        Processor changedProcessor;

        public ProcessorCreate()
        {
            InitializeComponent();
        }

        public ProcessorCreate(Processor processor)
        {
            InitializeComponent();
            isChange = true;
            changedProcessor = processor;

            Model.Text = processor.Model;
            NumberOfCores.Text = processor.NumberOfCores.ToString();
            Year.Text = processor.Year.ToString();
            MemoryType.Text = processor.MemoryType.ToString();
            Frequency.Text = processor.Frequency.ToString();
            Status.Text = processor.Status.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(isChange)
            {
                UpdateProcessor();
            }
            else
            {
                SaveProcessor();
            }
            ProcessorViewModel DataContext = new ProcessorViewModel();
            DataContext.LoadData();

            // Открываем окно со списком клиентов после успешного добавления
            ProcessorWindow processorWindow = new ProcessorWindow();
            processorWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }

        private void SaveProcessor()
        {
            Processor newProcessor = new Processor
            {

                Model = Model.Text,
                NumberOfCores = Int32.Parse(NumberOfCores.Text),
                Year = Int32.Parse(Year.Text),
                MemoryType = MemoryType.Text,
                Frequency = Int32.Parse(Frequency.Text),
                Status = Status.Text
            };

            using (var dbContext = new AppDbContext())
            {
                dbContext.Processors.Add(newProcessor);
                dbContext.SaveChanges();

            }

            MessageBox.Show("Процессор успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void UpdateProcessor()
        {
            changedProcessor.Model = Model.Text;
            changedProcessor.NumberOfCores = Int32.Parse(NumberOfCores.Text);
            changedProcessor.Year = Int32.Parse(Year.Text);
            changedProcessor.MemoryType = MemoryType.Text;
            changedProcessor.Frequency = Int32.Parse(Frequency.Text);
            changedProcessor.Status = Status.Text;

            // Добавляем нового клиента в базу данных
            using (var dbContext = new AppDbContext())
            {
                dbContext.Processors.Update(changedProcessor);
                dbContext.SaveChanges();
            }

            MessageBox.Show("Процессор успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProcessorWindow processorWindow = new ProcessorWindow();
            processorWindow.Show();
            this.Close();
        }
    }
}
