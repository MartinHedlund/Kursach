namespace Kursach
{
    public class PersonModel
    {
        // поля
        private double dop = 0;

        // Свойства
        public int P_id { get; set; }
        public string FIO { get; set; }
        public double Salary { get; set; }
        public bool Zan { get; set; }
        public double Dop
        {
            get
            { return dop;}
            set
            { dop = value;}

        }
        public double Itog
        {
            get
            {return dop + Salary;}
        }
    }

}