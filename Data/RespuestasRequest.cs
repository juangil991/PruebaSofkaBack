using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetoAPI.Data
{
    public class RespuestasRequest<T>: IRequest<T>
    {
        public string Message { get; set; } // Mensaje de error 
        public T Data { get; set; } // respuesta a solicitudes del el front

    }
}
