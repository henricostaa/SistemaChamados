using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SistemaChamados.Helpers;
using SistemaChamados.Views;

namespace SistemaChamados.ViewModels
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand TicketsCommand { get; set; }
        public ICommand ConfiguracoesCommand { get; set; }
        public ICommand UsuarioCommand { get; set; }
        public ICommand HistoricoCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeView();
        private void Tickets(object obj) => CurrentView = new TicketsView();
        private void Configuracoes(object obj) => CurrentView = new ConfiguracoesView();
        private void Usuario(object obj) => CurrentView = new UsuarioView();
        private void Historico(object obj) => CurrentView = new HistoricoView();
        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            TicketsCommand = new RelayCommand(Tickets);
            ConfiguracoesCommand = new RelayCommand(Configuracoes);
            UsuarioCommand = new RelayCommand(Usuario);
            HistoricoCommand = new RelayCommand(Historico);
            CurrentView = new HomeView();
        }
    }
}
