using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetoAPI.Data;
using RetoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//API para gestionar la conexion con la tabla de preguntas
namespace RetoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PregutasController : ControllerBase
    {
        private static Random rnd = new Random();

         [HttpGet("{Id}")]
         public ActionResult GetPreguntas(int id)
         {
            // PreguntasRequest<List<PreguntasDb>> Envio = new PreguntasRequest<List<PreguntasDb>>();
            PreguntasRequest<PreguntasDb> Envio = new PreguntasRequest<PreguntasDb>();
            List<PreguntasDb> lista = new List<PreguntasDb>();
             try
             {
                  using (PruebaDBContext Db = new PruebaDBContext()) // abre conexion con base de datos
                 {
                    var lst = Db.PreguntasDbs.ToList(); // obtiene todos los datos de la tabla respuestas
                     IEnumerable<PreguntasDb> preguntas = from Pre in lst
                                                       where Pre.Dificultad == id && Pre.Estado==0   //busca las preguntas relacionadas a la dificultad y que no hayan sido utilizadas
                                                       orderby Pre.Id
                                                       select Pre;
                     lista = preguntas.ToList();
                    var listaRandom = lista.OrderBy(x => rnd.Next()); // envia solo un dato de manera aleactoria
                    Envio.Data = listaRandom.First();
                 }
             }
             catch (Exception ex)
             {
                 Envio.Message = ex.Message;
             }

             return Ok(Envio);
            
          }
        [HttpPut]
        public IActionResult Edit(PreguntasResponse Datos)
        {
            PreguntasRequest<object> Mpregunta = new PreguntasRequest<object>();
            try
            {
                //actualiza el estado de la pregunta si ya ha sido utilizada si el estado es 99 ejecuta el procedimiento almacenado que reinicializa el estado de todas las preguntas
                using (PruebaDBContext Db = new PruebaDBContext())
                {
                    if (Datos.Estado != 99)
                    {
                        PreguntasDb State = Db.PreguntasDbs.Find(Datos.Id);
                        State.Estado = Datos.Estado;

                        Db.Entry(State).State = Microsoft.EntityFrameworkCore.EntityState.Modified;   
                        Db.SaveChanges();
                    }
                    else
                    {
                        var procedure = Db.PreguntasDbs.FromSqlRaw("execute CondInicial").ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Mpregunta.Message= ex.Message;
            }
            return Ok(Mpregunta);
        }



    }
}
