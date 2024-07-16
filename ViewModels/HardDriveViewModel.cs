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
    public class HardDriveViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<HardDrive> _hardDrives;
        public ObservableCollection<HardDrive> HardDrive
        {
            get { return _hardDrives; }
            set
            {
                _hardDrives = value;
                OnPropertyChanged(nameof(HardDrive));
            }
        }

        public HardDriveViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                HardDrive = new ObservableCollection<HardDrive>(dbContext.HardDrives.ToList());
            }
        }

        public HardDrive GetById(int id)
        {
            return HardDrive.FirstOrDefault(powerSupply => powerSupply.Id == id);
        }

        public void Delete(HardDrive powerSupply)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.HardDrives.Remove(powerSupply);
                dbContext.SaveChanges();
                HardDrive.Remove(powerSupply); // Обновляем отображаемую коллекцию
            }
        }

        public void Update(HardDrive powerSupply)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.HardDrives.Update(powerSupply);
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
