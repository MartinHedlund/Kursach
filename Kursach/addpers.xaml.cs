using System;
using System.Windows;
using System.Windows.Controls;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для addpers.xaml
    /// </summary>
    public partial class addpers : Page
    {
        private SQLRequest request = new SQLRequest();
        public addpers()
        {
            InitializeComponent();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(NameSalary.Text) > 0 && NameFio.Text.Length > 0)
                {
                    string qwry = $"INSERT INTO Person ( FIO, salary ) " +
                        $"VALUES ( '{NameFio.Text}', {NameSalary.Text} )";
                    request.Insert(qwry);
                    NameFio.Clear();
                    NameSalary.Clear();
                    MessageBox.Show("Добавлен!");
                }
                else
                {
                    MessageBox.Show("Введите правильное значение оклада");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введите правильные значения");
            }
        }

        private void Button_del(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(IDText.Text) > 0)
                {
                    string qwery = $"DELETE FROM Person WHERE (((Person.p_id)={IDText.Text}));";
                    request.Delete(qwery);
                    IDText.Clear();
                    MessageBox.Show("Удален!");
                }
                else
                {
                    MessageBox.Show("Введите правильное значение ID");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введите правильные значения");
            }
        }
    }
}
