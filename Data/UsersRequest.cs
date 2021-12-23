using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetoAPI.Data
{
    public class UsersRequest<T> : IRequest<T>
    {
        public string Message { get ; set; }
        public T Data { get ; set; }
    }
}
