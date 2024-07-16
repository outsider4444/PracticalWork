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
    public class RamViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Ram> _rams;
        public ObservableCollection<Ram> Rams
        {
            get { return _rams; }
            set
            {
                _rams = value;
                OnPropertyChanged(nameof(Rams));
            }
        }

        public RamViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                Rams = new ObservableCollection<Ram>(dbContext.Rams.ToList());
            }
        }

        public Ram GetById(int id)
        {
            return Rams.FirstOrDefault(ram => ram.Id == id);
        }

        public void Delete(Ram ram)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Rams.Remove(ram);
                dbContext.SaveChanges();
                Rams.Remove(ram); // Обновляем отображаемую коллекцию
            }
        }

        public void Update(Ram ram)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Rams.Update(ram);
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
