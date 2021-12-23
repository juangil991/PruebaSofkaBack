using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetoAPI.Data
{
    interface IRequest<T> //interfaz para gestionar el envio y recepcion de datos del front
    {
        public string Message { get; set; } // Mensaje de error 
        public T Data { get; set; } // respuesta a solicitudes del el front
    }
}
