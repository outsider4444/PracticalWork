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
    public class ComputerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Computer> _computers;
        public ObservableCollection<Computer> Computers
        {
            get { return _computers; }
            set
            {
                _computers = value;
                OnPropertyChanged(nameof(Computers));
            }
        }

        public ComputerViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                Computers = new ObservableCollection<Computer>(dbContext.Computers.ToList());
            }
        }

        public Computer GetById(int id)
        {
            return Computers.FirstOrDefault(comp => comp.Id == id);
        }

        public void Delete(Computer comp)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Computers.Remove(comp);
                dbContext.SaveChanges();
                Computers.Remove(comp); // Обновляем отображаемую коллекцию
            }
        }

        public void Update(Computer comp)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Computers.Update(comp);
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
