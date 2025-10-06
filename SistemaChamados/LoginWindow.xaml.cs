using SistemaChamados;
using SistemaChamados.Data;
using SistemaChamados.Models;
using System.Windows;

namespace SistemaChamados
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
           
         
        }

       

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string senha = txtSenha.Password.Trim();

            using (var db = new AppDbContext())
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.Username == username);

                if (usuario == null)
                {
                    MessageBox.Show("❌ Usuário não encontrado.", "Erro de Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (usuario.SenhaHash != senha)
                {
                    MessageBox.Show("❌ Senha incorreta.", "Erro de Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (usuario.Status != "Ativo")
                {
                    MessageBox.Show("⚠ Usuário inativo.", "Erro de Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Login bem-sucedido
                MainWindow main = new MainWindow(); // ou passe o usuário logado se quiser
                main.Show();
                this.Close();
            }
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }
    }
}