using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMascotasConsoleApp.Clases
{
    public class Reserva
    {
        public Int32 ID { set; get; }

        public string TipoReserva { set; get; }

        public string Costo { set; get; }

        public string Duracion { set; get; }

        public Reserva() { }

        public Int32 newId(List<Reserva> listReserva)
        {
            var resultado = 0;

            if (listReserva != null) {
                var ultimoID = 0;

                if (listReserva.OrderByDescending(x => x.ID).ToList().Count() > 0)
                {
                    ultimoID = listReserva.OrderByDescending(x => x.ID).ToList()[0].ID;
                }

                resultado = ultimoID + 1;
            }
            else
            {
                resultado = 1;
            }
            return resultado;
        }
    }
}
