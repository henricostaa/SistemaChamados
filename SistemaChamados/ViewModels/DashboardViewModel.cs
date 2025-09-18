using SistemaChamados.Helpers;
using System.Windows.Input;
using System.Windows;
using SistemaChamados.Views;

namespace SistemaChamados.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public ICommand AbrirChamadoCommand { get; }
        public ICommand ConsultarChamadoCommand { get; }
        public ICommand RelatoriosCommand { get; }

        public DashboardViewModel()
        {
            AbrirChamadoCommand = new RelayCommand(AbrirChamado);
            ConsultarChamadoCommand = new RelayCommand(ConsultarChamado);
            RelatoriosCommand = new RelayCommand(AbrirRelatorios);
        }

        private void AbrirChamado(object obj)
        {
            var abrirChamado = new AbrirChamadoView();
            abrirChamado.Show();
        }

        private void ConsultarChamado(object obj)
        {
            var consultaChamados = new ConsultaChamadosView();
            consultaChamados.Show();
        }

        private void AbrirRelatorios(object obj)
        {
            MessageBox.Show("Tela de relatórios ainda não implementada.");
        }
    }
}