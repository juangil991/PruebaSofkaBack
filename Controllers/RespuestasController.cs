using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetoAPI.Data;
using RetoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//API para gestionar la conexion con la tabla de respuestas
namespace RetoAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestasController : ControllerBase
    {
        private static Random rnd = new Random();

        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<List<RespuestasDb>>>> GetPreguntas(int id)
        {
            RespuestasRequest<List<RespuestasDb>> Envio = new RespuestasRequest<List<RespuestasDb>>();
            
            List<RespuestasDb> lista = new List<RespuestasDb>();

            try
            {
                 using (PruebaDBContext Db = new PruebaDBContext()) //abre conexion con la base de datos
                {
                    var lst = await Db.RespuestasDbs.ToListAsync(); //optiene todos los elementos de la tabla respuesas
                    IEnumerable<RespuestasDb> respuestas = from Res in lst          //busca las  respuestas asociadas a la pregunta por el  parametro ID
                                                      where Res.IdPregunta == id
                                                      orderby Res.Id
                                                      select Res;
                    lista = respuestas.ToList();
                    var listarandom = lista.OrderBy(x => rnd.Next()); // ordena de manera aleatoria los elementos de la tabla respuestas asociados al id de la pregunta
                    Envio.Data = listarandom.ToList();
                }
            }
            catch (Exception ex)
            {
                Envio.Message = ex.Message;
            }
            
            return Ok(Envio);
        
    }
  

    }
}
