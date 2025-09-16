using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SistemaChamados.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string nome = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }

        protected bool SetProperty<T>(ref T campo, T valor, [CallerMemberName] string nome = null)
        {
            if (!Equals(campo, valor))
            {
                campo = valor;
                OnPropertyChanged(nome);
                return true;
            }
            return false;
        }
    }
}