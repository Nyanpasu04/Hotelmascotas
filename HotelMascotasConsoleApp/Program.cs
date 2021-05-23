using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotelMascotasConsoleApp
{
    class Program
    {
        public List<Clases.Reserva> ListReservas = new List<Clases.Reserva>();
        public List<Clases.Duenio> ListDuenios = new List<Clases.Duenio>();
        public List<Clases.Mascota> ListMascotas = new List<Clases.Mascota>();
        public string[] Campos = { "Rut", "Tipo de reserva", "Duracion", "Costo", "Nombre del dueño", "Correo", "Direccion", "Telefono", "Nombre de la mascota", "Raza", "Años", "Vacunas" };
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DB.txt");

        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.LeerData();
            prog.MenuInicial();
        }

        public void MenuInicial()
        {
            Console.Clear();
            Console.WriteLine("Bienvenido a nuestro Hotel, ingrese el numero de la opcion que desea \n");
            Console.WriteLine("1.- Agregar reservas \n");
            Console.WriteLine("2.- Modificar reservas \n");
            Console.WriteLine("3.- Eliminar reservas \n");
            Console.WriteLine("4.- Buscar reservas \n");
            Console.WriteLine("5.- Salir \n");

            var x = Console.ReadLine();

            try
            {
                var opcion = Convert.ToInt32(x);

                switch (opcion)
                {
                    case 1:
                        Agregar();
                        break;
                    case 2:
                        Modificar();
                        break;
                    case 3:
                        Eliminar();
                        break;
                    case 4:
                        Console.WriteLine("Ingrese el rut del dueño \n");
                        Console.WriteLine("*Si desea listar todos, solo precione enter");
                        var nom = Console.ReadLine();
                        Listar(new Clases.Reserva(), nom);
                        break;
                    case 5:
                        GuardarData();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("A ocurrido un error, si persiste cominiquese con el administrador \n");
                Console.WriteLine("Precione enter para continuar \n");
                Console.ReadLine();
                MenuInicial();
            }
        }

        public void LeerData()
        {
            var text = File.ReadAllText(path);
            var Listas = text.Split(new string[] { "---///---" }, StringSplitOptions.None);

            for (var i = 0; Listas.Length > i; i++)
            {
                if (!string.IsNullOrEmpty(Listas[i]))
                {
                    var lista = Listas[i].Split(new string[] { "*-*" }, StringSplitOptions.None);

                    foreach (var item in lista)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var propiedades = item.Split(new string[] { "-;-" }, StringSplitOptions.None);

                            var reserva = new Clases.Reserva();
                            var duenio = new Clases.Duenio();
                            var mascota = new Clases.Mascota();

                            switch (i)
                            {
                                case 0:
                                    reserva.ID = Convert.ToInt32(propiedades[0]);
                                    reserva.TipoReserva = propiedades[1];
                                    reserva.Costo = propiedades[2];
                                    reserva.Duracion = propiedades[3];

                                    ListReservas.Add(reserva);
                                    break;
                                case 1:
                                    duenio.NombreDuenio = propiedades[0];
                                    duenio.Rut = propiedades[1];
                                    duenio.Correo = propiedades[2];
                                    duenio.Direccion = propiedades[3];
                                    duenio.Telefono = propiedades[4];
                                    duenio.IDReserva = Convert.ToInt32(propiedades[5]);

                                    ListDuenios.Add(duenio);
                                    break;
                                case 2:
                                    mascota.NombreMascota = propiedades[0];
                                    mascota.Raza = propiedades[1];
                                    mascota.Anios = propiedades[2];
                                    mascota.Vacunas = Convert.ToBoolean(propiedades[3]);
                                    mascota.IDReserva = Convert.ToInt32(propiedades[4]);

                                    ListMascotas.Add(mascota);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void GuardarData()
        {
            File.WriteAllText(path, String.Empty);

            if(ListReservas.Count() > 0)
            {
                foreach (var reserva in ListReservas)
                {
                    File.AppendAllText(path, reserva.ID + "-;-" + reserva.TipoReserva + "-;-" + reserva.Costo + "-;-" + reserva.Duracion);
                    File.AppendAllText(path, "*-*");
                }
            }
            File.AppendAllText(path, "---///---");

            if (ListDuenios.Count() > 0)
            {
                foreach (var duenio in ListDuenios)
                {
                    File.AppendAllText(path, duenio.NombreDuenio + "-;-" + duenio.Rut + "-;-" + duenio.Correo + "-;-" + duenio.Direccion + "-;-" + duenio.Telefono + "-;-" + duenio.IDReserva);
                    File.AppendAllText(path, "*-*");
                }
            }
            File.AppendAllText(path, "---///---");

            if (ListMascotas.Count() > 0)
            {
                foreach (var mascota in ListMascotas)
                {
                    File.AppendAllText(path, mascota.NombreMascota + "-;-" + mascota.Raza + "-;-" + mascota.Anios + "-;-" + mascota.Vacunas + "-;-" + mascota.IDReserva);
                    File.AppendAllText(path, "*-*");
                }
            }
            File.AppendAllText(path, "---///---");
        }

        public void Agregar()
        {
            var reserva = new Clases.Reserva();
            var duenio = new Clases.Duenio();
            var mascota = new Clases.Mascota();

            var idReserva = reserva.newId(ListReservas);
            reserva.ID = idReserva;
            duenio.IDReserva = idReserva;
            mascota.IDReserva = idReserva;

            Console.Clear();

            Console.WriteLine("Menu de ingreso de reserva \n");

            foreach (var campo in Campos)
            {
                Console.WriteLine("Ingrese " + campo);

                var x = Console.ReadLine();

                if (string.IsNullOrEmpty(x))
                {
                    x = "sin datos";
                }

                switch (campo)
                {
                    case "Tipo de reserva":
                        reserva.TipoReserva = x;
                        break;
                    case "Duracion":
                        reserva.Duracion = x;
                        break;
                    case "Costo":
                        reserva.Costo = x;
                        break;
                    case "Nombre del dueño":
                        duenio.NombreDuenio = x;
                        break;
                    case "Rut":
                        if (ListDuenios != null)
                        {
                            var existe = ListDuenios.Where(y => y.Rut == x).ToList();
                            if (existe.Count() == 0)
                            {
                                duenio.Rut = x;
                            }
                            else
                            {
                                Console.WriteLine("El Rut Ingresado ya existe. \n");
                                Console.ReadLine();
                                MenuInicial();
                            }
                        }
                        else
                        {
                            duenio.Rut = x;
                        }
                        break;
                    case "Correo":
                        duenio.Correo = x;
                        break;
                    case "Direccion":
                        duenio.Direccion = x;
                        break;
                    case "Telefono":
                        duenio.Telefono = x;
                        break;
                    case "Nombre de la mascota":
                        mascota.NombreMascota = x;
                        break;
                    case "Raza":
                        mascota.Raza = x;
                        break;
                    case "Años":
                        mascota.Anios = x;
                        break;
                    case "Vacunas":
                        if (x.ToLower() == "si")
                        {
                            mascota.Vacunas = true;
                        }
                        else
                        {
                            mascota.Vacunas = false;
                        }
                        break;
                }
            }

            Console.WriteLine("¿Esta seguro que desea agregar? S/N \n");

            var confirmar = Console.ReadLine();

            if (confirmar.ToLower() == "s")
            {
                ListReservas.Add(reserva);
                ListDuenios.Add(duenio);
                ListMascotas.Add(mascota);
            }

            MenuInicial();
        }

        public void Listar(Clases.Reserva reserva, string rut)
        {
            if (reserva.ID != 0)
            {
                var macotas = new Clases.Mascota().GetMascota(reserva.ID, ListMascotas);
                var duenio = new Clases.Duenio().GetDuenio(reserva.ID, ListDuenios);

                #region
                foreach (var campo in Campos)
                {
                    switch (campo)
                    {
                        case "Tipo de reserva":
                            Console.WriteLine("Tipo de reserva: " + reserva.TipoReserva + "\n");
                            break;
                        case "Duracion":
                            Console.WriteLine("Duracion: " + reserva.Duracion + "\n");
                            break;
                        case "Costo":
                            Console.WriteLine("Costo: " + reserva.Costo + "\n");
                            break;
                        case "Nombre del dueño":
                            Console.WriteLine("Nombre del dueño: " + duenio.NombreDuenio + "\n");
                            break;
                        case "Rut":
                            Console.WriteLine("Rut: " + duenio.Rut + "\n");
                            break;
                        case "Correo":
                            Console.WriteLine("Correo: " + duenio.Correo + "\n");
                            break;
                        case "Direccion":
                            Console.WriteLine("Direccion: " + duenio.Direccion + "\n");
                            break;
                        case "Telefono":
                            Console.WriteLine("Telefono: " + duenio.Telefono + "\n");
                            break;
                        case "Nombre de la mascota":
                            Console.WriteLine("Nombre de la mascota: " + macotas.NombreMascota + "\n");
                            break;
                        case "Raza":
                            Console.WriteLine("Raza: " + macotas.Raza + "\n");
                            break;
                        case "Años":
                            Console.WriteLine("Años: " + macotas.Anios + "\n");
                            break;
                        case "Vacunas":
                            var vacunas = "";
                            if (macotas.Vacunas == true)
                            {
                                vacunas = "Si";
                            }
                            else
                            {
                                vacunas = "No";
                            }

                            Console.WriteLine("Vacunas: " + vacunas + "\n");
                            break;
                    }
                }
                #endregion
            }
            else if (!string.IsNullOrEmpty(rut))
            {
                if (ListDuenios != null)
                {
                    var duenios = ListDuenios.Where(y => y.Rut.ToLower().Contains(rut.ToLower())).ToList();

                    if (duenios.Count() > 0)
                    {
                        var duenio = duenios[0];
                        var rev = ListReservas.Where(y => y.ID == duenio.IDReserva).ToList();

                        if (rev.Count() > 0)
                        {
                            #region
                            foreach (var r in rev)
                            {
                                var macotas = new Clases.Mascota().GetMascota(r.ID, ListMascotas);

                                foreach (var campo in Campos)
                                {
                                    switch (campo)
                                    {
                                        case "Tipo de reserva":
                                            Console.WriteLine("Tipo de reserva: " + r.TipoReserva + "\n");
                                            break;
                                        case "Duracion":
                                            Console.WriteLine("Duracion: " + r.Duracion + "\n");
                                            break;
                                        case "Costo":
                                            Console.WriteLine("Costo: " + r.Costo + "\n");
                                            break;
                                        case "Nombre del dueño":
                                            Console.WriteLine("Nombre del dueño: " + duenio.NombreDuenio + "\n");
                                            break;
                                        case "Rut":
                                            Console.WriteLine("Rut: " + duenio.Rut + "\n");
                                            break;
                                        case "Correo":
                                            Console.WriteLine("Correo: " + duenio.Correo + "\n");
                                            break;
                                        case "Direccion":
                                            Console.WriteLine("Direccion: " + duenio.Direccion + "\n");
                                            break;
                                        case "Telefono":
                                            Console.WriteLine("Telefono: " + duenio.Telefono + "\n");
                                            break;
                                        case "Nombre de la mascota":
                                            Console.WriteLine("Nombre de la mascota: " + macotas.NombreMascota + "\n");
                                            break;
                                        case "Raza":
                                            Console.WriteLine("Raza: " + macotas.Raza + "\n");
                                            break;
                                        case "Años":
                                            Console.WriteLine("Años: " + macotas.Anios + "\n");
                                            break;
                                        case "Vacunas":
                                            var vacunas = "";
                                            if (macotas.Vacunas == true)
                                            {
                                                vacunas = "Si";
                                            }
                                            else
                                            {
                                                vacunas = "No";
                                            }

                                            Console.WriteLine("Vacunas: " + vacunas + "\n");
                                            break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron coincidencias \n");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron coincidencias \n");
                }
            }
            else
            {
                #region
                foreach (var rev in ListReservas)
                {
                    var macotas = new Clases.Mascota().GetMascota(rev.ID, ListMascotas);
                    var duenio = new Clases.Duenio().GetDuenio(rev.ID, ListDuenios);

                    foreach (var campo in Campos)
                    {
                        switch (campo)
                        {
                            case "Tipo de reserva":
                                Console.WriteLine("Tipo de reserva: " + rev.TipoReserva + "\n");
                                break;
                            case "Duracion":
                                Console.WriteLine("Duracion: " + rev.Duracion + "\n");
                                break;
                            case "Costo":
                                Console.WriteLine("Costo: " + rev.Costo + "\n");
                                break;
                            case "Nombre del dueño":
                                Console.WriteLine("Nombre del dueño: " + duenio.NombreDuenio + "\n");
                                break;
                            case "Rut":
                                Console.WriteLine("Rut: " + duenio.Rut + "\n");
                                break;
                            case "Correo":
                                Console.WriteLine("Correo: " + duenio.Correo + "\n");
                                break;
                            case "Direccion":
                                Console.WriteLine("Direccion: " + duenio.Direccion + "\n");
                                break;
                            case "Telefono":
                                Console.WriteLine("Telefono: " + duenio.Telefono + "\n");
                                break;
                            case "Nombre de la mascota":
                                Console.WriteLine("Nombre de la mascota: " + macotas.NombreMascota + "\n");
                                break;
                            case "Raza":
                                Console.WriteLine("Raza: " + macotas.Raza + "\n");
                                break;
                            case "Años":
                                Console.WriteLine("Años: " + macotas.Anios + "\n");
                                break;
                            case "Vacunas":
                                var vacunas = "";
                                if (macotas.Vacunas == true)
                                {
                                    vacunas = "Si";
                                }
                                else
                                {
                                    vacunas = "No";
                                }

                                Console.WriteLine("Vacunas: " + vacunas + "\n");
                                break;
                        }
                    }

                    Console.WriteLine("---///--- \n");
                }
                #endregion
            }

            Console.WriteLine("Precione enter para continuar \n");
            Console.ReadLine();

            MenuInicial();
        }

        public void Eliminar()
        {
            var idReserva = 0;
            Console.Clear();

            Console.WriteLine("Menu de eliminacion de reserva \n");
            Console.WriteLine("Ingrese el rut del dueño que realizo la reserva \n");

            var rut = Console.ReadLine();

            if (!string.IsNullOrEmpty(rut))
            {
                if (ListDuenios != null)
                {
                    var duenio = ListDuenios.Where(x => x.Rut == rut).ToList()[0];
                    var rev = ListReservas.Where(x => x.ID == duenio.IDReserva).ToList();

                    if (rev.Count() > 0)
                    {
                        Console.WriteLine("Se a encontrado la siguiente reserva \n");
                        var reserva = rev[0];

                        idReserva = reserva.ID;

                        var macotas = new Clases.Mascota().GetMascota(reserva.ID, ListMascotas);

                        foreach (var campo in Campos)
                        {
                            switch (campo)
                            {
                                case "Tipo de reserva":
                                    Console.WriteLine("Tipo de reserva: " + reserva.TipoReserva + "\n");
                                    break;
                                case "Duracion":
                                    Console.WriteLine("Duracion: " + reserva.Duracion + "\n");
                                    break;
                                case "Costo":
                                    Console.WriteLine("Costo: " + reserva.Costo + "\n");
                                    break;
                                case "Nombre del dueño":
                                    Console.WriteLine("Nombre del dueño: " + duenio.NombreDuenio + "\n");
                                    break;
                                case "Rut":
                                    Console.WriteLine("Rut: " + duenio.Rut + "\n");
                                    break;
                                case "Correo":
                                    Console.WriteLine("Correo: " + duenio.Correo + "\n");
                                    break;
                                case "Direccion":
                                    Console.WriteLine("Direccion: " + duenio.Direccion + "\n");
                                    break;
                                case "Telefono":
                                    Console.WriteLine("Telefono: " + duenio.Telefono + "\n");
                                    break;
                                case "Nombre de la mascota":
                                    Console.WriteLine("Nombre de la mascota: " + macotas.NombreMascota + "\n");
                                    break;
                                case "Raza":
                                    Console.WriteLine("Raza: " + macotas.Raza + "\n");
                                    break;
                                case "Años":
                                    Console.WriteLine("Años: " + macotas.Anios + "\n");
                                    break;
                                case "Vacunas":
                                    var vacunas = "";
                                    if (macotas.Vacunas == true)
                                    {
                                        vacunas = "Si";
                                    }
                                    else
                                    {
                                        vacunas = "No";
                                    }

                                    Console.WriteLine("Vacunas: " + vacunas + "\n");
                                    break;
                            }
                        }

                        Console.WriteLine("¿Desea eliminarlo? S/N \n");

                        var confirmar = Console.ReadLine();

                        if (confirmar.ToLower() == "s")
                        {
                            ListReservas = ListReservas.Where(x => x.ID != idReserva).ToList();
                            ListDuenios = ListDuenios.Where(x => x.IDReserva != idReserva).ToList();
                            ListMascotas = ListMascotas.Where(x => x.IDReserva != idReserva).ToList();
                        }

                        Console.WriteLine("Precione enter para continuar \n");
                        Console.ReadLine();

                        MenuInicial();
                    }
                    else
                    {
                        Console.WriteLine("No se en encontraron coincidencias \n");
                        Console.WriteLine("Precione enter para continuar \n");
                        Console.ReadLine();

                        MenuInicial();
                    }
                }
                else
                {
                    Console.WriteLine("No se en encontraron coincidencias \n");
                    Console.WriteLine("Precione enter para continuar \n");
                    Console.ReadLine();

                    MenuInicial();
                }
            }
            else
            {
                Console.WriteLine("No ha ingresado ningun rut \n");
                Console.WriteLine("Precione enter para continuar \n");
                Console.ReadLine();

                MenuInicial();
            }
        }

        public void Modificar()
        {
            Console.Clear();

            Console.WriteLine("Menu de modificacion de reserva \n");
            Console.WriteLine("Ingrese el rut del dueño que realizo la reserva \n");
            Console.WriteLine("*Si desea mantener el valor solo precione enter \n");

            var rut = Console.ReadLine();
            var idReserva = 0;

            if (!string.IsNullOrEmpty(rut))
            {
                if (ListDuenios != null)
                {
                    var duenio = ListDuenios.Where(x => x.Rut == rut).ToList()[0];
                    var rev = ListReservas.Where(x => x.ID == duenio.IDReserva).ToList();

                    if (rev.Count() > 0)
                    {
                        var reserva = rev[0];
                        idReserva = reserva.ID;

                        var mascota = ListMascotas.Where(x => x.IDReserva == reserva.ID).ToList()[0];

                        foreach (var campo in Campos)
                        {
                            if (campo != "Rut")
                            {
                                Console.WriteLine("Ingrese " + campo + "\n");

                                var x = Console.ReadLine();

                                switch (campo)
                                {
                                    case "Tipo de reserva":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            reserva.TipoReserva = x;
                                        }
                                        break;
                                    case "Duracion":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            reserva.Duracion = x;
                                        }
                                        break;
                                    case "Costo":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            reserva.Costo = x;
                                        }
                                        break;
                                    case "Nombre del dueño":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            duenio.NombreDuenio = x;
                                        }
                                        break;
                                    case "Correo":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            duenio.Correo = x;
                                        }
                                        break;
                                    case "Direccion":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            duenio.Direccion = x;
                                        }
                                        break;
                                    case "Telefono":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            duenio.Telefono = x;
                                        }
                                        break;
                                    case "Nombre de la mascota":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            mascota.NombreMascota = x;
                                        }
                                        break;
                                    case "Raza":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            mascota.Raza = x;
                                        }
                                        break;
                                    case "Años":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            mascota.Anios = x;
                                        }
                                        break;
                                    case "Vacunas":
                                        if (!string.IsNullOrEmpty(x))
                                        {
                                            if (x.ToLower() == "si")
                                            {
                                                mascota.Vacunas = true;
                                            }
                                            else
                                            {
                                                mascota.Vacunas = false;
                                            }
                                        }
                                        break;
                                }
                            }
                        }

                        Console.WriteLine("¿Esta seguro que desea modificarlo? S/N \n");

                        var confirmar = Console.ReadLine();

                        if (confirmar.ToLower() == "s")
                        {
                            ListReservas = ListReservas.Where(x => x.ID != idReserva).ToList();
                            ListDuenios = ListDuenios.Where(x => x.IDReserva != idReserva).ToList();
                            ListMascotas = ListMascotas.Where(x => x.IDReserva != idReserva).ToList();

                            ListReservas.Add(reserva);
                            ListDuenios.Add(duenio);
                            ListMascotas.Add(mascota);

                            Console.WriteLine("Modificado correctamente \n");
                        }

                        Console.WriteLine("Precione enter para continuar \n");
                        Console.ReadLine();

                        MenuInicial();
                    }
                    else
                    {
                        Console.WriteLine("No se en encontraron coincidencias \n");
                        Console.WriteLine("Precione enter para continuar \n");
                        Console.ReadLine();

                        MenuInicial();
                    }
                }
                else
                {
                    Console.WriteLine("No se en encontraron coincidencias \n");
                    Console.WriteLine("Precione enter para continuar \n");
                    Console.ReadLine();

                    MenuInicial();
                }
            }
            else
            {
                Console.WriteLine("No ha ingresado ningun rut \n");
                Console.WriteLine("Precione enter para continuar \n");
                Console.ReadLine();

                MenuInicial();
            }
        }
    }
}