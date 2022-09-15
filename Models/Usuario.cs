using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace API_Consultas_Agendadas.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Medicos = new HashSet<Medico>();
            Pacientes = new HashSet<Paciente>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? IdTipoUsuario { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<Medico> Medicos { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
