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
        public ObservableCollection<GraphicsCard> GraphicsCard
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
                GraphicsCard = new ObservableCollection<GraphicsCard>(dbContext.GraphicCards.ToList());
            }
        }

        public GraphicsCard GetById(int id)
        {
            return GraphicsCard.FirstOrDefault(graphicCards => graphicCards.Id == id);
        }

        public void Delete(GraphicsCard graphicCards)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.GraphicCards.Remove(graphicCards);
                dbContext.SaveChanges();
                GraphicsCard.Remove(graphicCards); // Обновляем отображаемую коллекцию
            }
        }

        public void Update(GraphicsCard graphicCards)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.GraphicCards.Update(graphicCards);
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
