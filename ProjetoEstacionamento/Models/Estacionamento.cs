using ProjetoEstacionamento.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEstacionamento.Models
{
    public class Estacionamento
    {
        private int TotalVagasMotos;
        private int VagasMotos;
        private int TotalVagasCarros;
        private int VagasCarros;
        private int TotalVagasGrandes;
        private int VagasGrandes;
        private List<Veiculo> Veiculos;

        public Estacionamento(int vagasMotos, int vagasCarros, int vagasGrandes)
        {
            TotalVagasMotos = vagasMotos;
            VagasMotos = vagasMotos;
            TotalVagasCarros = vagasCarros;
            VagasCarros = vagasCarros;
            TotalVagasGrandes = vagasGrandes;
            VagasGrandes = vagasGrandes;
            Veiculos = new List<Veiculo>();
        }

        public bool ExisteLugarParaMoto()
        {
            // Motos podem parar em qualquer vaga
            return (VagasMotos + VagasCarros + VagasGrandes) > 0;
        }

        public bool ExisteLugarParaCarro()
        {
            // Carros podem parar em vagas de carros e grandes
            return (VagasCarros + VagasGrandes) > 0;
        }

        public bool ExisteLugarParaVan()
        {
            // Vans podem parar em vagas grandes ou ocupar 3 vagas de carros
            return (VagasGrandes + (int)Math.Floor((double)VagasCarros / 3)) > 0;
        }


        public void Estacionar(Veiculo veiculo)
        {

            switch (veiculo.Tipo)
            {
                case TipoVeiculo.Moto:

                    if (!ExisteLugarParaMoto())
                    {
                        Console.WriteLine("> Não há vagas disponíveis para motos");
                        return;
                    }

                    if (VagasMotos > 0) // Tenta primeiro uma vaga para moto
                    {
                        InserirNaVaga(veiculo, TipoVaga.Moto);
                    }
                    else if (VagasCarros > 0) // Se não houver vagas para motos, tenta uma vaga para carro
                    {
                        InserirNaVaga(veiculo, TipoVaga.Carro);
                    }
                    else if (VagasGrandes > 0) // Se não houver vagas para carros, tenta uma vaga grande
                    {
                        InserirNaVaga(veiculo, TipoVaga.Grande);
                    }
                    break;
                case TipoVeiculo.Carro:

                    if (!ExisteLugarParaCarro())
                    {
                        Console.WriteLine("> Não há vagas disponíveis para carros");
                        return;
                    }

                    if (VagasCarros > 0) // Tenta primero uma vaga para carro
                    {
                        InserirNaVaga(veiculo, TipoVaga.Carro);
                    }
                    else if (VagasGrandes > 0) // Se não houver vagas para carros, tenta uma vaga grande
                    {
                        InserirNaVaga(veiculo, TipoVaga.Grande);
                    }
                    break;
                case TipoVeiculo.Van:

                    if (!ExisteLugarParaVan())
                    {
                        Console.WriteLine("> Não há vagas disponíveis para vans");
                        return;
                    }

                    if (VagasGrandes > 0) // Tenta primeiro uma vaga grande
                    {
                        InserirNaVaga(veiculo, TipoVaga.Grande);
                    }
                    else if (VagasCarros >= 3) // Se não houver vagas grandes, tenta 3 vagas de carros
                    {
                        InserirNaVaga(veiculo, TipoVaga.Carro);
                    }
                    break;
                default:
                    return;
            }

            Console.WriteLine($"> Veículo '{veiculo.Tipo}' estacionado em uma vaga do tipo '{veiculo.VagaEstacionado}'");
        }

        private void InserirNaVaga(Veiculo veiculo, TipoVaga tipoVaga)
        {
            veiculo.VagaEstacionado = tipoVaga;
            switch (tipoVaga)
            {
                case TipoVaga.Moto:
                    VagasMotos--;
                    break;
                case TipoVaga.Carro:
                    if (veiculo.Tipo == TipoVeiculo.Van)
                    {
                        VagasCarros -= 3;
                    }
                    else
                    {
                        VagasCarros--;
                    }
                    break;
                case TipoVaga.Grande:
                    VagasGrandes--;
                    break;
            }
            Veiculos.Add(veiculo);
        }

        public void Sair(string? placa)
        {
            if (Veiculos.Count == 0)
            {
                Console.WriteLine("> Não há veículos estacionados");
                return;
            }

            Veiculo? veiculo;
            if (string.IsNullOrEmpty(placa)) // Se não for informada a placa, sai o veiculo mais antigo
            {
                veiculo = Veiculos[0];
            }
            else // Se for informada a placa, sai o veiculo com a placa informada
            {
                placa = placa.ToUpper(); // Converte a placa para maiúsculo
                veiculo = Veiculos.Find(v => v.Placa == placa);
            }

            if (veiculo != null)
            {
                switch (veiculo.VagaEstacionado)
                {
                    case TipoVaga.Moto:
                        VagasMotos++;
                        break;
                    case TipoVaga.Carro:
                        if (veiculo.Tipo == TipoVeiculo.Van)
                        {
                            VagasCarros += 3;
                        }
                        else
                        {
                            VagasCarros++;
                        }
                        break;
                    case TipoVaga.Grande:
                        VagasGrandes++;
                        break;
                }

                Veiculos.Remove(veiculo);
            }
            else
            {
                Console.WriteLine("> Veículo não encontrado");
                return;
            }

            Console.WriteLine($"> Veículo '{veiculo.Tipo}' saiu da vaga '{veiculo.VagaEstacionado}'");
        }

        public void PrintarEstacionamento()
        {
            Console.WriteLine(  $"\n--------------------------------------------" +
                                $"\nVeículos estacionados" +
                                $"\n--------------------------------------------");

            if (Veiculos.Count == 0)
            {
                Console.WriteLine("Nenhum veículo estacionado");
                return;
            }

            foreach (var veiculo in Veiculos)
            {
                Console.WriteLine(  $"\nTipo.............. {veiculo.Tipo}" +
                                    $"\nPlaca............. {veiculo.Placa}" +
                                    $"\nTipo de Vaga...... {veiculo.VagaEstacionado}");
            }
        }

        public void PrintarInformacoes()
        {
            Console.WriteLine(  $"\n--------------------------------------------" +
                                $"\nInformações do Estacionamento" +
                                $"\n--------------------------------------------");

            Console.WriteLine("Situação: ");
            if (Veiculos.Count == 0)
            {
                Console.WriteLine("  Vazio");
            }
            else if (!ExisteLugarParaMoto() && !ExisteLugarParaCarro() && !ExisteLugarParaVan())
            {
                Console.WriteLine("  Cheio");
            }
            else
            {
                Console.WriteLine("  Parcialmente cheio");
            }

            Console.WriteLine(  $"\nVagas totais:" +
                                $"\n  Motos............ {TotalVagasMotos}" +
                                $"\n  Carros........... {TotalVagasCarros}" +
                                $"\n  Grandes.......... {TotalVagasGrandes}" +
                                $"\n    Total.......... {TotalVagasMotos + TotalVagasCarros + TotalVagasGrandes}");

            Console.WriteLine(  $"\nVagas disponíveis:" +
                                $"\n  Motos............ {VagasMotos}" + (VagasMotos == 0 ? " (Cheio)" : "") +
                                $"\n  Carros........... {VagasCarros}" + (VagasCarros == 0 ? " (Cheio)" : "") +
                                $"\n  Grandes.......... {VagasGrandes}" + (VagasGrandes == 0 ? " (Cheio)" : "") +
                                $"\n    Total.......... {VagasMotos + VagasCarros + VagasGrandes}");

            Console.WriteLine(  $"\nVeiculos no estacionamento" +
                                $"\n  Motos............ {Veiculos.Count(v => v.Tipo == TipoVeiculo.Moto)}" +
                                $"\n  Carros........... {Veiculos.Count(v => v.Tipo == TipoVeiculo.Carro)}" +
                                $"\n  Vans............. {Veiculos.Count(v => v.Tipo == TipoVeiculo.Van)}" +
                                $"\n    Total.......... {Veiculos.Count}");
        }
    }
}
