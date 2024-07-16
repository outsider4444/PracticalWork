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
    /// Логика взаимодействия для GraphicsCardCreate.xaml
    /// </summary>
    public partial class GraphicsCardCreate : Window
    {
        bool isChange = false;
        GraphicsCard changedGraphicsCard;

        public GraphicsCardCreate()
        {
            InitializeComponent();
        }

        public GraphicsCardCreate(GraphicsCard graphicsCard)
        {
            InitializeComponent();

            isChange = true;
            changedGraphicsCard = graphicsCard;

            Model.Text = graphicsCard.Model;
            VideoMemorySize.Text = graphicsCard.VideoMemorySize.ToString();
            Year.Text = graphicsCard.Year.ToString();
            MemoryType.Text = graphicsCard.MemoryType.ToString();
            Frequency.Text = graphicsCard.Frequency.ToString();
            Status.Text = graphicsCard.Status.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GraphicsCardWindow graphicsCardWindow = new GraphicsCardWindow();
            graphicsCardWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isChange)
            {
                UpdateGraphicsCard();
            }
            else
            {
                SaveGraphicsCard();
            }


            GraphicsCardViewModel DataContext = new GraphicsCardViewModel();
            DataContext.LoadData();

            // Открываем окно со списком клиентов после успешного добавления
            GraphicsCardWindow ramWindow = new GraphicsCardWindow();
            ramWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }
        private void SaveGraphicsCard()
        {
            GraphicsCard newGraphicsCard = new GraphicsCard
            {

                Model = Model.Text,
                VideoMemorySize = Int32.Parse(VideoMemorySize.Text),
                Year = Int32.Parse(Year.Text),
                MemoryType = MemoryType.Text,
                Frequency = Int32.Parse(Frequency.Text),
                Status = Status.Text
            };

            using (var dbContext = new AppDbContext())
            {
                dbContext.GraphicsCards.Add(newGraphicsCard);
                dbContext.SaveChanges();

            }

            MessageBox.Show("Видеокарта успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void UpdateGraphicsCard()
        {
            changedGraphicsCard.Model = Model.Text;
            changedGraphicsCard.VideoMemorySize = Int32.Parse(VideoMemorySize.Text);
            changedGraphicsCard.Year = Int32.Parse(Year.Text);
            changedGraphicsCard.MemoryType = MemoryType.Text;
            changedGraphicsCard.Frequency = Int32.Parse(Frequency.Text);
            changedGraphicsCard.Status = Status.Text;

            // Добавляем нового клиента в базу данных
            using (var dbContext = new AppDbContext())
            {
                dbContext.GraphicsCards.Update(changedGraphicsCard);
                dbContext.SaveChanges();
            }

            MessageBox.Show("Видеокарта успешно обновлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
