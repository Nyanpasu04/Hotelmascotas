using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMascotasConsoleApp.Clases
{
    public class Duenio
    {
        public string NombreDuenio { set; get; }

        public string Rut { set; get; }

        public string Correo { set; get; }

        public string Direccion { set; get; }

        public string Telefono { set; get; }

        public Int32 IDReserva { set; get; }  

        public Duenio() { }

        public Duenio GetDuenio(Int32 idReserva, List<Duenio> listDuenios)
        {
            return listDuenios.Where(x => x.IDReserva == idReserva).ToList()[0];
        }
    }
}
