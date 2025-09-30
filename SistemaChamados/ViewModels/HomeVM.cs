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

namespace SistemaChamados.ViewModels
{
    class HomeVM : ViewModelBase
    {
        public ObservableCollection<Chamado> Chamados { get; set; } = new ObservableCollection<Chamado>();
        public int TotalAbertos => Chamados.Count(c => c.Status == "Aberto");
        public int TotalFechados => Chamados.Count(c => c.Status == "Fechado");
        public int TotalPendentes => Chamados.Count(c => c.Status == "Pendente");
        public int TotalChamados => Chamados.Count;

        public ObservableCollection<string> UltimosChamados { get; set; } = new ObservableCollection<string>();

        public ICommand CarregarVisaoGeralCommand { get; }

        public HomeVM()
        {
            
            CarregarVisaoGeralCommand = new RelayCommand(CarregarVisaoGeral);
        }

        private void CarregarVisaoGeral(object obj)
        {
            UltimosChamados.Clear();

            foreach (var chamado in Chamados)
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

