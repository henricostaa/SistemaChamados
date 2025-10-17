using SistemaChamados.Models;
using SistemaChamados.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using SistemaChamados.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Data;

namespace SistemaChamados.ViewModels
{
    public class HistoricoVM : INotifyPropertyChanged
    {
        private ObservableCollection<Chamado> _todosChamados = new();
        public ObservableCollection<Chamado> ChamadosFiltrados { get; set; } = new();


        private string _filtroStatus = "Todos";
        public string FiltroStatus
        {
            get => _filtroStatus;
            set
            {
                _filtroStatus = value;
                OnPropertyChanged(nameof(FiltroStatus));
            }
        }

        private string _filtroPrioridade = "Todas";
        public string FiltroPrioridade
        {
            get => _filtroPrioridade;
            set
            {
                _filtroPrioridade = value;
                OnPropertyChanged(nameof(FiltroPrioridade));
            }
        }

        private string _filtroPeriodo = "Todo Período";
        public string FiltroPeriodo
        {
            get => _filtroPeriodo;
            set
            {
                _filtroPeriodo = value;
                OnPropertyChanged(nameof(FiltroPeriodo));
            }
        }

        public ICommand AplicarFiltrosCommand { get; }
        public ICommand AtenderChamadoCommand { get; }
        public ICommand ConcluirChamadoCommand { get; }
        public ICommand MostrarDetalhesCommand { get; }

        public HistoricoVM()
        {
            MostrarDetalhesCommand = new RelayCommand(ExecutarMostrarDetalhes);
            AtenderChamadoCommand = new RelayCommand(ExecutarAtenderChamado);
            ConcluirChamadoCommand = new RelayCommand(ExecutarConcluirChamado);
            AplicarFiltrosCommand = new RelayCommand(_ => AplicarFiltros());
            CarregarChamados();
        }

        private void ExecutarMostrarDetalhes(object chamadoObj)
        {
            if (chamadoObj is Chamado chamado)
            {
                MessageBox.Show($"Chamado #{chamado.ChamadoId}\n\nTítulo: {chamado.Titulo}\nDescrição: {chamado.Descricao}\nStatus: {chamado.Status}\nPrioridade: {chamado.Prioridade}\nSolicitante: {chamado.Solicitante?.Username}", "Detalhes do Chamado");
            }
        }

        private void ExecutarAtenderChamado(object chamadoObj)
        {
            if (chamadoObj is Chamado chamado)
            {
                using var db = new AppDbContext();
                var chamadoDb = db.Chamados.Find(chamado.ChamadoId);
                if (chamadoDb != null)
                {
                    chamadoDb.Status = "Em Andamento";
                    chamadoDb.DataAtribuicao = DateTime.Now;
                    db.SaveChanges();
                }

                chamado.Status = "Em Andamento"; // Atualiza também na UI
                CollectionViewSource.GetDefaultView(ChamadosFiltrados).Refresh();
            }

        }
        

        private void ExecutarConcluirChamado(object chamadoObj)
        {
            if (chamadoObj is Chamado chamado)
            {
                using var db = new AppDbContext();
                var chamadoDb = db.Chamados.Find(chamado.ChamadoId);
                if (chamadoDb != null)
                {
                    chamadoDb.Status = "Fechado";
                    chamadoDb.DataFechamento = DateTime.Now;
                    db.SaveChanges();
                }

                chamado.Status = "Fechado"; // Atualiza também na UI
                CollectionViewSource.GetDefaultView(ChamadosFiltrados).Refresh();

            }
        }

       

        private void CarregarChamados()
        {
            using var db = new AppDbContext();
            var lista = db.Chamados
                .Include(c => c.Solicitante)
                .Include(c => c.AtribuidoA)
                .OrderByDescending(c => c.DataAbertura)
                .ToList();

            _todosChamados = new ObservableCollection<Chamado>(lista);
            ChamadosFiltrados = new ObservableCollection<Chamado>(_todosChamados);
            CollectionViewSource.GetDefaultView(ChamadosFiltrados).Refresh();
        }

        private void AplicarFiltros()
        {
            var filtrados = _todosChamados.Where(c =>
            {
                bool statusOk = FiltroStatus == "Todos" || string.Equals(c.Status?.Trim(), FiltroStatus.Trim(), StringComparison.OrdinalIgnoreCase);
                bool prioridadeOk = FiltroPrioridade == "Todas" || string.Equals(c.Prioridade?.Trim(), FiltroPrioridade.Trim(), StringComparison.OrdinalIgnoreCase);

                bool periodoOk = true;
                if (FiltroPeriodo != "Todo Período")
                {
                    DateTime limite = FiltroPeriodo switch
                    {
                        "Últimos 7 Dias" => DateTime.Now.AddDays(-7),
                        "Últimos 30 Dias" => DateTime.Now.AddDays(-30),
                        _ => DateTime.MinValue
                    };

                    periodoOk = c.DataAbertura >= limite;
                }

                return statusOk && prioridadeOk && periodoOk;
            }).ToList();

            ChamadosFiltrados = new ObservableCollection<Chamado>(filtrados);
            OnPropertyChanged(nameof(ChamadosFiltrados));
        }

       

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
