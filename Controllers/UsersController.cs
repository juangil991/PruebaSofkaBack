using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetoAPI.Data;
using RetoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//API para gestionar la conexion con la tabla de ususarios
namespace RetoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static string UsuarioActual { get; set; } 
        public int UsuarioActualId { get; set; }
        [HttpGet]
        public IActionResult GetUser()
        {
            UsersRequest<List<User>> UsersResponse = new UsersRequest<List<User>>();
            try
            {
                using (PruebaDBContext Db = new PruebaDBContext())
                {
                    var lst = Db.Users.ToList();
                    UsersResponse.Data = lst;
                }
            }
            catch (Exception ex)
            {
                UsersResponse.Message = ex.Message;
            }
            return Ok(UsersResponse);
        }

        [HttpPost]
        public IActionResult AddUser(UsersResponse Datos)
        {
            UsersRequest<object> UserResponse = new UsersRequest<object>();
            UsuarioActual = Datos.Usuario; 
            try
            {
                using (PruebaDBContext Db = new PruebaDBContext())
                {
                    User NewUser = new User();                
                    NewUser.Usuario = Datos.Usuario;
                    NewUser.Puntaje = 0;
                    Db.Users.Add(NewUser);
                    Db.SaveChanges();                              
                    
                }
                

            }
            catch (Exception ex)
            {
                UserResponse.Message = ex.Message;
            }
            return Ok(UserResponse);
        }

        [HttpPut]
        public IActionResult UserPuntaje(UsersResponse Datos)
        {
            UsersRequest<object> MUsers = new UsersRequest<object>();
            try
            {
                using (PruebaDBContext Db = new PruebaDBContext())
                {
                    var lst = Db.Users.ToList();
                    User UserAct = new();
                    IEnumerable<User> AllUser = from usr in lst
                                               where usr.Usuario == UsuarioActual
                                               select usr;
                    var lista = AllUser.ToList();
                    UserAct = lista.First();
                    User State = Db.Users.Find(UserAct.Id);
                    State.Puntaje = Datos.Puntaje;

                    Db.Entry(State).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    Db.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                MUsers.Message = ex.Message;
            }
            return Ok(MUsers);
        }

    }
}
