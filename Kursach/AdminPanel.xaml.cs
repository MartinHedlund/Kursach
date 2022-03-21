using System;
using System.Collections.Generic;
using System.Windows;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private static List<PersonModel> People { get; set; }
        private static List<WorkModel> Works { get; set; }
        private static List<TypeWorkModel> TypeWorks { get; set; }

        public AdminPanel()
        {
            InitializeComponent();
        }
        internal static void dateupdate()
        {
            PersonViewModel PersonV = new PersonViewModel();
            People = new List<PersonModel>(PersonV.GetAllPeople());

            WorkViewModel workView = new WorkViewModel();
            Works = new List<WorkModel>(workView.GetAllWork());

            TypeWorkViewModel typeWorkView = new TypeWorkViewModel();
            TypeWorks = new List<TypeWorkModel>(typeWorkView.GetAllTypeWork());
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dateupdate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dateupdate();
            TableGrid.ItemsSource = People;
            Frame.Visibility = Visibility.Visible;
            Frame.Source = null;
            Frame.Source = new System.Uri("addpers.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dateupdate();
            TableGrid.ItemsSource = Works;
            Frame.Visibility = Visibility.Visible;
            Frame.Source = null;
            Frame.Source = new System.Uri("addwork.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dateupdate();
            TableGrid.ItemsSource = TypeWorks;
            Frame.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void Btt_save(object sender, RoutedEventArgs e)
        {
            Save();
        }
        private void Btt_update(object sender, RoutedEventArgs e)
        {
            Save();
            dateupdate();
        }
        internal static void Save()
        {
            SQLRequest request = new SQLRequest();
            foreach (var person in People)
            {
                string qrtyPerson = $"" +
                    $"UPDATE Person SET Person.FIO = \"{person.FIO}\", Person.salary = {person.Salary}, Person.zan = {person.Zan}, " +
                    $"Person.dop_plata = {person.Dop}, Person.itog = {person.Itog} " +
                    $"WHERE (((Person.p_id)={person.P_id}));";
                request.Update(qrtyPerson);
                request.Close();
            }
            foreach (var work in Works)
            {
                string qrtyWork = $"" +
                    $"UPDATE[Work] SET[Work].discription = \"{work.Discription}\", [Work].payment_per_day = {work.Payment_per_day} " +
                    $"WHERE(((Work.id_work) = {work.Id_work}));";
                request.Update(qrtyWork);
                request.Close();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

    }
}
