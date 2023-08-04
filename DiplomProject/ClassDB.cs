using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace DiplomProject
{
    public static class ClassDB
    {
        private static string connectionStr = "Data Source = localhost; Initial catalog = UKkantele; Integrated Security = True;";

        public static List<string> GetReader(string query)
        {
            try
            {
                List<string> res = new List<string>();
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // построчно считываем данные.
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                res.Add(reader[i].ToString());
                            }
                        }
                        reader.Close();
                        sqlConnection.Close();
                        return res;
                        
                    }
                    sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public static List<string> GetReader(string query, List<SqlParameter> pars)
        {
            try
            {
                List<string> res = new List<string>();
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);

                    foreach (SqlParameter par in pars)
                    {
                        cmd.Parameters.Add(par);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.HasRows)
                    {
                        // построчно считываем данные.
                        while (reader.Read())
                        {
                            for(int i = 0; i < reader.FieldCount; i++)
                            {
                                res.Add(reader[i].ToString());
                            }
                        }
                        reader.Close();
                        sqlConnection.Close();
                        return res;

                    }
                    sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public static string GetScalary(string query)
        {
            try
            {
                string value = "";

                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmdGet = new SqlCommand(query, sqlConnection);

                    if (cmdGet.ExecuteScalar() != null)
                    {
                        value = cmdGet.ExecuteScalar().ToString();
                    }
                    sqlConnection.Close();
                }
                return value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public static string GetScalary(string query, List<SqlParameter> pars)
        {
            try
            {
                string value = "";

                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmdGet = new SqlCommand(query, sqlConnection);

                    foreach (SqlParameter par in pars)
                    {
                        cmdGet.Parameters.Add(par);
                    }

                    if(cmdGet.ExecuteScalar() != null)
                    {
                        value = cmdGet.ExecuteScalar().ToString();
                    }

                    sqlConnection.Close();
                }
                return value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }


        }
        public static int GetId(string command)
        {
            int id = -1;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmdGetId = new SqlCommand(command, sqlConnection);

                    var value = cmdGetId.ExecuteScalar();

                    if (value != null)
                    {
                        int.TryParse(value.ToString(), out id);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return id;
        }
        public static void InsertInTable(string command, List<SqlParameter> pars)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmdInsert = new SqlCommand(command, sqlConnection);

                    foreach (SqlParameter par in pars)
                    {
                        cmdInsert.Parameters.Add(par);
                    }

                    cmdInsert.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void DeleteRow(string query)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmdDelete = new SqlCommand(query, sqlConnection);

                    cmdDelete.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void UpdateDBTable(string query)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);

                    cmd.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void UpdateDBTable(string query, SqlParameter par)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);

                    cmd.Parameters.Add(par);

                    cmd.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void UpdateDBTable(string query, List<SqlParameter> pars)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);

                    foreach (SqlParameter par in pars)
                    {
                        cmd.Parameters.Add(par);
                    }

                    cmd.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static DataTable UpdateDataGridTable(string query, string table)
        {
            DataTable dt;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);

                    cmd.ExecuteNonQuery();

                    SqlDataAdapter dataAdp = new SqlDataAdapter(cmd);
                    dt = new DataTable(table); // В скобках указываем название таблицы

                    dataAdp.Fill(dt);

                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.ColumnName.Contains("_"))
                        {
                            c.ColumnName = c.ColumnName.Replace("_", " ");
                        }
                    }

                    sqlConnection.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public static void FillComboBox(string query, ComboBox cb, bool twoValues)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);

                    List<ComboBoxFill> cbItem = new List<ComboBoxFill>();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (twoValues)
                    {
                        int testId = 0;
                        string testName = "";
                        while (reader.Read())
                        {
                            testId = int.Parse(reader[0].ToString());
                            testName = reader[1].ToString();
                            cbItem.Add(new ComboBoxFill(testId, testName));
                        }

                        cb.DisplayMemberPath = "Name";
                        cb.ItemsSource = cbItem;
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            if (reader[0].ToString() != "Код" && reader[0].ToString() != "Код_вопроса" && reader[0].ToString() != "Код_теста" && reader[0].ToString() != "Кол-во_вариантов_ответа")
                            {
                                cb.Items.Add(reader[0].ToString());
                            }
                        }
                    }

                    cb.SelectedIndex = 0;

                    reader.Close();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка системы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
