using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;//таймер
        private TimeSpan remainingTime;//оставшееся время
        private TimeSpan workTime;//выбранное время
        private TimeSpan breakTime = TimeSpan.FromMinutes(5);//время отдыха(фиксированное - 5 минут)
        private bool isWorkMode = true;//режим работы
        private bool isRunning = false;//идет ли таймер

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Timer()
        {   //новый таймер
            timer = new DispatcherTimer();
            //устанавливаю таймер тика 1 сек
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;//обработчик события на каждую секунду
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            //проверка осталось ли время
            if {(remainingTime.TotalSeconds > 0)
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
            UpdateTimerDisplay();
        }else{
                StopTimer();
                string message = isWorkMode ?
                    "Время закончилось! Отдохните 5 минут" :
                    "Отдых закончился! Возвращайтесь к работе";
                MessageBox.Show(message, "Помодоро таймер",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                SwitchMode();
        }
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WorkModeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void work30min_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}