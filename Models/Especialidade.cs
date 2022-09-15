using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace API_Consultas_Agendadas.Models
{
    public partial class Especialidade
    {
        public Especialidade()
        {
            Medicos = new HashSet<Medico>();
        }

        
        public int Id { get; set; }
        public string Categoria { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
