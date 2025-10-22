using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaChamados.Views
{
    /// <summary>
    /// Interação lógica para MostrarDetalhesView.xam
    /// </summary>
    public partial class MostrarDetalhesView : UserControl
    {
        public MostrarDetalhesView()
        {
            InitializeComponent();
        }
        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            // Acessa o NavigationVM e troca a view atual
            if (Application.Current.MainWindow is MainWindow mainWindow &&
                mainWindow.DataContext is ViewModels.NavigationVM navVM)
            {
                navVM.CurrentView = new HistoricoView();
            }
        }

    }
}
