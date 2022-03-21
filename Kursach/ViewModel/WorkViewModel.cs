using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Kursach
{
    internal class WorkViewModel
    {
        public List<WorkModel> GetAllWork()
        {
            List<WorkModel> output = new List<WorkModel>();
            SQLRequest request = new SQLRequest();
            string qwery = "SELECT Work.* " +
                "FROM[Work];";
            OleDbDataReader reader = request.Select(qwery);
            while (reader.Read())
            {
                WorkModel work = new WorkModel();
                work.Id_work = Convert.ToInt32(reader.GetValue(0));
                work.Discription = Convert.ToString(reader.GetValue(1));
                work.Payment_per_day = Convert.ToDouble(reader.GetValue(2));
                output.Add(work);
            }
            reader.Close();
            request.Close();
            return output;
        }
    }
}
