using System;

namespace Kursach
{
    public class TypeWorkModel
    {
        public int ID_typework { get; set; }
        public int ID_person { get; set; }
        public string Person_t { get; set; }
        public string Work_t { get; set; }
        public DateTime Date_start { get; set; }
        public DateTime Date_end { get; set; }
    }
}
