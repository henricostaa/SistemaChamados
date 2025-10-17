using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SistemaChamados.ViewModels
{
    public class ChatBotVM : INotifyPropertyChanged
    {
        private string _mensagemUsuario;

        // Lista de mensagens do chat
        public ObservableCollection<ChatMessage> ChatMessages { get; set; }

        // Texto atual digitado pelo usuário
        public string MensagemUsuario
        {
            get => _mensagemUsuario;
            set
            {
                _mensagemUsuario = value;
                OnPropertyChanged();
            }
        }

        // Comando para enviar mensagem
        public ICommand EnviarMensagemCommand { get; }

        public ChatBotVM()
        {
            ChatMessages = new ObservableCollection<ChatMessage>();
            EnviarMensagemCommand = new RelayCommand(EnviarMensagem, PodeEnviar);

            // Mensagem inicial do bot
            ChatMessages.Add(new ChatMessage
            {
                Text = "Olá! 👋 Sou a inteligência artificial da InovaTech. Como posso ajudar você hoje?",
                Foreground = Brushes.White,
                GradientStart = (Color)ColorConverter.ConvertFromString("#3949AB"),
                GradientEnd = (Color)ColorConverter.ConvertFromString("#5C6BC0"),
                Alignment = "Left"
            });
        }

        private bool PodeEnviar(object parameter)
        {
            return !string.IsNullOrWhiteSpace(MensagemUsuario);
        }

        private void EnviarMensagem(object parameter)
        {
            // Adiciona mensagem do usuário
            ChatMessages.Add(new ChatMessage
            {
                Text = MensagemUsuario,
                Foreground = Brushes.White,
                GradientStart = (Color)ColorConverter.ConvertFromString("#8E24AA"),
                GradientEnd = (Color)ColorConverter.ConvertFromString("#BA68C8"),
                Alignment = "Right"
            });

            string resposta = GerarResposta(MensagemUsuario);

            // Resposta do bot
            ChatMessages.Add(new ChatMessage
            {
                Text = resposta,
                Foreground = Brushes.White,
                GradientStart = (Color)ColorConverter.ConvertFromString("#3949AB"),
                GradientEnd = (Color)ColorConverter.ConvertFromString("#5C6BC0"),
                Alignment = "Left"
            });

            MensagemUsuario = string.Empty;
        }

        // Simulação simples de resposta
        private string GerarResposta(string pergunta)
        {
            pergunta = pergunta.ToLower();

            if (pergunta.Contains("olá") || pergunta.Contains("oi"))
                return "Oi! 😄 Que bom te ver por aqui!";
            else if (pergunta.Contains("ajuda") || pergunta.Contains("problema"))
                return "Claro! Me diga o que está acontecendo e eu tentarei ajudar.";
            else if (pergunta.Contains("obrigado"))
                return "De nada! 💜";
            else if (pergunta.Contains("adeus") || pergunta.Contains("tchau"))
                return "Até logo! 👋";

            return "Interessante! Pode me explicar um pouco mais sobre isso?";
        }

        // Notificação de mudança de propriedade
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nome = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }

    // Modelo de mensagem individual
    public class ChatMessage
    {
        public string Text { get; set; }
        
        public Brush Foreground { get; set; }
        public string Alignment { get; set; }

        public Color GradientStart { get; set; }

        public Color GradientEnd { get; set; }
    }

    // Implementação simples de ICommand
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
