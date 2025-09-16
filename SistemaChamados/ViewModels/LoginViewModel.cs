using SistemaChamadosWPF.Helpers;
using System.Windows.Input;
using System.Windows;

namespace SistemaChamadosWPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string usuario;
        public string Usuario
        {
            get => usuario;
            set => SetProperty(ref usuario, value);
        }

        private string senha;
        public string Senha
        {
            get => senha;
            set => SetProperty(ref senha, value);
        }

        public ICommand EntrarCommand { get; }

        public LoginViewModel()
        {
            EntrarCommand = new RelayCommand(ExecutarLogin);
        }

        private void ExecutarLogin(object parametro)
        {
            if (Usuario == "admin" && Senha == "123")
                MessageBox.Show("Login bem-sucedido!");
            else
                MessageBox.Show("Usuário ou senha inválidos.");
        }
    }
}