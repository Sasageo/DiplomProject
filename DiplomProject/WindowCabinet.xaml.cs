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
using System.Windows.Shapes;

namespace DiplomProject
{
    /// <summary>
    /// Interaction logic for WindowCabinet.xaml
    /// </summary>
    public partial class WindowCabinet : Window
    {
        WindowAdmin wa;
        WindowWorker ww;
        int idUser;
        public WindowCabinet(WindowWorker w, int id)
        {
            InitializeComponent();
            idUser = id;
            ww = w;
            FillInfo();
        }
        public WindowCabinet(WindowAdmin w, int id)
        {
            InitializeComponent();
            idUser = id;
            wa = w;
            FillInfo();
        }
        private void FillInfo()
        {
            labelErrorCab.Content = "";
            passwordBoxNew.Password = "";
            passwordBoxOld.Password = "";

            string fio = "";
            string login = "";
            string phone = "";

            string cmdInfo = $"select Фио, Телефон, Логин from Пользователи where Код = {idUser}";

            List<string> values = ClassDB.GetReader(cmdInfo);

            // если есть данные.
            if (values != null)
            {
                fio = values[0].ToString();
                phone = values[1].ToString();
                login = values[2].ToString();

                labelLogin.Content = $"{login}";
                labelName.Content = "ФИО: " + fio;
                labelPhone.Content = "Номер телефона: " + phone;

                textBoxLoginEdit.Text = login;
                textBoxNumberEdit.Text = phone;
            }

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (wa != null)
            {
                wa.Show();
            }
            else
            {
                ww.Show();
            }
        }
        private bool CheckLogin()
        {
            string cmdInfo = $"select Код from Пользователи where Логин = @login and Код <> {idUser}";

            List<SqlParameter> pars = new List<SqlParameter>();

            pars.Add(new SqlParameter("@login", textBoxLoginEdit.Text));

            string check = ClassDB.GetScalary(cmdInfo, pars);

            // если есть данные.
            if (check != "" && check != null)
            {
                labelErrorCab.Content = "Такой логин уже занят";
                return false;
            }
            else
            {
                labelErrorCab.Content = "";
                return true;
            }
        }
        private void buttonEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxLoginEdit.Text.Trim() != "" && textBoxNumberEdit.Text.Trim() != "" && passwordBoxOld.Password.Trim() != "")
            {
                if (CheckLogin())
                {
                    string cmdInfo = $"select Код from Пользователи where Логин = @login and Пароль = HASHBYTES('SHA2_256', Convert(varchar(MAX), @password))";

                    List<SqlParameter> pars = new List<SqlParameter>();

                    pars.Add(new SqlParameter("@login", labelLogin.Content));
                    pars.Add(new SqlParameter("@password", passwordBoxOld.Password));

                    string check = ClassDB.GetScalary(cmdInfo, pars);

                    // если есть данные.
                    if (check != "" && check != null)
                    {

                        if (passwordBoxNew.Password.Trim() != "")
                        {
                            string edit = $"update Пользователи set Логин = '{textBoxLoginEdit.Text}', " +
                            $"Пароль = HASHBYTES('SHA2_256', Convert(varchar(MAX), '{passwordBoxNew.Password}')), " +
                            $"Телефон = '{textBoxNumberEdit.Text}' where Код = {idUser}";

                            ClassDB.UpdateDBTable(edit);
                        }
                        else
                        {
                            string edit = $"update Пользователи set Логин = '{textBoxLoginEdit.Text}', " +
                            $"Телефон = '{textBoxNumberEdit.Text}' where Код = {idUser}";

                            ClassDB.UpdateDBTable(edit);
                        }

                        FillInfo();
                        labelErrorCab.Content = "Данные успешно изменены";
                    }
                    else
                    {
                        labelErrorCab.Content = "Неверный пароль";
                    }
                }
            }

            else
            {
                labelErrorCab.Content = "Некорректные данные";
            }
        }
        private void textBoxNumberEdit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) || (textBoxNumberEdit.Text == "" && e.Text != "8"))
            {
                e.Handled = true;
            }
        }
    }
}
