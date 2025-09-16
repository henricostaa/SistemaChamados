using System;
using System.Windows.Input;

namespace SistemaChamadosWPF.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> executar;
        private readonly Predicate<object> podeExecutar;

        public RelayCommand(Action<object> executar, Predicate<object> podeExecutar = null)
        {
            this.executar = executar;
            this.podeExecutar = podeExecutar;
        }

        public bool CanExecute(object parameter) => podeExecutar == null || podeExecutar(parameter);

        public void Execute(object parameter) => executar(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}