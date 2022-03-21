using System;
using System.Windows;
using System.Windows.Controls;

namespace Kursach
{

    public partial class addwork : Page
    {
        private SQLRequest request = new SQLRequest();
        public addwork()
        {
            InitializeComponent();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(NameSalary.Text) > 0 && NameType.Text.Length > 0)
                {
                    string qwry = $"INSERT INTO [Work] " +
                        $"( discription, payment_per_day ) " +
                        $"VALUES ( '{NameType.Text}', {NameSalary.Text} )";
                    request.Insert(qwry);
                    NameType.Clear();
                    NameSalary.Clear();
                    MessageBox.Show("Добавлен!");
                }
                else
                {
                    MessageBox.Show("Введите правильные значения");
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
                    string qwery = $"DELETE FROM [Work] WHERE (((Work.id_work)={IDText.Text}));";
                    request.Delete(qwery);
                    IDText.Clear();
                    MessageBox.Show("Удалена!");
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
