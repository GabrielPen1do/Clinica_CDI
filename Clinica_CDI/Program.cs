using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Clinica_CDI
{
    internal class Program
    {

        public static void ListarPacientesCadastrados(Paciente agenda)
        {
            Console.WriteLine("Pacientes Cadastrados: ");
            for (int i = 0; i < agenda.quant; i++)
            {
                Console.WriteLine($"Nome: {agenda.agenda[i].NomeCompleto}\nData de nascimento: {agenda.agenda[i].DataNascimento:dd/MM/yyyy}");
            }
        }

        public static void NumeroProcedimentoPeriodo(Paciente agenda)
        {
            Console.WriteLine($"Digite um período determinado para contar os procedimentos (MM/AAAA a MM/AAAA)");
            Console.WriteLine($"Início do período (MM/AAAA a MM/AAAA)");
            DateTime dataInicio = DateTime.ParseExact(Console.ReadLine(), "MM/yyyy", null);
            Console.WriteLine($"Fim do período (MM/AAAA a MM/AAAA)");
            DateTime dataFim = DateTime.ParseExact(Console.ReadLine(), "MM/yyyy", null);

            Console.WriteLine($"Intervalo entre {dataInicio:MM/yyyy} a {dataFim:MM/yyyy}:");

            int totalProcedimentos = 0;

            for (int i = 0; i < agenda.quant; i++)
            {
                if (agenda.agenda[i].DataUltimoProcedimento >= dataInicio && agenda.agenda[i].DataUltimoProcedimento <= dataFim)
                {
                    totalProcedimentos++;
                }
            }

            Console.WriteLine($"Número de procedimentos {totalProcedimentos}");

        }

        public static void TotalProcedimento(Paciente agenda)
        {
            Console.WriteLine("Digite o procedimento para calcular o tempo total de duração:\nRaios – X de Tórax em PA\nUltrassonografia Obstétrica\nUltrassonografia de Próstata\nTomografia");
            string procedimentoBusca = Console.ReadLine();

            Console.WriteLine("Digite um período determinado para contar os procedimentos (MM/AAAA a MM/AAAA)");
            DateTime inicioPeriodo = DateTime.ParseExact(Console.ReadLine(), "MM/yyyy", null);
            DateTime fimPeriodo = DateTime.ParseExact(Console.ReadLine(), "MM/yyyy", null);

            int tempoTotal = 0;

            for (int i = 0; i < agenda.quant; i++)
            {
                if (agenda.agenda[i].DataUltimoProcedimento >= inicioPeriodo && agenda.agenda[i].DataUltimoProcedimento <= fimPeriodo)
                {
                    if (agenda.agenda[i].UltimoProcedimento == null)
                    {
                        continue;
                    }

                    if (agenda.agenda[i].UltimoProcedimento.Nome == procedimentoBusca)
                    {
                        tempoTotal += DuracaoProcedimento(agenda.agenda[i].UltimoProcedimento.Nome);
                    }
                }
            }

            Console.WriteLine($"Duração total do procedimento: {procedimentoBusca}\nMinutos: {tempoTotal}");
        }

        public static int DuracaoProcedimento(string nomeProcedimento)
        {
            switch (nomeProcedimento)
            {
                case "Raios – X de Tórax em PA":
                    return 30;
                case "Ultrassonografia Obstétrica":
                    return 40;
                case "Ultrassonografia de Próstata":
                    return 20;
                case "Tomografia":
                    return 30;
                default:
                    return 0; 
            }
        }

        public static void ListarAtendimentosData(Paciente agenda)
        {
            Console.WriteLine("Digite a data desejada para listar os atendimentos (DD/MM/AAAA)");
            DateTime dataPesquisa = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.WriteLine($"Lista dos atendimentos no dia {dataPesquisa:dd/MM/yyyy}:");
            for (int i = 0; i < agenda.quant; i++)
            {
                if (agenda.agenda[i].DataUltimoProcedimento.Date == dataPesquisa.Date)
                {
                    Console.WriteLine($"Paciente: {agenda.agenda[i].NomeCompleto}\nProcedimento: {agenda.agenda[i].UltimoProcedimento.Nome}");
                }
            }
        }

        public static void ExibirMenu()
        {
            Console.WriteLine("Escolhe uma opção");
            Console.WriteLine("1: Cadastrar paciente");
            Console.WriteLine("2: Realizar atendimento");
            Console.WriteLine("3: Listar o nome e data de nascimento de todos os pacientes cadastrados");
            Console.WriteLine("4: Exibir a lista de atendimentos realizados em uma determinada data");
            Console.WriteLine("5: Exibir o número de cada procedimento realizado em um período informado");
            Console.WriteLine("6: Exibir o tempo total de duração de um procedimento ou de todos os procedimentos em um período");
        }

        public static void ProcurarPacienteCadastrado(Paciente paciente)
        {
            Console.WriteLine("Digite o nome do paciente que você deseja pesquisar");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o CPF do paciente que você deseja pesquisar");
            string cpf = Console.ReadLine();

            Paciente pacienteEncontrado = paciente.ProcurarPaciente(nome, cpf);

            if (pacienteEncontrado != null)
            {
                Console.WriteLine("Paciente encontrado:");
                ImprimirInformacoesPaciente(pacienteEncontrado);
                RealizarNovoAtendimento(pacienteEncontrado);
            }

            else
            {
                Console.WriteLine("Paciente não encontrado, insira as informações do paciente.");
                CadastrarPaciente(paciente);


            }
        }

        public static void RealizarAtendimento(Paciente paciente, Procedimento procedimento, DateTime dataAtendimento)
        {
            Console.WriteLine($"\nAtendimento realizado para o paciente {paciente.NomeCompleto} na data {dataAtendimento} - Procedimento: {procedimento.Nome}");
            paciente.UltimoProcedimento = procedimento;
            paciente.DataUltimoProcedimento = dataAtendimento;
        }

        public static bool VerificarTempoProcedimento(Paciente paciente, string procedimento, int meses)
        {
            if (paciente.DataUltimoProcedimento == DateTime.MinValue)
            {
                return true;
            }

            int diferencaMeses = (DateTime.Now.Year - paciente.DataUltimoProcedimento.Year) * 12 +
                DateTime.Now.Month - paciente.DataUltimoProcedimento.Month;

            return diferencaMeses >= meses;
        }

        public static void RealizarNovoAtendimento(Paciente paciente)
        {
            Console.WriteLine("\nNovo Atendimento: ");
            Console.WriteLine("Data do atendimento (DD/MM/AAAA)");
            DateTime dataAtendimento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy",null);

            Console.WriteLine("Escolha qual procedimento deseja realizar: ");
            Console.WriteLine("1: Raios – X de Tórax em PA");
            Console.WriteLine("2: Ultrassonografia Obstétrica");
            Console.WriteLine("3: Ultrassonografia de Próstata");
            Console.WriteLine("4: Tomografia");

            int opcaoAtendimento = int.Parse(Console.ReadLine());

            switch (opcaoAtendimento)
            {
                case 1:
                    RealizarAtendimento(paciente, new Procedimento("Raios – X de Tórax em PA", 30, "NIKFOXMOHZ"), dataAtendimento);
                    break;
                case 2:
                    if (paciente.Sexo == 'F' && (DateTime.Now.Year - paciente.DataNascimento.Year < 60))
                    {
                        RealizarAtendimento(paciente, new Procedimento("Ultrassonografia Obstétrica", 40, "HOMNFQIRSY"), dataAtendimento);
                    }

                    else
                    {
                        Console.WriteLine("Este procedimento pode ser realizado só por Pacientes do sexo Feminino com idade menor que 60 anos");
                    }
                    break;
                case 3:
                    if (paciente.Sexo == 'M')
                    {
                        RealizarAtendimento(paciente, new Procedimento("Ultrassonografia de Próstata", 20, "ODLTTTUIZM"), dataAtendimento);
                    }
                    else
                    {
                        Console.WriteLine("Este procedimento pode ser realizado somente por Pacientes do sexo Masculino.");
                    }
                    break;
                case 4:
                    if (VerificarTempoProcedimento(paciente, "Ultrassonografia Obstétrica", 3) &&
                       VerificarTempoProcedimento(paciente, "Ultrassonografia de Próstata", 3))
                    {
                        RealizarAtendimento(paciente, new Procedimento("Tomografia", 30, "MOVKLGXRDN"), dataAtendimento);
                    }
                    else
                    {
                        Console.WriteLine("Este procedimento só pode ser realizado em Pacientes que não realizaram Ultrassonografia Obstétrica ou Ultrassonografia de Próstata nos últimos três meses.");
                    }
                    break;
                   
                

            }
        }

        static void CadastrarPaciente(Paciente paciente)
        {
            Console.WriteLine("Insira as informações do paciente:");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Nome da Mãe: ");
            string nomeMae = Console.ReadLine();

            Console.Write("Data de Nascimento (DD/MM/AAAA): ");
            DateTime dataNascimento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.Write("Sexo (M/F): ");
            char sexo = char.Parse(Console.ReadLine().ToUpper());

            Console.Write("CPF: ");
            string cpf = Console.ReadLine();

            bool pacienteJaCadastrado = false;

            for (int i = 0; i < paciente.quant; i++)
            {
                if (paciente.agenda[i].Cpf == cpf)
                {
                    pacienteJaCadastrado = true;
                    break;
                }
            }

            if (!pacienteJaCadastrado)
            {
                if ((DateTime.Now - dataNascimento).TotalDays > 12 * 365)
                {
                    Paciente novoPaciente = new Paciente(nome, nomeMae, dataNascimento, sexo, cpf);
                    paciente.InserirPaciente(novoPaciente);

                    Console.WriteLine("Informações do paciente cadastrado:");
                    ImprimirInformacoesPaciente(novoPaciente);
                }
                else
                {
                    Console.WriteLine("A clínica só atende pacientes adultos (acima de 12 anos).");
                }
            }
            else
            {
                Console.WriteLine("Já existe um paciente cadastrado com esse CPF.");
            }

        }

        static void ImprimirInformacoesPaciente(Paciente paciente)
        {
            Console.WriteLine($"Nome: {paciente.NomeCompleto} ");
            Console.WriteLine($"Nome da Mãe: {paciente.NomeMae} ");
            Console.WriteLine($"Data de Nascimento: {paciente.DataNascimento} ");
            Console.WriteLine($"Sexo: {paciente.Sexo} ");
            Console.WriteLine($"CPF: {paciente.Cpf} ");

        }
        static void Main(string[] args)
        {
          
            bool continuarOperacao = true;
            Paciente agenda = new Paciente(100);
           

            while (continuarOperacao)
            {
                ExibirMenu();
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        CadastrarPaciente(agenda);
                        break;
                    case 2:
                        ProcurarPacienteCadastrado(agenda);
                        break;
                    case 3:
                        ListarPacientesCadastrados(agenda);
                        break;
                    case 4:
                        ListarAtendimentosData(agenda);
                        break;  
                    case 5:
                        NumeroProcedimentoPeriodo(agenda);
                        break;
                    case 6:
                        TotalProcedimento(agenda);
                        break;
                }

                Console.Write("\nDeseja realizar outra operação? (S/N): ");
                continuarOperacao = Console.ReadLine().ToUpper() == "S";

            }
            Console.ReadKey();

        }
    }
}
