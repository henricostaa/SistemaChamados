using SistemaChamados.Models;
using SistemaChamados.Models;
using System.Collections.Generic;

namespace SistemaChamados.Services
{
    public static class ChamadoRepository
    {
        private static readonly List<Chamado> chamados = new List<Chamado>();

        public static void Adicionar(Chamado chamado)
        {
            chamados.Add(chamado);
        }

        public static List<Chamado> Listar()
        {
            return new List<Chamado>(chamados);
        }
    }
}