using SistemaChamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SistemaChamados.ViewModels
{
    public class MostrarDetalhesPopupVM : ViewModelBase
    {
        public Chamado ChamadoSelecionado { get; set; }

        public MostrarDetalhesPopupVM(Chamado chamado)
        {
            ChamadoSelecionado = chamado;
        }
    }
}