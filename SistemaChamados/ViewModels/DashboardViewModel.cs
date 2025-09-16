using SistemaChamados.Helpers;
using System.Windows.Input;
using System.Windows;

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
            MessageBox.Show("Tela de Abrir Chamado ainda não implementada.");
        }

        private void ConsultarChamado(object obj)
        {
            MessageBox.Show("Tela de consulta ainda não implementada.");
        }

        private void AbrirRelatorios(object obj)
        {
            MessageBox.Show("Tela de relatórios ainda não implementada.");
        }
    }
}