using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica_CDI
{
    internal class Paciente
    {
        public string NomeCompleto { get; set; }
        public string NomeMae { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public string Cpf { get; set; }
        public Paciente[] agenda { get; set; }
        public Procedimento UltimoProcedimento { get; set; }
        public DateTime DataUltimoProcedimento { get; set; }
        public int quant;

        public Paciente(int tam)
        {
            agenda = new Paciente[tam];
            quant = 0;
        }

        public Paciente(string nome, string nomeMae, DateTime nascimento, char sexo, string cpf)
        {
            this.NomeCompleto = nome;
            this.NomeMae = nomeMae;
            this.DataNascimento = nascimento;
            this.Sexo = sexo;
            this.Cpf = cpf;
        }


        public bool InserirPaciente(Paciente dados)
        {
            if (quant < agenda.Length)
            {
                agenda[quant] = dados;
                quant++;
                return true;
            }

            else
            {
                return false;
            }
        }

        public Paciente ProcurarPaciente(string nome, string cpf)
        {

            for (int i = 0; i < quant; i++)
            {
                if (agenda[i].NomeCompleto.Equals(nome) || agenda[i].Cpf.Equals(cpf))
                {
                    return agenda[i];
                }
            }

            return null;
        }



    }
}
