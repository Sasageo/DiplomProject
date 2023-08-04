using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;
using Brushes = System.Windows.Media.Brushes;

namespace DiplomProject
{
    /// <summary>
    /// Interaction logic for WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        MainWindow mw;
        private string queryForResult;
        private int mainTestId;
        private string curTable;
        private int variantCount;
        private int idUser;

        private List<ClassVariants> question = new List<ClassVariants>();

        private DataTable dt;
        private DataTable dtU;
        private DataTable dtR;
        private DataTable dtRU;
        private DataTable dtTUR;
        private DataTable dtQUR;
        public WindowAdmin(MainWindow m, int id)
        {
            InitializeComponent();
            mw = m;
            idUser = id;
            labelErrorDisk.Visibility = Visibility.Hidden;

            curTable = "Тесты";

            FillAllDT();
            UpdateAllDG();

            comboBoxTables.SelectedIndex = 0;

            ClassDB.FillComboBox("select Код, Название from Тесты", comboBoxTests, true);
            ClassDB.FillComboBox("select Код, Название from Тесты", comboBoxTestsForR, true);
        }
        private void MakeWorkerWord(string title)
        {
            // создаем приложение ворд
            Word.Application winword = new Word.Application();

            // добавляем документ
            Word.Document document = winword.Documents.Add();

            Word.Paragraph para = document.Content.Paragraphs.Add();

            para.Range.Text = title;
            para.Range.Font.Name = "Times new roman";
            para.Range.Font.Size = 16;
            para.Range.InsertParagraphAfter();

            // добавляем параграф с датой
            Word.Paragraph datePar = document.Content.Paragraphs.Add();
            string date = DateTime.Now.ToShortDateString();

            datePar.Range.Text = "Дата направления: " + date;
            datePar.Range.Font.Name = "Times new roman";
            datePar.Range.Font.Size = 14;
            datePar.Range.InsertParagraphAfter();

            // добавляем параграф с датой прохождения теста
            Word.Paragraph dateTPar = document.Content.Paragraphs.Add();

            dateTPar.Range.Text = "Дата прохождения: " + dtTUR.DefaultView[dataGridTestResult.SelectedIndex]["Дата"].ToString();
            dateTPar.Range.Font.Name = "Times new roman";
            dateTPar.Range.Font.Size = 14;
            dateTPar.Range.InsertParagraphAfter();

            // добавляем параграф с поставщиком
            Word.Paragraph workerPar = document.Content.Paragraphs.Add();
            workerPar.Range.Text = string.Concat("Сотрудник: ", labelUserResult.Content);
            workerPar.Range.Font.Name = "Times new roman";
            workerPar.Range.Font.Size = 14;
            workerPar.Range.InsertParagraphAfter();

            // добавляем параграф с Тестом
            Word.Paragraph testPar = document.Content.Paragraphs.Add();
            string nameTest = dtTUR.DefaultView[dataGridTestResult.SelectedIndex]["Тест"].ToString();
            testPar.Range.Text = "Тест: " + nameTest;
            testPar.Range.Font.Name = "Times new roman";
            testPar.Range.Font.Size = 14;
            testPar.Range.InsertParagraphAfter();

            // добавляем параграф с Тестом
            Word.Paragraph testResPar = document.Content.Paragraphs.Add();
            string resTest = dtTUR.DefaultView[dataGridTestResult.SelectedIndex]["Результат"].ToString();
            testResPar.Range.Text = "Результат: " + resTest;
            testResPar.Range.Font.Name = "Times new roman";
            testResPar.Range.Font.Size = 14;
            testResPar.Range.InsertParagraphAfter();

            // формируем таблицу
            // количество колонок - 3
            // количество строк - nRows

            Word.Paragraph qResPar = document.Content.Paragraphs.Add();
            qResPar.Range.Text = "Вопросы:";
            qResPar.Range.Font.Name = "Times new roman";
            qResPar.Range.Font.Size = 14;
            testResPar.Range.InsertParagraphAfter();

            int nRows = dataGridQuestResult.Items.Count + 1;
            Word.Table myTable = document.Tables.Add(testPar.Range, nRows, 3);
            myTable.Borders.Enable = 1;
            // устанавливаем названия колонок
            var headerRow = myTable.Rows[1].Cells;
            headerRow[1].Range.Text = "Вопрос";
            headerRow[2].Range.Text = "Вариант";
            headerRow[3].Range.Text = "Балл";
            // добавляем данные из таблицы в ворд
            for (int i = 0; i < dataGridQuestResult.Items.Count; i++)
            {
                var dataRow = myTable.Rows[i + 2].Cells;
                dataRow[1].Range.Text = dtQUR.DefaultView[i]["Вопрос"].ToString();
                dataRow[2].Range.Text = dtQUR.DefaultView[i]["Вариант"].ToString();
                dataRow[3].Range.Text = dtQUR.DefaultView[i]["Балл"].ToString();
            }

            Word.Paragraph testResPPar = document.Content.Paragraphs.Add();
            string testResPoint = dtTUR.DefaultView[dataGridTestResult.SelectedIndex]["Итого баллов"].ToString();
            testResPPar.Range.Text = "Итого баллов: " + testResPoint;
            testResPPar.Range.Font.Name = "Times new roman";
            testResPPar.Range.Font.Size = 16;
            testResPPar.Range.InsertParagraphAfter();

            // указываем в какой файл сохранить

            string filepath = "";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "docx";
            if (saveFileDialog.ShowDialog() == true)
            {
                filepath = saveFileDialog.FileName;
                document.SaveAs(filepath);
            }

            document.Close();
            winword.Quit();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            mw.Show();
        }
        private bool CheckVariants()
        {
            for (int i = 0; i < question.Count; i++)
            {
                if (question[i].Name.Trim() != "")
                    continue;
                else
                    return false;
            }

            return true;
        }
        private bool CheckDisk()
        {
            string query = $"select COUNT(*) from [Значения тестов] where( ({textBoxPointTo.Text.Replace(",",".")} between От and  До) or ({double.Parse(textBoxPointDo.Text.Replace(",", "."))} between От and  До) ) and[Код_теста] = {mainTestId}";
            string value = ClassDB.GetScalary(query);

            if (int.Parse(value) != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void ClearInfoEdit()
        {
            labelCode.Content = "Не выбрана строка";
            buttonDeleteRow.IsEnabled = false;
            buttonEditRow.IsEnabled = false;
            textBoxEdit.Text = "";
        }

        /////////////////////////////////////////////////////////
        /////////////////События работы с кнопками///////////////
        /////////////////////////////////////////////////////////
        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            int idTest = mainTestId;

            if (CheckVariants())
            {
                List<SqlParameter> pars1 = new List<SqlParameter>();
                pars1.Add(new SqlParameter("par1", idTest));
                pars1.Add(new SqlParameter("par2", textBoxQuestion.Text));

                ClassDB.InsertInTable("insert into Вопросы values (@par1, @par2)", pars1);

                string idQuestion = ClassDB.GetId($"select TOP(1) Код from Вопросы where Вопрос = '{textBoxQuestion.Text}' and Код_теста = {idTest} ORDER BY Код DESC").ToString();

                string cmdInsertV = $"insert into [Варианты ответов] values ({idQuestion}, @par1, @par2)";

                for (int i = 0; i < question.Count; i++)
                {
                    List<SqlParameter> pars2 = new List<SqlParameter>();
                    pars2.Add(new SqlParameter("par1", question[i].Name));
                    pars2.Add(new SqlParameter("par2", question[i].Point));

                    ClassDB.InsertInTable(cmdInsertV, pars2);
                }

                buttonAddQuestion.IsEnabled = false;
                textBoxQuestion.Text = "Напишите вопрос...";
                textBoxQuestion.IsEnabled = true;
                question.Clear();
                dataGridQuestion.ItemsSource = null;

                FillAllDT();
                UpdateAllDG();
            }
        }
        private void buttonAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxUserLogin.Text.Trim() != ""
                && textBoxUserPassword.Text.Trim() != ""
                && textBoxUserName.Text.Trim() != ""
                && textBoxUserPhone.Text.Trim() != "")
            {
                string query = $"insert into Пользователи values(@login, HASHBYTES('SHA2_256', '{textBoxUserPassword.Text}'), @name, @phone, @role)";
                List<SqlParameter> pars = new List<SqlParameter>();

                pars.Add(new SqlParameter("login", textBoxUserLogin.Text));
                pars.Add(new SqlParameter("name", textBoxUserName.Text));
                pars.Add(new SqlParameter("phone", textBoxUserPhone.Text));
                pars.Add(new SqlParameter("role", ((ComboBoxItem)comboBoxRoles.SelectedItem).Content));

                ClassDB.InsertInTable(query, pars);

                textBoxUserLogin.Text = "";
                textBoxUserName.Text = "";
                textBoxUserPassword.Text = "";
                textBoxUserPhone.Text = "";

                FillAllDT();
                UpdateAllDG();
            }
        }
        private void buttonCreateTest_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxTestName.Text.Trim() != "" && textBoxDescription.Text.Trim() != "")
            {
                string strInsertTest = "insert into Тесты values (@par1, @par2, @par3)";

                List<SqlParameter> pars = new List<SqlParameter>();
                pars.Add(new SqlParameter("par1", textBoxTestName.Text));
                pars.Add(new SqlParameter("par2", textBoxDescription.Text));
                pars.Add(new SqlParameter("par3", (int)sliderCountValue.Value));

                ClassDB.InsertInTable(strInsertTest, pars);
                mainTestId = ClassDB.GetId($"select TOP(1) Код from Тесты where Название = '{textBoxTestName.Text}' ORDER BY Код DESC");

                labelTestInfo.Content = "Тест: " + textBoxTestName.Text;

                variantCount = (int)sliderCountValue.Value;
                ClassDB.FillComboBox("select Код, Название from Тесты", comboBoxTests, true);
                buttonAddDisk.IsEnabled = true;

                FillAllDT();
                UpdateAllDG();
            }
        }
        private void buttonAddDisk_Click(object sender, RoutedEventArgs e)
        {
            if (mainTestId != 0 && CheckDisk() && double.Parse(textBoxPointTo.Text) < double.Parse(textBoxPointDo.Text))
            {
                int idTest = mainTestId;
                string strInsertDisk = "insert into [Значения тестов] values (@par1, @par2, @par3, @par4)";

                List<SqlParameter> pars = new List<SqlParameter>();

                pars.Add(new SqlParameter("@par1", idTest));
                pars.Add(new SqlParameter("@par2", textBoxDisk.Text));
                pars.Add(new SqlParameter("@par3", double.Parse(textBoxPointTo.Text)));
                pars.Add(new SqlParameter("@par4", double.Parse(textBoxPointDo.Text)));

                ClassDB.InsertInTable(strInsertDisk, pars);

                labelErrorDisk.Visibility = Visibility.Hidden;

                FillAllDT();
                UpdateAllDG();

                textBoxDisk.Text = "Напишите значение...";
                textBoxPointTo.Text = "";
                textBoxPointDo.Text = "";
            }
            else
            {
                labelErrorDisk.Visibility = Visibility.Visible;
                labelErrorDisk.Content = "Данные некорректно введены";
                labelErrorDisk.Foreground = Brushes.Red;
            }
        }
        private void buttonChoice_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxFill cbf = (ComboBoxFill)comboBoxTests.SelectedItem;
            labelTestInfo.Content = "Тест: " + cbf.Name;

            mainTestId = cbf.Id;

            string countValues = "select [Кол-во_вариантов_ответа] from Тесты" +
                $" where Тесты.Код = {mainTestId}";

            buttonAddDisk.IsEnabled = true;

            string valueStr = ClassDB.GetScalary(countValues);
            variantCount = int.Parse(valueStr);
        }
        private void buttonEditRow_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEdit.Text.Trim() != "")
            {
                ComboBoxItem t = (ComboBoxItem)comboBoxTables.SelectedItem;
                string table = t.Content.ToString();

                string column = comboBoxFields.Text;

                string cmd = $"Update [{table}] set [{column}] = @par where Код = {int.Parse(labelCode.Content.ToString())}";

                double num;
                SqlParameter par;

                if (double.TryParse(textBoxEdit.Text, out num))
                {
                    par = new SqlParameter("par", num);
                }
                else
                {
                    par = new SqlParameter("par", textBoxEdit.Text);
                }

                ClassDB.UpdateDBTable(cmd, par);
                FillAllDT();
                UpdateAllDG();
                ClearInfoEdit();
            }
        }
        private void buttonEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxUserLoginEdit.Text.Trim() != ""
                && textBoxUserNameEdit.Text.Trim() != ""
                && textBoxUserPhoneEdit.Text.Trim() != "")
            {
                string query = $"update Пользователи set Логин = @login, ФИО = @name, Телефон = @phone, Роль = @role where Код = {int.Parse(labelUserCode.Content.ToString())}";

                List<SqlParameter> pars = new List<SqlParameter>();

                pars.Add(new SqlParameter("login", textBoxUserLoginEdit.Text));
                pars.Add(new SqlParameter("name", textBoxUserNameEdit.Text));
                pars.Add(new SqlParameter("phone", textBoxUserPhoneEdit.Text));
                pars.Add(new SqlParameter("role", ((ComboBoxItem)comboBoxRolesEdit.SelectedItem).Content));

                ClassDB.UpdateDBTable(query, pars);

                FillAllDT();
                UpdateAllDG();

                labelUserCode.Content = "Не выбрана строка";
                textBoxUserLoginEdit.Text = "";
                textBoxUserNameEdit.Text = "";
                textBoxUserPhoneEdit.Text = "";
                buttonDeleteUser.IsEnabled = false;
                buttonEditUser.IsEnabled = false;
            }
        }
        private void buttonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            ClassDB.DeleteRow($"delete from Пользователи where Код = {labelUserCode.Content}");

            FillAllDT();
            UpdateAllDG();

            labelUserCode.Content = "Не выбрана строка";
            textBoxUserLoginEdit.Text = "";
            textBoxUserNameEdit.Text = "";
            textBoxUserPhoneEdit.Text = "";
            buttonDeleteUser.IsEnabled = false;
        }
        private void buttonDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            ClassDB.DeleteRow($"delete from [{((ComboBoxItem)comboBoxTables.SelectedItem).Content}] where Код = {labelCode.Content}");

            labelCode.Content = "Строка не выбрана";

            ClearInfoEdit();

            curTable = ((ComboBoxItem)comboBoxTables.SelectedItem).Content.ToString();

            FillAllDT();

            UpdateAllDG();

        }
        private void buttonDeleteR_Click(object sender, RoutedEventArgs e)
        {
            ClassDB.DeleteRow($"delete from Уведомления where Код = {labelCodeR.Content}");

            FillAllDT();
            UpdateAllDG();

            buttonDeleteR.IsEnabled = false;
            buttonEditR.IsEnabled = false;
            textBoxEditR.Text = "";
            labelCodeR.Content = "Не выбрана строка";
        }
        private void buttonAddR_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxR.Text.Trim() != "")
            {
                string query = $"insert into Уведомления values ({labelCodeUserForR.Content}, {((ComboBoxFill)comboBoxTestsForR.SelectedItem).Id}, '{textBoxR.Text}', 'Отправлено')";

                ClassDB.UpdateDBTable(query);

                FillAllDT();
                UpdateAllDG();
            }
        }
        private void buttonEditR_Click(object sender, RoutedEventArgs e)
        {
            string query = $"update Уведомления set Текст = '{textBoxEditR.Text}' where Код = {labelCodeR.Content}";

            ClassDB.UpdateDBTable(query);

            FillAllDT();
            UpdateAllDG();

            buttonDeleteR.IsEnabled = false;
            buttonEditR.IsEnabled = false;
            textBoxEditR.Text = "";
            labelCodeR.Content = "Не выбрана строка";
        }
        private void buttonWord_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTestResult.SelectedIndex != -1)
            {
                MakeWorkerWord("Направление к психотерапевту");
            }
        }
        private void buttonLK_Click(object sender, RoutedEventArgs e)
        {
            WindowCabinet wc = new WindowCabinet(this, idUser);
            wc.Show();
            this.Hide();
        }
        private void buttonSendR_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxR.Text.Trim() != "")
            {
                for (int i = 0; i < dataGridUserForR.Items.Count; i++)
                {
                    string id = dtRU.DefaultView[i]["Код"].ToString();
                    string q = $"insert into Уведомления values ({id}, {((ComboBoxFill)comboBoxTestsForR.SelectedItem).Id}, '{textBoxR.Text}', 'Отправлено')";
                    ClassDB.UpdateDBTable(q);
                }

                FillAllDT();
                UpdateAllDG();
            }
        }

        /////////////////////////////////////////////////////////
        ////////////События работы с текст боксами///////////////
        /////////////////////////////////////////////////////////
        private void textBoxQuestion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                question.Clear();

                for (int i = 1; i <= variantCount; i++)
                {
                    question.Add(new ClassVariants(i, "", 0.0));
                }

                dataGridQuestion.ItemsSource = question;
                buttonAddQuestion.IsEnabled = true;
                textBoxQuestion.IsEnabled = false;
            }
        }
        private void textBoxPointDo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && e.Text != ",")
            {
                e.Handled = true;
            }
            else if (e.Text == "," && (textBoxPointDo.Text == "" || textBoxPointDo.Text.Contains(',')))
            {
                e.Handled = true;
            }
        }
        private void textBoxPointTo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && e.Text != ",")
            {
                e.Handled = true;
            }
            else if (e.Text == "," && (textBoxPointTo.Text == "" || textBoxPointTo.Text.Contains(',')))
            {
                e.Handled = true;
            }
        }
        private void textBoxEdit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (comboBoxFields.Text == "От" || comboBoxFields.Text == "До" || comboBoxFields.Text == "Балл")
            {
                textBoxEdit.MaxLength = 50;

                if (!Char.IsDigit(e.Text, 0) && e.Text != ",")
                {
                    e.Handled = true;
                }
                else if (e.Text == "," && (textBoxEdit.Text == "" || textBoxEdit.Text.Contains(',')))
                {
                    e.Handled = true;
                }
            }
            else if (comboBoxFields.Text == "Описание")
            {
                textBoxEdit.MaxLength = 1000;
            }
            else
            {
                textBoxEdit.MaxLength = 50;
            }

        }
        private void textBoxUserPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) || (textBoxUserPhone.Text == "" && e.Text != "8"))
            {
                e.Handled = true;
            }
        }
        private void textBoxUserLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (ClassDB.GetId($"select Код from Пользователи where Логин = '{textBoxUserLogin.Text}'") == -1)
            {
                buttonAddUser.IsEnabled = true;
            }
            else
            {
                buttonAddUser.IsEnabled = false;
            }
        }
        private void textBoxUserPhoneEdit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) || (textBoxUserPhoneEdit.Text == "" && e.Text != "8"))
            {
                e.Handled = true;
            }
        }

        /////////////////////////////////////////////////////////
        //////Методы и события для работы с комбобоксами/////////
        /////////////////////////////////////////////////////////
        private void comboBoxTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGridTables.ItemsSource = null;

            ComboBoxItem item = (ComboBoxItem)comboBoxTables.SelectedItem;

            string table = item.Content.ToString();

            curTable = table;

            dt = ClassDB.UpdateDataGridTable($"select * from [{table}]", "dt");

            dataGridTables.ItemsSource = dt.DefaultView;

            comboBoxFields.Items.Clear();

            ClearInfoEdit();

            string columnGet = "SELECT column_name as Столбцы" +
                " FROM INFORMATION_SCHEMA.COLUMNS " +
                $"WHERE TABLE_NAME = '{table}'; ";

            ClassDB.FillComboBox(columnGet, comboBoxFields, false);
        }
        private void comboBoxFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridTables.SelectedIndex != -1)
            {
                string field = comboBoxFields.SelectedValue.ToString();

                field = field.Replace("_", " ");

                string value = dt.DefaultView[dataGridTables.SelectedIndex][field].ToString();

                textBoxEdit.Text = value;
            }
        }
        private void comboBoxTestsForR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxTestsForR.SelectedIndex != -1)
            {
                labelTestForR.Content = ((ComboBoxFill)comboBoxTestsForR.SelectedItem).Name;
            }
        }

        /////////////////////////////////////////////////////////
        //////Методы и события для работы с датагрид/////////////
        /////////////////////////////////////////////////////////
        private void FillAllDT()
        {
            queryForResult = "select [Результаты тестов].[Пользователь] as [Код пользователя], " +
                   "Пользователи.ФИО, " +
                   "[Результаты тестов].[Код теста], " +
                   "Тесты.Название as [Тест], " +
                   "[Результаты тестов].Дата as Дата, " +
                   "[Результаты тестов].[Итого баллов], " +
                   "[Результаты тестов].Результат " +
                   "from[Результаты тестов] " +
                   "inner join Пользователи on[Результаты тестов].Пользователь = Пользователи.Код " +
                   "inner join Тесты on [Результаты тестов].[Код теста] = Тесты.Код";

            string queryR = "select Уведомления.Код, Пользователи.ФИО as [Пользователь], Тесты.Название as [Тест], Текст, Уведомления.Статус from Пользователи " +
               "inner join Уведомления on Пользователи.Код = Уведомления.[Код_пользователя] " +
               "inner join Тесты on Уведомления.Код_теста = Тесты.Код";

            dt = ClassDB.UpdateDataGridTable($"select * from [{curTable}]", "dt");
            dtU = ClassDB.UpdateDataGridTable("select * from Пользователи", "dtU");
            dtR = ClassDB.UpdateDataGridTable(queryR, "dtR");
            dtRU = ClassDB.UpdateDataGridTable("select Код, ФИО from Пользователи where Роль = 'Сотрудник'", "dtRU");
            dtTUR = ClassDB.UpdateDataGridTable(queryForResult, "dtTUR");

            if (dtTUR.Rows.Count != 0)
            {
                string qQr = "select Вопросы.Вопрос, [Варианты ответов].Вариант, [Варианты ответов].Балл " +
                "from[Значения вопросов] inner join[Варианты ответов] on[Варианты ответов].Код = [Значения вопросов].Код_варианта " +
                "inner join Вопросы on Вопросы.Код = [Варианты ответов].Код_вопроса " +
                $"where Код_пользователя = {dtTUR.DefaultView[0]["Код пользователя"]} and Дата = '{dtTUR.DefaultView[0]["Дата"]}' and Вопросы.Код_теста = {dtTUR.DefaultView[0]["Код теста"]}";

                dtQUR = ClassDB.UpdateDataGridTable(qQr, "dtQUR");
            }

        }
        private void UpdateAllDG()
        {
            dataGridTables.ItemsSource = dt.DefaultView;
            dataGridUsers.ItemsSource = dtU.DefaultView;
            dataGridR.ItemsSource = dtR.DefaultView;
            dataGridUserForR.ItemsSource = dtRU.DefaultView;
            dataGridTestResult.ItemsSource = dtTUR.DefaultView;

            if (dtTUR.Rows.Count > 0)
            {
                dataGridQuestResult.ItemsSource = dtQUR.DefaultView;
            }

            dt = ClassDB.UpdateDataGridTable($"select * from [{curTable}]", "dt");

            dataGridTables.ItemsSource = dt.DefaultView;

            comboBoxFields.Items.Clear();

            ClearInfoEdit();

            string columnGet = "SELECT column_name as Столбцы" +
                " FROM INFORMATION_SCHEMA.COLUMNS " +
                $"WHERE TABLE_NAME = '{curTable}'; ";

            ClassDB.FillComboBox(columnGet, comboBoxFields, false);
        }
        private void dataGridTables_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridTables.ItemsSource != null && dataGridTables.SelectedIndex != -1)
            {
                string s = dt.DefaultView[dataGridTables.SelectedIndex]["Код"].ToString();

                string field = comboBoxFields.Text;

                field = field.Replace("_", " ");

                string value = dt.DefaultView[dataGridTables.SelectedIndex][field].ToString();
                labelCode.Content = s;
                buttonDeleteRow.IsEnabled = true;
                buttonEditRow.IsEnabled = true;
                textBoxEdit.Text = value;
            }
        }
        private void dataGridUsers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridUsers.ItemsSource != null && dataGridUsers.SelectedIndex != -1)
            {
                string s = dtU.DefaultView[dataGridUsers.SelectedIndex]["Код"].ToString();

                labelUserCode.Content = s;
                buttonDeleteUser.IsEnabled = true;
                buttonEditUser.IsEnabled = true;

                string login = dtU.DefaultView[dataGridUsers.SelectedIndex]["Логин"].ToString();
                string name = dtU.DefaultView[dataGridUsers.SelectedIndex]["ФИО"].ToString();
                string phone = dtU.DefaultView[dataGridUsers.SelectedIndex]["Телефон"].ToString();
                string role = dtU.DefaultView[dataGridUsers.SelectedIndex]["Роль"].ToString();

                textBoxUserLoginEdit.Text = login;
                textBoxUserNameEdit.Text = name;
                textBoxUserPhoneEdit.Text = phone;
                comboBoxRolesEdit.Text = role;

            }
        }
        private void dataGridUserForR_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridUserForR.ItemsSource != null && dataGridUserForR.SelectedIndex != -1)
            {
                string s = dtRU.DefaultView[dataGridUserForR.SelectedIndex]["Код"].ToString();
                labelCodeUserForR.Content = s;
                buttonAddR.IsEnabled = true;
            }
            else if (dataGridUserForR.SelectedIndex == -1)
            {
                labelCodeUserForR.Content = "Не выбрана строка";
                buttonAddR.IsEnabled = false;
            }
        }
        private void dataGridR_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridR.ItemsSource != null && dataGridR.SelectedIndex != -1)
            {
                string s = dtR.DefaultView[dataGridR.SelectedIndex]["Код"].ToString();

                labelCodeR.Content = s;
                buttonDeleteR.IsEnabled = true;
                buttonEditR.IsEnabled = true;

                textBoxEditR.Text = dtR.DefaultView[dataGridR.SelectedIndex]["Текст"].ToString();

            }
        }

        //Метод для скрытия столбцов в DataGrid
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Код пользователя" || propertyDescriptor.DisplayName == "Код теста" || propertyDescriptor.DisplayName == "Пароль")
            {
                e.Cancel = true;
            }
        }
        private void dataGridTestResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridTestResult.SelectedIndex != -1)
            {
                buttonWord.IsEnabled = true;
                labelDate.Content = dtTUR.DefaultView[dataGridTestResult.SelectedIndex]["Дата"].ToString();
                labelUserResult.Content = dtTUR.DefaultView[dataGridTestResult.SelectedIndex]["ФИО"].ToString();

                string qQr = "select Вопросы.Вопрос, [Варианты ответов].Вариант, [Варианты ответов].Балл " +
                    "from[Значения вопросов] inner join[Варианты ответов] on[Варианты ответов].Код = [Значения вопросов].Код_варианта " +
                    "inner join Вопросы on Вопросы.Код = [Варианты ответов].Код_вопроса " +
                    $"where Код_пользователя = {dtTUR.DefaultView[dataGridTestResult.SelectedIndex]["Код пользователя"]} " +
                    $"and Дата = '{dtTUR.DefaultView[dataGridTestResult.SelectedIndex]["Дата"]}' " +
                    $"and Вопросы.Код_теста = {dtTUR.DefaultView[dataGridTestResult.SelectedIndex]["Код теста"]}";

                dtQUR = ClassDB.UpdateDataGridTable(qQr, "dtQUR");
                dataGridQuestResult.ItemsSource = dtQUR.DefaultView;

            }
        }
        private void dataGridQuestion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (dataGridQuestion.CurrentColumn.Header.ToString() == "Кол-во баллов")
            {
                string s = dataGridQuestion.Columns[2].GetCellContent(dataGridQuestion.Items[dataGridQuestion.SelectedIndex]).ToString();

                s = s.Replace("System.Windows.Controls.TextBox: ", "");

                if (!Char.IsDigit(e.Text, 0) && e.Text != ".")
                {
                    e.Handled = true;
                }
                else if (e.Text == "." && (s == "" || s.Contains('.')))
                {
                    e.Handled = true;
                }
                
            }
        }
    }
}

