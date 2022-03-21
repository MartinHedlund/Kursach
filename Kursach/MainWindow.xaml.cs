using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Kursach
{
    public partial class MainWindow : Window
    {
        // поля
        public double cost_of_day = 0;
        private static readonly SQLRequest request = new SQLRequest();
        private int IDperson;
        private PersonModel person = new PersonModel();
        private WorkViewModel workView = new WorkViewModel();
        private PersonViewModel PersonV = new PersonViewModel();
        public List<PersonModel> People { get; set; }
        public List<WorkModel> Works { get; set; }
        // Методы
        public MainWindow()
        {
            InitializeComponent();
            dataUpdate();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            request.Close();
            Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void ComDiscr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WorkModel tempwork = (WorkModel)ComDiscr.SelectedItem; // выбираем вид работы
            if (ComDiscr.SelectedIndex != -1)
            {
                try
                {
                    cost_of_day = tempwork.Payment_per_day;
                    Text_Cost_of_day.Text = "Цена за день: " + cost_of_day.ToString() + 'р';
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                Text_Cost_of_day.Text = "Цена за день: ...";
                cost_of_day = 0;
            }

        }
        private void Raschet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan data = (TimeSpan)(data_end.SelectedDate - data_start.SelectedDate);
                if (data.TotalDays > 0)
                {
                    Itog_SUM.Text = (data.Days * cost_of_day).ToString() + 'р';
                }
                else if (data.TotalDays < 0)
                {
                    throw new Exception();
                }
                else
                {
                    Itog_SUM.Text = cost_of_day.ToString() + 'р';
                }

                if (Itog_SUM.Text.Length > 0)
                { 
                    LoadBtt.IsEnabled = true; // включает кнопку отправки
                    Raschet.IsEnabled = false; // выключает кнопку расчета;
                    person.Dop += Convert.ToDouble(data.Days * cost_of_day);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректная дата");
            }

        }
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ComBOX.SelectedItem != null)
                {
                    person = (Kursach.PersonModel)ComBOX.SelectedItem;
                    IDperson = person.P_id;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        /// Вспомогательные Методы
        public void dataUpdate()
        {
            // обновляем занятость людей
            request.UpdateNEZan(); 
            request.UpdateZan();
            //

            People = new List<PersonModel>(PersonV.GetFreePeople());
            Works = new List<WorkModel>(workView.GetAllWork());

            ComDiscr.ItemsSource = Works;

            if (People.Count > 0)
            {
                ComBOX.ItemsSource = People;
            }
            else
            {
                Raschet.IsEnabled = false;
                MessageBox.Show("Сейчас все сотрудники заняты.\nПодождите пока кто-то осободиться");
            }
            free_person.Text = "Сейчас свободно: " + People.Count.ToString();
        }
        private void LoadBtt_Click(object sender, RoutedEventArgs e)
        {
            string qwry = $"INSERT INTO TypeWork " +
                $"([id_person], [person_t], [work_t], [data_start], [data_end]) " +
                $"VALUES({IDperson}, '{ComBOX.Text}', '{ComDiscr.Text}', '{data_start.SelectedDate}', '{data_end.SelectedDate}')";
            try
            {
                if (ComBOX.SelectedIndex != -1 && ComDiscr.SelectedIndex != -1)
                {
                    request.Insert(qwry);
                    request.UpdateZan();
                    request.Close();
                    LoadBtt.IsEnabled = false;
                    Raschet.IsEnabled = true;
                    dataUpdate();
                    string qwery = $"UPDATE Person SET Person.dop_plata = {person.Dop}, Person.itog = {person.Itog} WHERE(((Person.p_id) = {IDperson}))";
                    request.Update(qwery);
                    MessageBox.Show("Запрос добавлен");
                    data_end.SelectedDate = null;
                    data_start.SelectedDate = null;
                    request.Close();
                }
                else
                {
                    LoadBtt.IsEnabled = false;
                    Raschet.IsEnabled = true;
                    MessageBox.Show("Запоните все поля");
                }
            }
            catch (Exception) { MessageBox.Show("не добавленна строк"); }            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dataUpdate();
        }

        private void Backe_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}
