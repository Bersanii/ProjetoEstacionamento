using ProjetoEstacionamento.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEstacionamento.Models
{
    public class Veiculo
    {
        public TipoVeiculo Tipo { get; set; }
        public string Placa { get; set; }
        public TipoVaga? VagaEstacionado { get; set; }

        public Veiculo(TipoVeiculo tipo)
        {
            Tipo = tipo;
            Placa = GerarPlaca();
        }

        static string GerarPlaca()
        {
            Random random = new Random();

            // Letras aleatórias (A-Z)
            char[] letras = new char[3];
            for (int i = 0; i < 3; i++)
            {
                letras[i] = (char)random.Next('A', 'Z' + 1);
            }

            // Números aleatórios (0-9)
            int[] numeros = new int[4];
            for (int i = 0; i < 4; i++)
            {
                numeros[i] = random.Next(0, 10);
            }

            string placa = $"{letras[0]}{letras[1]}{letras[2]}{numeros[0]}{numeros[1]}{numeros[2]}{numeros[3]}";

            return placa;
        }
    }
}
