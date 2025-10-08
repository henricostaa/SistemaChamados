using SistemaChamados.Data;
using SistemaChamados.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Windows.Data;

namespace SistemaChamados.ViewModels
{
    public class HistoricoVM : INotifyPropertyChanged
    {
        public ObservableCollection<Chamado> Chamados { get; set; } = new();

        public ICollectionView ChamadosFiltrados { get; set; }

        public string FiltroStatus { get; set; }
        public string FiltroPrioridade { get; set; }

        public HistoricoVM()
        {
            CarregarChamados();
            ChamadosFiltrados = CollectionViewSource.GetDefaultView(Chamados);
            ChamadosFiltrados.Filter = FiltrarChamados;
        }

        private bool FiltrarChamados(object obj)
        {
            if (obj is not Chamado chamado) return false;

            bool statusOk = string.IsNullOrEmpty(FiltroStatus) || chamado.Status == FiltroStatus;
            bool prioridadeOk = string.IsNullOrEmpty(FiltroPrioridade) || chamado.Prioridade == FiltroPrioridade;

            return statusOk && prioridadeOk;
        }

        public void AtualizarFiltro()
        {
            ChamadosFiltrados.Refresh();
        }

        private void CarregarChamados()
        {
            using (var context = new AppDbContext()) // substitua pelo seu DbContext real
            {
                var lista = context.Chamados
                    .Include(c => c.Solicitante)
                    .Include(c => c.AtribuidoA)
                    .ToList();

                Chamados.Clear();
                foreach (var chamado in lista)
                    Chamados.Add(chamado);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}