using ProjetoEstacionamento.Enums;
using ProjetoEstacionamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEstacionamento.Services
{
    internal class EstacionamentoServices
    {
        private Estacionamento Estacionamento { get; set; }

        public EstacionamentoServices()
        {
            int vagasMotos;
            int vagasCarros;
            int vagasGrandes;

            while (true)
            {
                

                Console.Clear();
                try
                {
                    Console.WriteLine("Configuração do estacionamento");
                    Console.Write("Vagas para motos: ");
                    string? s = Console.ReadLine();
                    _ = string.IsNullOrEmpty(s) ? vagasMotos = 0 : vagasMotos = int.Parse(s);
                    Console.Write("Vagas para carros: ");
                    s = Console.ReadLine();
                    _ = string.IsNullOrEmpty(s) ? vagasCarros = 0 : vagasCarros = int.Parse(s);
                    Console.Write("Vagas para vans: ");
                    s = Console.ReadLine();
                    _ = string.IsNullOrEmpty(s) ? vagasGrandes = 0 : vagasGrandes = int.Parse(s);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Número de vagas inválido");
                    Console.Write("\nPressione qualquer tecla para continuar...");
                    Console.ReadLine();
                    continue;
                }

                if (vagasMotos < 0 || vagasCarros < 0 || vagasGrandes < 0)
                {
                    Console.WriteLine("Número de vagas inválido");
                    Console.Write("\nPressione qualquer tecla para continuar...");
                    Console.ReadLine();
                    continue;
                }

                break;
            }

            Estacionamento = new Estacionamento(vagasMotos, vagasCarros, vagasGrandes);
        }

        public void IniciarMenuLoop() 
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Entrada");
                Console.WriteLine("2 - Saida");
                Console.WriteLine("3 - Veiculos no Estacionamento");
                Console.WriteLine("4 - Informações");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");

                int opcao;
                try
                {
                    string? s = Console.ReadLine();
                    _ = string.IsNullOrEmpty(s) ? throw new Exception() : opcao = int.Parse(s);
                }
                catch (Exception e)
                {
                    Console.WriteLine("> Opção inválida");
                    Console.Write("\nPressione qualquer tecla para continuar...");
                    Console.ReadLine();
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        Console.Write("Tipo de veículo (");
                        TipoVeiculo[] tipos = (TipoVeiculo[])Enum.GetValues(typeof(TipoVeiculo));
                        Console.Write(string.Join(", ", tipos));
                        Console.Write("): ");
                        try
                        {
                            string? s = Console.ReadLine();
                            if (string.IsNullOrEmpty(s) || !Enum.IsDefined(typeof(TipoVeiculo), s))
                            {
                                throw new Exception();
                            };

                            TipoVeiculo tipoVeiculo = (TipoVeiculo)Enum.Parse(typeof(TipoVeiculo), s);

                            Veiculo veiculo = new Veiculo(tipoVeiculo);

                            Estacionamento.Estacionar(veiculo);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("> Tipo de veículo inválido");
                        }
                        break;
                    case 2:
                        Console.Write("Placa (Vazio para remover veiculo mais antigo): ");
                        string? placa = Console.ReadLine();

                        Estacionamento.Sair(placa);
                        break;
                    case 3:
                        Estacionamento.PrintarEstacionamento();
                        break;
                    case 4:
                        Estacionamento.PrintarInformacoes();
                        break;
                    case 0:
                        return;

                    default:
                        Console.WriteLine("> Opção inválida");
                        break;
                }
                Console.Write("\nPressione qualquer tecla para continuar...");
                Console.ReadLine();
            }
        }
    }
}
