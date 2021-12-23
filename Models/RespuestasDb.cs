using System;
using System.Collections.Generic;

#nullable disable

namespace RetoAPI.Models
{
    public partial class RespuestasDb
    {
        public int Id { get; set; }
        public string Respuestas { get; set; }
        public int? Correcta { get; set; }
        public int? IdPregunta { get; set; }
    }
}
