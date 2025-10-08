using SistemaChamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SistemaChamados.Helpers;
using System.ComponentModel;
using System.Windows.Input;
using SistemaChamados.Data;
using System.Windows.Data;

namespace SistemaChamados.ViewModels
{
    class HomeVM : ViewModelBase
    {
        private ObservableCollection<Chamado> _todosChamados = new ObservableCollection<Chamado>();
        public ObservableCollection<Chamado> ChamadosFiltrados { get; set; } = new ObservableCollection<Chamado>();

        public int TotalAbertos => _todosChamados.Count(c => c.Status == "Aberto");
        public int TotalFechados => _todosChamados.Count(c => c.Status == "Fechado");
        public int TotalPendentes => _todosChamados.Count(c => c.Status == "Pendente");
        public int TotalChamados => _todosChamados.Count;

        public ObservableCollection<string> UltimosChamados { get; set; } = new ObservableCollection<string>();

        public ICommand CarregarVisaoGeralCommand { get; }
        public ICommand FiltrarAbertosCommand { get; }
        public ICommand FiltrarFechadosCommand { get; }
        public ICommand FiltrarPendentesCommand { get; }
        public ICommand FiltrarTodosCommand { get; }

        public HomeVM()
        {
            CarregarChamadosBanco();

            CarregarVisaoGeralCommand = new RelayCommand(CarregarVisaoGeral);
            FiltrarAbertosCommand = new RelayCommand(_ => FiltrarPorStatus("Aberto"));
            FiltrarFechadosCommand = new RelayCommand(_ => FiltrarPorStatus("Fechado"));
            FiltrarPendentesCommand = new RelayCommand(_ => FiltrarPorStatus("Pendente"));
            FiltrarTodosCommand = new RelayCommand(_ => FiltrarTodos());
        }

        private void CarregarChamadosBanco()
        {
            using (var db = new AppDbContext())
            {
                var lista = db.Chamados.OrderByDescending(c => c.DataAbertura).ToList();
                _todosChamados = new ObservableCollection<Chamado>(lista);
                ChamadosFiltrados = new ObservableCollection<Chamado>(_todosChamados);
            }

            UltimosChamados.Clear();
            foreach (var chamado in _todosChamados.Take(5))
            {
                UltimosChamados.Add($"{chamado.Titulo} – {chamado.Status}");
            }

            OnPropertyChanged(nameof(ChamadosFiltrados));
            OnPropertyChanged(nameof(UltimosChamados));
            OnPropertyChanged(nameof(TotalAbertos));
            OnPropertyChanged(nameof(TotalFechados));
            OnPropertyChanged(nameof(TotalPendentes));
            OnPropertyChanged(nameof(TotalChamados));
        }

        private void FiltrarPorStatus(string status)
        {
            var filtrados = _todosChamados.Where(c => c.Status == status).ToList();
            ChamadosFiltrados.Clear();
            foreach (var chamado in filtrados)
                ChamadosFiltrados.Add(chamado);

            OnPropertyChanged(nameof(ChamadosFiltrados));
        }

        private void FiltrarTodos()
        {
            ChamadosFiltrados.Clear();
            foreach (var chamado in _todosChamados)
                ChamadosFiltrados.Add(chamado);

            OnPropertyChanged(nameof(ChamadosFiltrados));
        }

        private void CarregarVisaoGeral(object obj)
        {
            UltimosChamados.Clear();

            foreach (var chamado in _todosChamados)
            {
                UltimosChamados.Add($"{chamado.Titulo} – {chamado.Status}");
            }

            OnPropertyChanged(nameof(TotalAbertos));
            OnPropertyChanged(nameof(TotalFechados));
            OnPropertyChanged(nameof(TotalPendentes));
            OnPropertyChanged(nameof(TotalChamados));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}