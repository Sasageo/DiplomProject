using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string role;
        private int id;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text.Trim() != "" || passwordBoxPass.Password.Trim() != "")
            {
                string cmdLogin = "select Роль, Код from Пользователи where Логин = @login and Пароль = HASHBYTES('SHA2_256', Convert(varchar(MAX), @password))";

                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(new SqlParameter("@login", textBoxLogin.Text));
                pars.Add(new SqlParameter("@password", passwordBoxPass.Password));

                List<string> values = ClassDB.GetReader(cmdLogin, pars);

                if (values != null)
                {
                    role = values[0].ToString();
                    id = Convert.ToInt32(values[1].ToString());

                    if (role == "Администратор")
                    {
                        WindowAdmin wa = new WindowAdmin(this, id);
                        wa.Show();
                        Hide();
                    }
                    else if(role == "Сотрудник")
                    {
                        WindowWorker ww = new WindowWorker(id, this);

                        ww.Show();
                        Hide();
                    }

                    textBoxLogin.Text = "";
                    passwordBoxPass.Password = "";
                }
                else
                {
                    MessageBox.Show("Логин или пароль введены неверно", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Данные в полях заполнены неверно", "Неккоректные данные", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

