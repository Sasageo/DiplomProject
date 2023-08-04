using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for WindowWorker.xaml
    /// </summary>
    public partial class WindowWorker : Window
    {
        MainWindow mw;
        private DataTable dtt, dtR;
        private string[] cS;
        int idRem;
        int curTestId;
        int currentQuestion = 0;
        List<ClassQuestions> questions = new List<ClassQuestions>();
        int idUser;
        public WindowWorker(int id, MainWindow m)
        {
            InitializeComponent();

            mw = m;
            idUser = id;
            cS = new string[] { "Код", "Код теста", "Код пользователя", "Код вопроса", "Балл", "Статус", "Кол-во вариантов ответа" };

            string testsQ = "select * from Тесты";

            dtt = ClassDB.UpdateDataGridTable(testsQ, "dtt");

            dataGridTests.ItemsSource = dtt.DefaultView;

            RememberCheck();
            FillName();
        }

        private void FillName()
        {
            labelName.Content += ClassDB.GetScalary($"select Фио from Пользователи where Код = {idUser}");
        }
        private void StartTest(int idTest, string nameTest)
        {
            labelTest.Content = "Тест: " + nameTest;

            SetQuestions(idTest);

            currentQuestion = 0;

            dataGridVariantsForE.ItemsSource = questions[currentQuestion].Variants;
            textBlockQuestion.Text = questions[currentQuestion].Name;
        }
        private void SetQuestions(int idTest)
        {
            questions.Clear();

            string cmd = $"select Код, Вопрос from Вопросы where Код_теста = {idTest}";

            List<string> values = ClassDB.GetReader(cmd);
            // если есть данные.
            if (values != null)
            {
                string nameQ = "";
                int idQ = 0;
                for (int i = 0; i < values.Count - 1; i += 2)
                {
                    idQ = Convert.ToInt32(values[i].ToString());
                    nameQ = values[i + 1].ToString();
                    questions.Add(new ClassQuestions(idQ, nameQ));
                }

                foreach (ClassQuestions q in questions)
                {
                    q.Variants = SetVariants(q.Id);
                }
            }
            else
            {
                MessageBox.Show("У данного теста еще нет вопросов", "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private List<ClassVariants> SetVariants(int idQ)
        {
            string cmdV = $"select Код, Вариант, Балл from [Варианты ответов] where Код_вопроса = {idQ}";

            List<string> values = ClassDB.GetReader(cmdV);
            List<ClassVariants> listV = new List<ClassVariants>();
            // если есть данные.
            if (values != null)
            {
                string nameV = "";
                int idV = 0;
                double point = 0.0;

                for (int i = 0; i < values.Count-2; i += 3)
                {
                    idV = Convert.ToInt32(values[i].ToString());
                    nameV = values[i + 1].ToString();
                    point = Convert.ToDouble(values[i + 2].ToString());
                    listV.Add(new ClassVariants(idV, nameV, point));
                }

                return listV;
            }
            else
            {
                MessageBox.Show("У данного вопроса еще нет вариантов", "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        private void GetResult()
        {
            string queryForResult = "select TOP(1) [Результаты тестов].[Итого баллов], " +
                "[Результаты тестов].Результат " +
                "from [Результаты тестов] " +
                "inner join Пользователи on [Результаты тестов].Пользователь = Пользователи.Код " +
                $"inner join Тесты on [Результаты тестов].[Код теста] = Тесты.Код where Пользователи.Код = {idUser} " +
                $"and Тесты.Код = {curTestId} order by Дата desc";

            double points = 0;
            string result = "";

            List<string> results = ClassDB.GetReader(queryForResult);

            if(results != null)
            {
                points = Convert.ToDouble(results[0].ToString());
                result = results[1].ToString();
            }

            MessageBox.Show($"Тест успешно решен\nВаши баллы: {points} \nВаш результат: {result}", "Тест");

        }
        private void SaveResult()
        {
            string query = $"update Уведомления set Статус = 'Готово' where Код = {idRem}";

            ClassDB.UpdateDBTable(query);

            RememberCheck();

            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string queryS = $"insert into [Значения вопросов] values({idUser}, @idV, '{date}')";

            foreach (ClassQuestions q in questions)
            {
                List<SqlParameter> pars = new List<SqlParameter>();

                pars.Add(new SqlParameter("idV", q.Result));

                ClassDB.InsertInTable(queryS, pars);
            }

            tabControlWorker.SelectedIndex = 0;
            tabItemTestE.IsEnabled = false;
        }
        private void RememberCheck()
        {
            string cRemQ = $"select Count(*) from Уведомления where Код_пользователя = {idUser} and Статус = 'Отправлено'";
            int rCount;

            rCount = int.Parse(ClassDB.GetScalary(cRemQ));
            if (rCount == 0)
            {
                tabItemRemember.IsEnabled = false;
                tabItemRemember.Header = "Уведомлений нет";
            }
            else
            {
                tabItemRemember.IsEnabled = true;
                tabItemRemember.Header = "Уведомления: " + ClassDB.GetScalary(cRemQ);

                string rQ = $"select " +
                    $"Уведомления.[Код], " +
                    $"Тесты.Название, " +
                    $"[Код_пользователя], " +
                    $"[Код_теста]," +
                    $"[Текст], " +
                    $"[Статус] " +
                    $"from Уведомления inner join Тесты on Тесты.Код = Уведомления.Код_теста where Код_пользователя = {idUser} and Статус = 'Отправлено'";

                dtR = ClassDB.UpdateDataGridTable(rQ, "dtR");

                dataGridRemembers.ItemsSource = dtR.DefaultView;
            }
        }

        /////////////////////////////////////////////////////////
        ////////////////////Методы для работы с БД///////////////
        /////////////////////////////////////////////////////////


        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;

            foreach (string s in cS)
            {
                if (propertyDescriptor.DisplayName == s)
                {
                    e.Cancel = true;
                }
            }
        }
        private void buttonAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVariantsForE.SelectedIndex != -1)
            {
                questions[currentQuestion].Result = questions[currentQuestion].Variants[dataGridVariantsForE.SelectedIndex].Id;

                if (currentQuestion + 1 == questions.Count - 1)
                {
                    buttonAnswer.Content = "Завершить тест";
                }

                if (currentQuestion + 1 < questions.Count)
                {
                    currentQuestion++;

                    dataGridVariantsForE.ItemsSource = questions[currentQuestion].Variants;
                    textBlockQuestion.Text = questions[currentQuestion].Name;
                }
                else
                {
                    SaveResult();
                    buttonAnswer.Content = "Следующий вопрос";
                    GetResult();
                }
            }
            else
            {
                MessageBox.Show("Не выбран вариант ответа", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void dataGridRemembers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridRemembers.SelectedIndex != -1)
            {
                tabItemTestE.IsEnabled = true;
                tabControlWorker.SelectedIndex = 1;

                idRem = int.Parse(dtR.DefaultView[dataGridRemembers.SelectedIndex]["Код"].ToString());

                curTestId = int.Parse(dtR.DefaultView[dataGridRemembers.SelectedIndex]["Код теста"].ToString());

                StartTest(curTestId,
                   dtR.DefaultView[dataGridRemembers.SelectedIndex]["Название"].ToString());

                dataGridRemembers.SelectedIndex = -1;
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            mw.Show();
        }
        private void buttonLK_Click(object sender, RoutedEventArgs e)
        {
            WindowCabinet wc = new WindowCabinet(this, idUser);
            wc.Show();
            this.Hide();
        }
    }
}
