using PracticalWork.Data;
using PracticalWork.Models;
using PracticalWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
    /// Логика взаимодействия для HardDriveCreate.xaml
    /// </summary>
    public partial class HardDriveCreate : Window
    {
        bool isChange = false;
        HardDrive changedHardDrive;

        public HardDriveCreate()
        {
            InitializeComponent();
        }

        public HardDriveCreate(HardDrive hardDrive)
        {
            InitializeComponent();

            isChange = true;
            changedHardDrive = hardDrive;

            Model.Text = hardDrive.Model;
            Capacity.Text = hardDrive.Capacity.ToString();
            Year.Text = hardDrive.Year.ToString();
            Status.Text = hardDrive.Status.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HardDriveWindow hardDriveWindow = new HardDriveWindow();
            hardDriveWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isChange)
            {
                UpdateHardDrive();
            }
            else
            {
                SaveHardDrive();
            }
            HardDriveViewModel DataContext = new HardDriveViewModel();
            DataContext.LoadData();

            // Открываем окно со списком клиентов после успешного добавления
            HardDriveWindow ramWindow = new HardDriveWindow();
            ramWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }
        private void SaveHardDrive()
        {
            HardDrive newHardDrive = new HardDrive
            {

                Model = Model.Text,
                Capacity = Int32.Parse(Capacity.Text),
                Year = Int32.Parse(Year.Text),
                Status = Status.Text
            };

            using (var dbContext = new AppDbContext())
            {
                dbContext.HardDrives.Add(newHardDrive);
                dbContext.SaveChanges();

            }

            MessageBox.Show("Жесткий диск успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void UpdateHardDrive()
        {
            changedHardDrive.Model = Model.Text;
            changedHardDrive.Capacity = Int32.Parse(Capacity.Text);
            changedHardDrive.Year = Int32.Parse(Year.Text);
            changedHardDrive.Status = Status.Text;

            // Добавляем нового клиента в базу данных
            using (var dbContext = new AppDbContext())
            {
                dbContext.HardDrives.Update(changedHardDrive);
                dbContext.SaveChanges();
            }

            MessageBox.Show("Жесткий диск успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
