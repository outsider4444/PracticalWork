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
    public class ProcessorViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Processor> _processors;
        public ObservableCollection<Processor> Processors
        {
            get { return _processors; }
            set
            {
                _processors = value;
                OnPropertyChanged(nameof(Processors));
            }
        }

        public ProcessorViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                Processors = new ObservableCollection<Processor>(dbContext.Processors.ToList());
            }
        }

        public Processor GetById(int id)
        {
            return Processors.FirstOrDefault(processor => processor.Id == id);
        }

        public void Delete(Processor processor)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Processors.Remove(processor);
                dbContext.SaveChanges();
                Processors.Remove(processor); // Обновляем отображаемую коллекцию
            }
        }

        public void Update(Processor processor)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.Processors.Update(processor);
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
