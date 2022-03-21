using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Kursach
{
    public partial class Window1 : Window
    {
        // поля
        private SQLRequest request = new SQLRequest();
        private bool pe = false, da = false;
        public List<PersonModel> People { get; set; }

        private PersonViewModel person = new PersonViewModel();
        //
        public Window1()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dateupdate();
        }

        public void dateupdate()
        {
            request.UpdateNEZan();
            request.UpdateZan();
            People = new List<PersonModel>(person.GetFreePeople());
            enter_pers.ItemsSource = People;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(cost.Text) > 0)
                {
                    var dataColletion = enter_pers.SelectedItems;
                    foreach (Kursach.PersonModel obj in dataColletion)
                    {
                        string qwry = $"INSERT INTO TypeWork " +
                              $"([id_person], [person_t], [work_t], [data_start], [data_end]) " +
                              $"VALUES({obj.P_id}, '{obj.FIO}', 'СРОЧНАЯ: {ggg.Text}', " +
                              $"'{DateTime.Now.ToShortDateString()}', '{data_end.SelectedDate}')";
                        request.Insert(qwry);
                        obj.Dop += Convert.ToInt32(cost.Text);
                        string qrtyPerson = $"" +
                            $"UPDATE Person SET Person.FIO = \"{obj.FIO}\", Person.salary = {obj.Salary}, Person.zan = {obj.Zan}, " +
                              $"Person.dop_plata = {obj.Dop}, Person.itog = {obj.Itog} " +
                              $"WHERE (((Person.p_id)={obj.P_id}));";
                        request.Update(qrtyPerson);
                        request.Close();
                    }
                }
                else
                {
                    throw new Exception("Не коректный ввод цены");
                }

                update.IsEnabled = true;// включаем кнопку обновление
                update.Visibility = Visibility.Visible;
                dateupdate(); // обновляем данные
                ggg.Clear();// очищаем поле ввода
                cost.Clear();// очищаем поле цены
                data_end.SelectedDate = null;//очищаем выбранных рабочников
                pe = false; // флаг что поле вводо пустое
                da = false; // флаг что поле даты пустое
                btt.IsEnabled = false;                
            }
            catch (Exception) { MessageBox.Show("ERRORRRRRRRRRRRRRRRRR!"); }

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data_end.SelectedDate != null)
            {
                da = true;
            }

            Check();
        }

        private void enter_pers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (enter_pers.SelectedItems != null)
            {
                pe = true;
            }

            Check();
        }

        private void Check()
        {
            if (pe && da && (ggg.Text.Length > 0))
            {
                btt.IsEnabled = true;
            }
        }

        private void Backe_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            dateupdate();
            ggg.Text = "";
            btt.IsEnabled = false;
            update.IsEnabled = false;
        }

      
    }
}