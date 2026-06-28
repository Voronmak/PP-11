using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пп_11.Models
{
    internal class Right
    {
        public int Id { get; set; }

        public int NumberOfRegistration { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public string DocumentOsnovanie { get; set; }

        public bool StatusOfRight { get; set; }

        public int IdGroundPlace { get; set; }

        [ForeignKey(nameof(IdGroundPlace))]
        public GroundPlace? GroundPlace { get; set; }

        public int IdPravoobladateli { get; set; }

        [ForeignKey(nameof(IdPravoobladateli))]
        public Pravoobladateli? Pravoobladateli { get; set; }

        public int IdTypeOfRight { get; set; }

        [ForeignKey(nameof(IdTypeOfRight))]
        public TypeOfRight? TypeOfRight { get; set; }

        public Right(int id, int numberOfRegistration, DateTime dateOfRegistration, string documentOsnovanie, bool statusOfRight, int idGroundPlace, int idPravoobladateli, int idTypeOfRight)
        {
            Id = id;
            NumberOfRegistration = numberOfRegistration;
            DateOfRegistration = dateOfRegistration;
            DocumentOsnovanie = documentOsnovanie;
            StatusOfRight = statusOfRight;
            IdGroundPlace = idGroundPlace;
            IdPravoobladateli = idPravoobladateli;
            IdTypeOfRight = idTypeOfRight;
        }

        public Right() { }
    }
}
