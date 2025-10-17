using SistemaChamados.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace SistemaChamados.ViewModels
{
    public class FAQViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PerguntaFaq> Perguntas { get; set; }
        public ICollectionView PerguntasFiltradas { get; set; }

        private string _textoBusca;
        public string TextoBusca
        {
            get => _textoBusca;
            set
            {
                _textoBusca = value;
                OnPropertyChanged(nameof(TextoBusca));
                PerguntasFiltradas.Refresh();
            }
        }

        private string _ordemBusca;
        public string OrdemBusca
        {
            get => _ordemBusca;
            set
            {
                _ordemBusca = value;
                OnPropertyChanged(nameof(OrdemBusca));
                PerguntasFiltradas.Refresh();
            }
        }

        public FAQViewModel()
        {
            Perguntas = new ObservableCollection<PerguntaFaq>
            {
                new PerguntaFaq { Ordem = 1, Pergunta = "Como Criar um Ticket ?", Resposta = "Acesse pelo menu 'Criar Ticket'", Categoria = "Acesso" },
                new PerguntaFaq { Ordem = 2, Pergunta = "Como Alterar minha Senha ?", Resposta = "Vá em Configurações > Alterar Senha", Categoria = "Segurança" },
                new PerguntaFaq { Ordem = 3, Pergunta = "Como Verificar o Status do Ticket ?", Resposta = "Acesse a seção 'Meus Tickets'", Categoria = "Tickets" },
                new PerguntaFaq { Ordem = 4, Pergunta = "Como Alterar Meu Perfil ?", Resposta = "Vá em Configurações > Meu Perfil", Categoria = "Perfil" },
            };

            PerguntasFiltradas = CollectionViewSource.GetDefaultView(Perguntas);
            PerguntasFiltradas.Filter = FiltrarPerguntas;
        }

        private bool FiltrarPerguntas(object obj)
        {
            if (obj is PerguntaFaq pergunta)
            {
                bool textoOk = string.IsNullOrWhiteSpace(TextoBusca) ||
                    pergunta.Pergunta.Contains(TextoBusca, System.StringComparison.OrdinalIgnoreCase) ||
                    pergunta.Resposta.Contains(TextoBusca, System.StringComparison.OrdinalIgnoreCase) ||
                    pergunta.Categoria.Contains(TextoBusca, System.StringComparison.OrdinalIgnoreCase);

                bool ordemOk = string.IsNullOrWhiteSpace(OrdemBusca) ||
                    pergunta.Ordem.ToString().Contains(OrdemBusca);

                return textoOk && ordemOk;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}