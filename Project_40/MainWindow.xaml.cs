using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace Project_40
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = 4000;          // Интервал 
            timer.Elapsed += OnTimedEvent;  // Вызов просьб Кузи 
            message = MessageNew;           // Проверка просьб Кузи и отображение элементов в приложении
            kuzya_action = RequestKyzia;    // Создание просьбы
            Starting();
        }
        private Timer timer = new Timer();
        private DateTime DOB { get; set; }       // Дата рождения
        private bool? Feed { get; set; }         // Кормить
        private bool? Walk { get; set; }          // Гулять
        private bool? Sleep { get; set; }         // Спать
        private bool? Play { get; set; }          // Играть
        private bool? Bathe { get; set; }         // Купать
        private bool? Treat { get; set; }         // Лечить

        private delegate void Message();
        private event Message message;
        private delegate void KuzyaAction();
        private event KuzyaAction kuzya_action;
        // Начало существования кузи
        private void Starting()
        {
            DOB = DateTime.Now; // Дата рождения
            Request(Feed: false, Walk: false, Sleep: false, Play: false, Bathe: false, Treat: false);   // Обнуление просьб
            timer.Start();
            message();   // Вызов функции проверки просьб
            TextBlock(MessageEnd, end, Visibility.Hidden);
            Dispatcher.Invoke(new Action(() => {
                MessageDeath.Visibility = Visibility.Hidden;
            }));
        }
        private void Finish()
        {
            timer.Stop();
            TextBlock(MessageEnd, end, Visibility.Visible);
            Dispatcher.Invoke(new Action(() => {
                MessageDeath.Visibility = Visibility.Visible;
            }));
        }
        // Функция выбора просьбы через рандом
        private void RequestKyzia()
        {
            switch (new Random().Next(0, 5))
            {
                case 0:
                    Request(Feed: true);
                    break;
                case 1:
                    Request(Walk: true);
                    break;
                case 2:
                    Request(Sleep: true);
                    break;
                case 3:
                    Request(Play: true);
                    break;
                case 4:
                    Request(Bathe: true);
                    break;
                default:
                    break;
            }
        }
        // Если у кузи есть просьба функция отображает или скрывает елементы просьбы: текст и кнопку
        private void MessageNew()
        {
            if (Treat == true) Finish();
            if (Feed == true) TextBlock(MessageFeed, feed, Visibility.Visible);
            else TextBlock(MessageFeed, feed, Visibility.Hidden);
            if (Walk == true) TextBlock(MessageWalk, walk, Visibility.Visible);
            else TextBlock(MessageWalk, walk, Visibility.Hidden);
            if (Sleep == true) TextBlock(MessageSleep, sleep, Visibility.Visible);
            else TextBlock(MessageSleep, sleep, Visibility.Hidden);
            if (Play == true) TextBlock(MessagePlay, play, Visibility.Visible);
            else TextBlock(MessagePlay, play, Visibility.Hidden);
            if (Bathe == true) TextBlock(MessageBathe, bathe, Visibility.Visible);
            else TextBlock(MessageBathe, bathe, Visibility.Hidden);
            if (CheckDisease()) Request(Treat: true);
            if (Treat == true) TextBlock(MessageTreat, treat, Visibility.Visible);
            else TextBlock(MessageTreat, treat, Visibility.Hidden);
        }

        // Функция подсчёта существующих просьб у кузи
        private bool CheckDisease()
        {
            if (CountRequests(Feed) + CountRequests(Walk) + CountRequests(Sleep) + CountRequests(Play) + CountRequests(Bathe) > 2) return true;
            else return false;
        }
        // Функция возврата 1 если просьба существует для подсчёта просьб
        private int CountRequests(bool? request = null)
        {
            if (request == true) return 1;
            else return 0;
        }
        // Функция отображения или скрытия элементов приложения 
        private void TextBlock(TextBlock block, Button button, Visibility visibility)
        {
            Dispatcher.Invoke(new Action(() => {
                block.Visibility = visibility;
                button.Visibility = visibility;
            }));
        }
        // Функция работы Кузи
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (e.SignalTime >= DOB.AddMinutes(2.0))    // если время больше даты рождения на 2 минуты то Кузя завершает цикл жизни
            {
                Finish();           // Кузя умирает
            }
            else
            {
                kuzya_action();     // Создание просьбы
                message();          // Проверка просьб Кузи и отображение элементов в приложении
            }
        }
        // Добавление просьбы
        private void Request(bool? Feed = null, bool? Walk = null, bool? Sleep = null, bool? Play = null, bool? Bathe = null, bool? Treat = null)
        {
            if (Feed != null) this.Feed = Feed;
            if (Walk != null) this.Walk = Walk;
            if (Sleep != null) this.Sleep = Sleep;
            if (Treat != null) this.Treat = Treat;
            if (Play != null) this.Play = Play;
            if (Bathe != null) this.Bathe = Bathe;
        }
        // Очистка просьб
        private void AnswerFull()
        {
            Feed = false;
            Walk = false;
            Sleep = false;
            Treat = false;
            Play = false;
            Bathe = false;
        }
        private void feed_Click(object sender, RoutedEventArgs e)
        {
            Request(Feed: false);   // Выполнение просьбы
            message();              // Проверка просьб Кузи и отображение элементов в приложении
        }
        private void walk_Click(object sender, RoutedEventArgs e)
        {
            Request(Walk: false);   // Выполнение просьбы
            message();              // Проверка просьб Кузи и отображение элементов в приложении
        }
        private void sleep_Click(object sender, RoutedEventArgs e)
        {
            Request(Sleep: false);   // Выполнение просьбы
            message();               // Проверка просьб Кузи и отображение элементов в приложении
        }
        private void play_Click(object sender, RoutedEventArgs e)
        {
            Request(Play: false);   // Выполнение просьбы
            message();              // Проверка просьб Кузи и отображение элементов в приложении
        }
        private void bathe_Click(object sender, RoutedEventArgs e)
        {
            Request(Bathe: false);   // Выполнение просьбы
            message();              // Проверка просьб Кузи и отображение элементов в приложении
        }
        private void treat_Click(object sender, RoutedEventArgs e)
        {
            AnswerFull();   // Очистка всех просьб после лечения
            message();      // Проверка просьб Кузи и отображение элементов в приложении
        }
        private void end_Click(object sender, RoutedEventArgs e)
        {
            Starting();     // Создание нового Кузи
        }
    }
}
