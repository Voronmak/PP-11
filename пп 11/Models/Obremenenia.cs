using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пп_11.Models
{
    internal class Obremenenia
    {
        public int Id { get; set; }

        public string TypeOfObremenenia { get; set; }

        public int NumberOfRegistration { get; set; }

        public DateTime DataOfRegistration { get; set; }

        public string? Discribe { get; set; }

        public string YstanovlFace { get; set; }

        public string Status { get; set; }

        public int IdRight { get; set; }

        [ForeignKey(nameof(IdRight))]
        public Right? Right { get; set; }

        public Obremenenia(int id, string typeOfObremenenia, int numberOfRegistration, DateTime dataOfRegistration, string? discribe, string ystanovlFace, string status, int idRight)
        {
            Id = id;
            TypeOfObremenenia = typeOfObremenenia;
            NumberOfRegistration = numberOfRegistration;
            DataOfRegistration = dataOfRegistration;
            Discribe = discribe;
            YstanovlFace = ystanovlFace;
            Status = status;
            IdRight = idRight;
        }

        public Obremenenia() { }
    }
}
