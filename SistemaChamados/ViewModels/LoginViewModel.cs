using SistemaChamados.Helpers;
using System.Windows.Input;
using System.Windows;
using SistemaChamados.Views;

namespace SistemaChamados.ViewModels
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
            {
               
                var home = new HomeView();
                

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is LoginWindow)
                    {
                        window.Close();
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválidos.");
            }
                
        }
    }
}