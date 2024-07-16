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
    /// Логика взаимодействия для RamCreate.xaml
    /// </summary>
    public partial class RamCreate : Window
    {
        bool isChange = false;
        Ram changedRam;

        public RamCreate()
        {
            InitializeComponent();
        }
        public RamCreate(Ram ram)
        {
            InitializeComponent();
            isChange = true;
            changedRam = ram;

            Model.Text = ram.Model;
            MemorySize.Text = ram.MemorySize.ToString();
            Year.Text = ram.Year.ToString();
            MemoryType.Text = ram.MemoryType.ToString();
            ClockSpeed.Text = ram.ClockSpeed.ToString();
            Status.Text = ram.Status.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isChange)
            {
                UpdateRam();
            }
            else
            {
                SaveRam();
            }


            RamViewModel DataContext = new RamViewModel();
            DataContext.LoadData();

            // Открываем окно со списком клиентов после успешного добавления
            RamWindow ramWindow = new RamWindow();
            ramWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }

        private void SaveRam()
        {
            Ram newRam = new Ram
            {

                Model = Model.Text,
                MemorySize = Int32.Parse(MemorySize.Text),
                Year = Int32.Parse(Year.Text),
                MemoryType = MemoryType.Text,
                ClockSpeed = Int32.Parse(ClockSpeed.Text),
                Status = Status.Text
            };

            using (var dbContext = new AppDbContext())
            {
                dbContext.Rams.Add(newRam);
                dbContext.SaveChanges();

            }

            MessageBox.Show("ОЗУ успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void UpdateRam()
        {
            changedRam.Model = Model.Text;
            changedRam.MemorySize = Int32.Parse(MemorySize.Text);
            changedRam.Year = Int32.Parse(Year.Text);
            changedRam.MemoryType = MemoryType.Text;
            changedRam.ClockSpeed = Int32.Parse(ClockSpeed.Text);
            changedRam.Status = Status.Text;

            // Добавляем нового клиента в базу данных
            using (var dbContext = new AppDbContext())
            {
                dbContext.Rams.Update(changedRam);
                dbContext.SaveChanges();
            }

            MessageBox.Show("ОЗУ успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
