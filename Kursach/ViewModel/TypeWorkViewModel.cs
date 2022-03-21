using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Kursach
{
    internal class TypeWorkViewModel
    {
        public List<TypeWorkModel> GetAllTypeWork()
        {
            List<TypeWorkModel> output = new List<TypeWorkModel>();
            SQLRequest request = new SQLRequest();
            string qwery = "SELECT TypeWork.* " +
                "FROM[TypeWork];";
            OleDbDataReader reader = request.Select(qwery);
            while (reader.Read())
            {
                TypeWorkModel typework = new TypeWorkModel();
                typework.ID_typework = Convert.ToInt32(reader.GetValue(0));
                typework.ID_person = Convert.ToInt32(reader.GetValue(1));
                typework.Person_t = Convert.ToString(reader.GetValue(2));
                typework.Work_t = Convert.ToString(reader.GetValue(3));
                typework.Date_start = Convert.ToDateTime(reader.GetValue(4));
                typework.Date_end = Convert.ToDateTime(reader.GetValue(5));
                output.Add(typework);
            }
            reader.Close();
            request.Close();
            return output;
        }
    }
}
