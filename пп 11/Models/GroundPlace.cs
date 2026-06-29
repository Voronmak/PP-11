using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пп_11.Models
{
    internal class GroundPlace
    {
        public int Id { get; set; }

        public int KadastrNumber { get; set; }

        public string Addres { get; set; }

        public double Square { get; set; }

        public string TypeOfGround { get; set; }

        public double KadastrPrice { get; set; }

        public string Status { get; set; }

        public GroundPlace(int kadastrNumber, string addres, double square, string typeOfGround, double kadastrPrice, string status)
        {
            KadastrNumber = kadastrNumber;
            Addres = addres;
            Square = square;
            TypeOfGround = typeOfGround;
            KadastrPrice = kadastrPrice;
            Status = status;
        }

        public GroundPlace() { }
    }
}
