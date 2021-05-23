using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMascotasConsoleApp.Clases
{
    public class Mascota
    {
        public string NombreMascota { set; get; }

        public string Raza { set; get; }

        public string Anios { set; get; }

        public bool Vacunas { set; get; }

        public Int32 IDReserva { set; get; }

        public Mascota() { }

        public Mascota GetMascota(Int32 idReserva, List<Mascota> listMascotas)
        {
            return listMascotas.Where(x => x.IDReserva == idReserva).ToList()[0];
        }
    }
}
