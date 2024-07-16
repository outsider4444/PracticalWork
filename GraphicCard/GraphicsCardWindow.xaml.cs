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
    /// Логика взаимодействия для GraphicCardWindow.xaml
    /// </summary>
    public partial class GraphicsCardWindow : Window
    {
        List<GraphicsCard> graphicsCards;
        public GraphicsCardWindow()
        {
            InitializeComponent();
            GraphicsCardViewModel graphicsCardViewModel = new GraphicsCardViewModel();
            graphicsCards = graphicsCardViewModel.GraphicsCards.ToList();
            GraphicsCardDataGrid.ItemsSource = graphicsCards;
        }

        private void GraphicsCardDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GraphicsCardDataGrid.SelectedItem != null)
            {
                var selectedProduct = GraphicsCardDataGrid.SelectedItem as GraphicsCard;
                if (selectedProduct != null)
                {
                    UpdateBtn.IsEnabled = true;
                    DeleteBtn.IsEnabled = true;
                }
                else
                {
                    UpdateBtn.IsEnabled = false;
                    DeleteBtn.IsEnabled = false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GraphicsCardCreate ramCreate = new GraphicsCardCreate();
            ramCreate.Show();
            this.Close();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            GraphicsCardViewModel DataContext = new GraphicsCardViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedGraphicsCard = (GraphicsCard)GraphicsCardDataGrid.SelectedItem;

            if (selectedGraphicsCard != null)
            {
                // Удаляем запись из базы данных
                DataContext.Delete(selectedGraphicsCard);
                MessageBox.Show("Видеокарта успешно удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                DataContext.LoadData();
                GraphicsCardDataGrid.ItemsSource = DataContext.GraphicsCards;
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
             GraphicsCardViewModel DataContext = new GraphicsCardViewModel();
            // Получаем выбранную запись из DataGrid
            var selectedGraphicsCard = GraphicsCardDataGrid.SelectedItem as GraphicsCard;

            if (selectedGraphicsCard != null)
            {
                GraphicsCard ram = DataContext.GetById(selectedGraphicsCard.Id);
                GraphicsCardCreate ramUpdate = new GraphicsCardCreate(ram);
                ramUpdate.Show();
                this.Close();
            }
        }
    }
}
