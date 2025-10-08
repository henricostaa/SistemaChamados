using Microsoft.VisualBasic;
using SistemaChamados.Data;
using SistemaChamados.Helpers;
using SistemaChamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SistemaChamados.ViewModels
{
    class UsuarioVM : ViewModelBase
    {
        private Usuario usuario => Sessao.UsuarioLogado;
        public string Nome => usuario?.Username;
        public string Email => usuario?.Email;
        public string Departamento => usuario?.Role;
        public string Telefone => usuario?.Telefone;
        public string Endereço => usuario?.Endereco;

        public DateTime? DataNascimento => usuario?.DataNascimento;



        public UsuarioVM()
        {
           
        }

        
    
        
    }
}
