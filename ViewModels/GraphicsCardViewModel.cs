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
    public class GraphicsCardViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<GraphicsCard> _graphicsCards;
        public ObservableCollection<GraphicsCard> GraphicsCards
        {
            get { return _graphicsCards; }
            set
            {
                _graphicsCards = value;
                OnPropertyChanged(nameof(GraphicsCard));
            }
        }

        public GraphicsCardViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var dbContext = new AppDbContext())
            {
                GraphicsCards = new ObservableCollection<GraphicsCard>(dbContext.GraphicsCards.ToList());
            }
        }

        public GraphicsCard GetById(int id)
        {
            return GraphicsCards.FirstOrDefault(graphicCards => graphicCards.Id == id);
        }

        public void Delete(GraphicsCard graphicCards)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.GraphicsCards.Remove(graphicCards);
                dbContext.SaveChanges();
                GraphicsCards.Remove(graphicCards); // Обновляем отображаемую коллекцию
            }
        }

        public void Update(GraphicsCard graphicCards)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.GraphicsCards.Update(graphicCards);
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
