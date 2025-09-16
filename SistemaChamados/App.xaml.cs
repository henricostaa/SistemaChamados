using System.Configuration;
using System.Data;
using System.Windows;
using SistemaChamados.Views;

namespace SistemaChamados
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var login = new LoginView();
            login.Show();
        }

    }

}
