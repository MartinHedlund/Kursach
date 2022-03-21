using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Kursach
{
    internal class PersonViewModel
    {
        public List<PersonModel> GetFreePeople() // возвращает список свободных людей
        {
            List<PersonModel> output = new List<PersonModel>();
            SQLRequest request = new SQLRequest();
            string qwery = "SELECT * " +
                "FROM Person " +
                "WHERE(((Person.zan) = False))";
            OleDbDataReader reader = request.Select(qwery);
            while (reader.Read())
            {
                PersonModel person = new PersonModel();
                person.P_id = Convert.ToInt32(reader.GetValue(0));
                person.FIO = Convert.ToString(reader.GetValue(1));
                person.Salary = Convert.ToDouble(reader.GetValue(2));
                person.Zan = Convert.ToBoolean(reader.GetValue(3));
                person.Dop = Convert.ToDouble(reader.GetValue(4));
                output.Add(person);
            }
            reader.Close();
            request.Close();
            return output;
        }
        public List<PersonModel> GetAllPeople() // возвращает список всех сотрудников
        {
            List<PersonModel> output = new List<PersonModel>();
            SQLRequest request = new SQLRequest();
            string qwery = "SELECT * " +
                "FROM Person ";
            OleDbDataReader reader = request.Select(qwery);
            while (reader.Read())
            {
                PersonModel person = new PersonModel();
                person.P_id = Convert.ToInt32(reader.GetValue(0));
                person.FIO = Convert.ToString(reader.GetValue(1));
                person.Salary = Convert.ToDouble(reader.GetValue(2));
                person.Zan = Convert.ToBoolean(reader.GetValue(3));
                person.Dop = Convert.ToDouble(reader.GetValue(4));
                output.Add(person);
            }
            reader.Close();
            request.Close();
            return output;
        }
    }
}
