using System;
using System.Collections.Generic;

#nullable disable

namespace RetoAPI.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public int? Puntaje { get; set; }
    }
}
