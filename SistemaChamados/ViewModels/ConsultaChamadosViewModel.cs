using SistemaChamados.Models;
using SistemaChamados.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using SistemaChamados.Helpers;
using SistemaChamados.Models;
using SistemaChamados.ViewModels;
using SistemaChamados.Services;


namespace SistemaChamados.ViewModels
{
    public class ConsultaChamadosViewModel : ViewModelBase
    {
        public ObservableCollection<Chamado> Chamados { get; set; } = new ObservableCollection<Chamado>();
        public ObservableCollection<Chamado> ChamadosFiltrados { get; set; } = new ObservableCollection<Chamado>();

        private string filtro;
        public string Filtro
        {
            get => filtro;
            set
            {
                SetProperty(ref filtro, value);
                FiltrarChamados();
            }
        }

        public ICommand AtualizarCommand { get; }

        public ConsultaChamadosViewModel()
        {
            AtualizarCommand = new RelayCommand(AtualizarChamados);
            AtualizarChamados();
        }

        private void AtualizarChamados(object obj = null)
        {
            Chamados.Clear();
            foreach (var chamado in ChamadoRepository.Listar())
                Chamados.Add(chamado);

            FiltrarChamados();
        }

        private void FiltrarChamados()
        {
            ChamadosFiltrados.Clear();

            var filtrados = string.IsNullOrWhiteSpace(Filtro)
                ? Chamados
                : Chamados.Where(c =>
                    c.Usuario.Contains(Filtro, System.StringComparison.OrdinalIgnoreCase) ||
                    c.Titulo.Contains(Filtro, System.StringComparison.OrdinalIgnoreCase) ||
                    c.Categoria.Contains(Filtro, System.StringComparison.OrdinalIgnoreCase) ||
                    c.Prioridade.Contains(Filtro, System.StringComparison.OrdinalIgnoreCase));

            foreach (var chamado in filtrados)
                ChamadosFiltrados.Add(chamado);
        }
    }
}