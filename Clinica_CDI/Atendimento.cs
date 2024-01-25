using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica_CDI
{
    internal class Atendimento
    {
        public Paciente PacienteAtendido { get; set; }
        public DateTime DataAtendimento { get; set; }
        public Procedimento ProcedimentoRealizado { get; set; }

        public Atendimento(Paciente paciente, DateTime dataAtendimento, Procedimento procedimentoRealizado)
        {
            this.PacienteAtendido = paciente;
            this.DataAtendimento = dataAtendimento;
            this.ProcedimentoRealizado = procedimentoRealizado;
        }

    }
}
