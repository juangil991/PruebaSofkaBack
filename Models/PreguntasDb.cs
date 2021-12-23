using System;
using System.Collections.Generic;

#nullable disable

namespace RetoAPI.Models
{
    public partial class PreguntasDb
    {
        public int Id { get; set; }
        public string Preguntas { get; set; }
        public int? Dificultad { get; set; }
        public int? Estado { get; set; }
    }
}
