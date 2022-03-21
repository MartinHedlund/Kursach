using System;
using System.Data.OleDb;
using System.Windows;

namespace Kursach
{
    internal class SQLRequest
    {
        public static string connection = @"Provider=Microsoft.Jet.OLEDB.4.0; 
        Data Source=E:\Труды Рабочей Видры\Уник\Второй курс\1 семак\МСППО(ППО)\Курсач\Kursach\Kursach\bin\Debug\DateBase.mdb"; // путь к базе данных

        private OleDbConnection connect;
        public SQLRequest()
        {
            connect = new OleDbConnection(connection); // подключаемся к базе данных
        }
        public OleDbDataReader Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            connect.Open(); // открываем базу данных

            OleDbCommand cmd = new OleDbCommand(selectSQL, connect); // создаём запрос
            OleDbDataReader reader = cmd.ExecuteReader(); // получаем данные
            return reader; // возвращаем
        }
        public void Update(string updateSQL)
        {
            connect.Open(); // открываем базу данных
            OleDbCommand cmd = new OleDbCommand(updateSQL, connect);
            cmd.ExecuteNonQuery();
            connect.Close();
        }
        public void Insert(string insertSQL)
        {
            try
            {
                connect.Open(); // открываем базу данных
                OleDbCommand cmd = new OleDbCommand(insertSQL, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Insert");
                connect.Close();
            }
        }
        public void Delete(string deleteSQL)
        {
            try
            {
                connect.Open(); // открываем базу данных
                OleDbCommand cmd = new OleDbCommand(deleteSQL, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Delete");
                connect.Close();
            }
        }
        public void UpdateNEZan()
        {

            string qwery2 = $"UPDATE Person INNER JOIN TypeWork ON Person.p_id = TypeWork.id_person SET Person.zan = 0 " +
                $"WHERE(((Person.zan) = True) AND((Now()) >[TypeWork].[data_end]))";

            try
            {
                connect.Close();
                connect.Open(); // открываем базу данных
                OleDbCommand cmd = new OleDbCommand(qwery2, connect); // освобождает
                cmd.Parameters.Add("@zan", OleDbType.VarChar).Value = 0;
                int k = cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " UpdateNEZAN");
                connect.Close();
            }
        }

        public void UpdateZan()
        {
            string qwery = $"UPDATE Person INNER JOIN TypeWork ON Person.p_id = TypeWork.id_person SET Person.zan = 1 " +
                $"WHERE(((Person.zan) = 0) AND((Now()) <[TypeWork].[data_end]))";

            try
            {
                connect.Close();
                connect.Open(); // открываем базу данных
                OleDbCommand cmd = new OleDbCommand(qwery, connect); //занимает
                cmd.Parameters.Add("@zan", OleDbType.VarChar).Value = 1;
                int k = cmd.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " UpdateZAN");
                connect.Close();
            }
        }
        public void Close()
        {
            connect.Close();
        }

    }
}
