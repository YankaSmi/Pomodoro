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
            Timer();
            workTime = TimeSpan.FromMinutes(20);
            remainingTime = workTime;
            TimerDisplay.Text = $"Работа: {FormatTime(remainingTime)}";
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
            if (remainingTime.TotalSeconds > 0)
            {
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                UpdateTimerDisplay();
            }
            else
            {
                StopTimer();
                string message = isWorkMode ?
                    "Время закончилось! Отдохните 5 минут" :
                    "Отдых закончился! Возвращайтесь к работе";
                MessageBox.Show(message, "Помодоро таймер",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                SwitchMode();
            }
        }

        private void StartBreakMode()
        {
            isWorkMode = false;
            remainingTime = breakTime;
            UpdateTimerDisplay();
        }

        private void StartWorkMode()
        {
            isWorkMode = true;
            remainingTime = workTime;
            UpdateTimerDisplay();
        }

        private void SwitchMode()
        {
            if (isWorkMode)
                StartBreakMode();
            else
                StartWorkMode();

            UpdateButtonState();
        }

        private void UpdateTimerDisplay()
        {
            string prefix = isWorkMode ? "Работа: " : "Отдых: ";
            TimerDisplay.Text = prefix + FormatTime(remainingTime);
        }

        private string FormatTime(TimeSpan time)
        {
            return $"{time.Minutes:D2}:{time.Seconds:D2}";
        }

        private void UpdateButtonState()
        {
            StartBtn.IsEnabled = !isRunning;
            PauseBtn.IsEnabled = isRunning;
        }

        private void StartTimer()
        {
            timer.Start();
            isRunning = true;
            UpdateButtonState();
        }

        private void StopTimer()
        {
            timer.Stop();
            isRunning = false;
            UpdateButtonState();
        }

        private void ResetTimer()
        {
            StopTimer();
            if (isWorkMode)
            {
                remainingTime = workTime;
                TimerDisplay.Text = $"Работа: {FormatTime(remainingTime)}";
            }
            else
            {
                remainingTime = breakTime;
                TimerDisplay.Text = $"Отдых: {FormatTime(remainingTime)}";
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            StartTimer();
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            StopTimer();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
        }

        private void WorkTimeCheck(object sender, RoutedEventArgs e)
        {
            if (!isRunning)
            {
                if (work20min.IsChecked == true)
                    workTime = TimeSpan.FromMinutes(20);
                else if (work25min.IsChecked == true)
                    workTime = TimeSpan.FromMinutes(25);
                else if (work30min.IsChecked == true)
                    workTime = TimeSpan.FromMinutes(30);
                else if (work45min.IsChecked == true)
                    workTime = TimeSpan.FromMinutes(45);
                else if (work60min.IsChecked == true)
                    workTime = TimeSpan.FromMinutes(60);
                else if (work25sec.IsChecked == true)  //для 25сек
                    workTime = TimeSpan.FromSeconds(25);

                if (isWorkMode)
                {
                    remainingTime = workTime;
                    UpdateTimerDisplay();
                }
            }
        }
    }
}