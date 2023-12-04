using System;
using ProjetoEstacionamento.Services;

namespace ProjetoEstacionamento
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EstacionamentoServices estacionamentoServices = new EstacionamentoServices();
            estacionamentoServices.IniciarMenuLoop();
        }
    }
}
