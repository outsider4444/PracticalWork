using PracticalWork.Data;
using PracticalWork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalWork.ViewModels
{
    public class PowerSupplyViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PowerSupply> _powerSupplies;
        public ObservableCollection<PowerSupply> PowerSupplies
        {
            get { return _powerSupplies; }
            set
            {
                _powerSupplies = value;
                OnPropertyChanged(nameof(PowerSupplies));
            }
        }

        public PowerSupplyViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                PowerSupplies = new ObservableCollection<PowerSupply>(dbContext.PowerSupplies.ToList());
            }
        }

        public PowerSupply GetById(int id)
        {
            return PowerSupplies.FirstOrDefault(powerSupply => powerSupply.Id == id);
        }

        public void Delete(PowerSupply powerSupply)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.PowerSupplies.Remove(powerSupply);
                dbContext.SaveChanges();
                PowerSupplies.Remove(powerSupply); // Обновляем отображаемую коллекцию
            }
        }

        public void Update(PowerSupply powerSupply)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.PowerSupplies.Update(powerSupply);
                dbContext.SaveChanges();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
