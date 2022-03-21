using System.Windows;

namespace Kursach
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginText.Text.Trim();
            string pass = PassText.Password.Trim();
            if (login == "admin" && pass == "admin")
            {
                AdminPanel admin = new AdminPanel();
                admin.Show();
                Close();
            }
            else if (login == "user" && pass == "user")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
