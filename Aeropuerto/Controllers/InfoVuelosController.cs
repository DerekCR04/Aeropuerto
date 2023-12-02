using Aeropuerto.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Aeropuerto.Controllers
{
    public class InfoVuelosController : Controller
    {

        private readonly IConfiguration Configuration;

        public InfoVuelosController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private List<InfoVueloViewModel> ObtenerInfo()
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string cadenaConexion = Configuration.GetConnectionString("Conexion");
            using (SqlConnection objetoSqlConnection = new SqlConnection(cadenaConexion))
            {
                string sentencia = "SELECT vuelos.vuelo_id, " +
                                   "aviones.modelo AS modelo_avion, " +
                                   "aerolineas.nombre AS nombre_aerolinea, " +
                                   "destinos.pais AS pais_destino, " +
                                   "horarios.hora_salida, " +
                                   "horarios.hora_llegada, " +
                                   "horarios.Fecha " +
                                   "FROM vuelos " +
                                   "JOIN aviones ON vuelos.avion_id = aviones.avion_id " +
                                   "JOIN aerolineas ON vuelos.aerolinea_id = aerolineas.aerolinea_id " +
                                   "JOIN destinos ON vuelos.destino_id = destinos.destino_id " +
                                   "JOIN horarios ON vuelos.vuelo_id = horarios.vuelo_id;";

                using (SqlDataAdapter objetoSqlDataAdapter = new SqlDataAdapter(sentencia, objetoSqlConnection))
                {
                    DataTable objetoDataTable = new DataTable();
                    objetoSqlDataAdapter.Fill(objetoDataTable);

                    List<InfoVueloViewModel> listaInfo = new List<InfoVueloViewModel>();
                    foreach (DataRow r in objetoDataTable.Rows)
                    {
                        InfoVueloViewModel objetoInfo = new InfoVueloViewModel();
                        objetoInfo.VueloId = Convert.ToInt32(r["vuelo_id"]);
                        objetoInfo.NombreAerolinea = Convert.ToString(r["nombre_aerolinea"]);
                        objetoInfo.Pais = Convert.ToString(r["Pais_destino"]);
                        objetoInfo.ModeloAvion = Convert.ToString(r["modelo_avion"]);
                        objetoInfo.HoraSalida = TimeSpan.Parse(r["hora_salida"].ToString());
                        objetoInfo.HoraLlegada = TimeSpan.Parse(r["hora_llegada"].ToString());
                        objetoInfo.Fecha = Convert.ToDateTime(r["Fecha"]);
                        listaInfo.Add(objetoInfo);
                    }

                    return listaInfo;
                }
            }
        }



        // GET: InfoVuelosController
        public ActionResult Index()
        {
            List<InfoVueloViewModel> objetoListInfo = ObtenerInfo();
            return View(objetoListInfo);

            //return View();
        }

        // GET: InfoVuelosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InfoVuelosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InfoVuelosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InfoVuelosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InfoVuelosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InfoVuelosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InfoVuelosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
