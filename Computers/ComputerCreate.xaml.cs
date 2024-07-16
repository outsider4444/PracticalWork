using PracticalWork.Data;
using PracticalWork.Models;
using PracticalWork.ViewModels;
using System.Windows;

namespace PracticalWork
{
    /// <summary>
    /// Логика взаимодействия для ComputerCreate.xaml
    /// </summary>
    public partial class ComputerCreate : Window
    {
        bool isChange = false;
        Computer changedComputer;

        public ComputerCreate()
        {
            InitializeComponent();
        }

        public ComputerCreate(Computer comp)
        {
            InitializeComponent();
            changedComputer = comp;
            isChange = true;

            Status.Text = comp.Status;
            Purpose.Text = comp.Purpose;
            Description.Text = comp.Description;
            ProcessorId.Text = comp.ProcessorId.ToString();
            GraphicsCardId.Text = comp.GraphicsCardId.ToString();
            PowerSupplyId.Text = comp.GraphicsCardId.ToString();
            HardDriveId.Text = comp.GraphicsCardId.ToString();
            RamId.Text = comp.RamId.ToString();
            Mouse.Text = comp.Mouse;
            Keyboard.Text = comp.Keyboard;
            Monitor.Text = comp.Monitor;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isChange)
            {
                UpdateComputer();
            }
            else
            {
                CreateComputer();
            }

            ComputerViewModel DataContext = new ComputerViewModel();
            DataContext.LoadData();

            // Открываем окно со списком клиентов после успешного добавления
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Закрываем текущее окно добавления
            this.Close();
        }
        private void UpdateComputer()
        {
            changedComputer.Status = Status.Text;
            changedComputer.Purpose = Purpose.Text;
            changedComputer.Description = Description.Text;
            changedComputer.ProcessorId = Int32.Parse(ProcessorId.Text);
            changedComputer.GraphicsCardId = Int32.Parse(GraphicsCardId.Text);
            changedComputer.PowerSupplyId = Int32.Parse(GraphicsCardId.Text);
            changedComputer.HardDriveId = Int32.Parse(GraphicsCardId.Text);
            changedComputer.RamId = Int32.Parse(RamId.Text);
            changedComputer.Mouse = Mouse.Text;
            changedComputer.Keyboard = Keyboard.Text;
            changedComputer.Monitor = Monitor.Text;

            using (var dbContext = new AppDbContext())
            {
                var existingComputer = dbContext.Computers.Find(changedComputer.Id);

                if (existingComputer == null)
                {
                    MessageBox.Show("Компьютер с указанным ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка наличия связанных объектов по ID
                bool processorExists = dbContext.Processors.Any(p => p.Id == changedComputer.ProcessorId);
                bool graphicsCardExists = dbContext.GraphicsCards.Any(g => g.Id == changedComputer.GraphicsCardId);
                bool powerSupplyExists = dbContext.PowerSupplies.Any(ps => ps.Id == changedComputer.PowerSupplyId);
                bool hardDriveExists = dbContext.HardDrives.Any(hd => hd.Id == changedComputer.HardDriveId);
                bool ramExists = dbContext.Rams.Any(r => r.Id == changedComputer.RamId);

                if (!processorExists)
                {
                    MessageBox.Show("Процессор с указанным ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!graphicsCardExists)
                {
                    MessageBox.Show("Видеокарта с указанным ID не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!powerSupplyExists)
                {
                    MessageBox.Show("Блок питания с указанным ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!hardDriveExists)
                {
                    MessageBox.Show("Жесткий диск с указанным ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!ramExists)
                {
                    MessageBox.Show("Оперативная память с указанным ID не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Обновление информации о компьютере
                existingComputer.Status = changedComputer.Status;
                existingComputer.Purpose = changedComputer.Purpose;
                existingComputer.Description = changedComputer.Description;
                existingComputer.ProcessorId = changedComputer.ProcessorId;
                existingComputer.GraphicsCardId = changedComputer.GraphicsCardId;
                existingComputer.PowerSupplyId = changedComputer.PowerSupplyId;
                existingComputer.HardDriveId = changedComputer.HardDriveId;
                existingComputer.RamId = changedComputer.RamId;
                existingComputer.Mouse = changedComputer.Mouse;
                existingComputer.Keyboard = changedComputer.Keyboard;
                existingComputer.Monitor = changedComputer.Monitor;

                dbContext.SaveChanges();

                // Уведомление об успешном обновлении
                MessageBox.Show("Информация о компьютере успешно обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    

    private void CreateComputer()
    {
        Computer computer = new Computer
        {
            Status = Status.Text,
            Purpose = Purpose.Text,
            Description = Description.Text,
            ProcessorId = Int32.Parse(ProcessorId.Text),
            GraphicsCardId = Int32.Parse(GraphicsCardId.Text),
            PowerSupplyId = Int32.Parse(GraphicsCardId.Text),
            HardDriveId = Int32.Parse(GraphicsCardId.Text),
            RamId = Int32.Parse(RamId.Text),
            Mouse = Mouse.Text,
            Keyboard = Keyboard.Text,
            Monitor = Monitor.Text
        };

        using (var dbContext = new AppDbContext())
        {
            // Проверка наличия связанных объектов по ID
            bool processorExists = dbContext.Processors.Any(p => p.Id == computer.ProcessorId);
            bool graphicsCardExists = dbContext.GraphicsCards.Any(g => g.Id == computer.GraphicsCardId);
            bool powerSupplyExists = dbContext.PowerSupplies.Any(ps => ps.Id == computer.PowerSupplyId);
            bool hardDriveExists = dbContext.HardDrives.Any(hd => hd.Id == computer.HardDriveId);
            bool ramExists = dbContext.Rams.Any(r => r.Id == computer.RamId);

            if (!processorExists)
            {
                MessageBox.Show("Процессор с указанным ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!graphicsCardExists)
            {
                MessageBox.Show("Видеокарта с указанным ID не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!powerSupplyExists)
            {
                MessageBox.Show("Блок питания с указанным ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!hardDriveExists)
            {
                MessageBox.Show("Жесткий диск с указанным ID не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!ramExists)
            {
                MessageBox.Show("Оперативная память с указанным ID не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dbContext.Computers.Add(computer);
            dbContext.SaveChanges();

            // Уведомление об успешном добавлении
            MessageBox.Show("Новый компьютер успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
}
