using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SistemaChamados.Helpers;
using SistemaChamados.Models;
using SistemaChamados.ViewModels;


namespace SistemaChamados.ViewModels
{
    public class TicketsVM : ViewModelBase
    {
        public ObservableCollection<Chamado> Chamados { get; set; } = new ObservableCollection<Chamado>();

        private string _titulo;
        public string Titulo
        {
            get => _titulo;
            set { _titulo = value; OnPropertyChanged(); }
        }

        private string _descricao;
        public string Descricao
        {
            get => _descricao;
            set { _descricao = value; OnPropertyChanged(); }
        }

        private string _status = "Aberto";
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public ICommand CriarChamadoCommand { get; }

        public TicketsVM()
        {
            CriarChamadoCommand = new RelayCommand(CriarChamado);
        }

        private void CriarChamado(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Titulo))
            {
                Chamados.Add(new Chamado
                {
                    Titulo = Titulo,
                    Descricao = Descricao,
                    Status = Status
                });

                Titulo = string.Empty;
                Descricao = string.Empty;
                Status = "Aberto";
            }
        }
    }
}

    