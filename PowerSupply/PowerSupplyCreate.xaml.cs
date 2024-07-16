using PracticalWork.Data;
using PracticalWork.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для PoweSupplyCreate.xaml
    /// </summary>
    public partial class PowerSupplyCreate : Window
    {
        bool isChange = false;
        PowerSupply changedPowerSupply;

        public PowerSupplyCreate()
        {
            InitializeComponent();
        }

        public PowerSupplyCreate(PowerSupply powerSupply)
        {
            InitializeComponent();

            isChange = true;
            changedPowerSupply = powerSupply;

            Model.Text = changedPowerSupply.Model;
            Power.Text = changedPowerSupply.Power.ToString();
            Year.Text = changedPowerSupply.Year.ToString();
            Status.Text = changedPowerSupply.Status.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isChange)
            {
                UpdatePowerSupply();
            }
            else
            {
                SavePowerSupply();
            }
        }

        private void SavePowerSupply()
        {
            PowerSupply newPowerSupply = new PowerSupply
            {

                Model = Model.Text,
                Power = Int32.Parse(Power.Text),
                Year = Int32.Parse(Year.Text),
                Status = Status.Text
            };

            using (var dbContext = new AppDbContext())
            {
                dbContext.PowerSupplies.Add(newPowerSupply);
                dbContext.SaveChanges();

            }

            MessageBox.Show("Блок питания успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void UpdatePowerSupply()
        {
            changedPowerSupply.Model = Model.Text;
            changedPowerSupply.Power = Int32.Parse(Power.Text);
            changedPowerSupply.Year = Int32.Parse(Year.Text);
            changedPowerSupply.Status = Status.Text;

            // Добавляем нового клиента в базу данных
            using (var dbContext = new AppDbContext())
            {
                dbContext.PowerSupplies.Update(changedPowerSupply);
                dbContext.SaveChanges();
            }

            MessageBox.Show("Блок питания успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PowerSupplyWindow powerWindow = new PowerSupplyWindow();
            powerWindow.Show();
            this.Close();
        }
    }
}
