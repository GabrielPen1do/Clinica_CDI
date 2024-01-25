using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica_CDI
{
    internal class Procedimento
    {
        public string Nome { get; set; }
        public int Duracao { get; set; }
        public string Codigo { get; set; }


        public Procedimento(string nome, int duracao, string codigo)
        {
            Nome = nome;
            Duracao = duracao;
            Codigo = codigo;
        }

      
    }
}
