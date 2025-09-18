using SistemaChamados.Helpers;
using SistemaChamados.Models;
using SistemaChamados.Services;
using System.Windows.Input;
using System.Windows;
using SistemaChamados.Helpers;
using SistemaChamados.Services;
using SistemaChamados.ViewModels;

namespace SistemaChamados.ViewModels
{
    public class AbrirChamadoViewModel : ViewModelBase
    {
        private Chamado chamado = new Chamado();
        public Chamado Chamado
        {
            get => chamado;
            set => SetProperty(ref chamado, value);
        }

        private string sugestaoIA;
        public string SugestaoIA
        {
            get => sugestaoIA;
            set => SetProperty(ref sugestaoIA, value);
        }

        public ICommand GerarSugestaoCommand { get; }
        public ICommand SalvarChamadoCommand { get; }

        public AbrirChamadoViewModel()
        {
            GerarSugestaoCommand = new RelayCommand(GerarSugestao);
            SalvarChamadoCommand = new RelayCommand(SalvarChamado);
        }

        private void GerarSugestao(object obj)
        {
            SugestaoIA = IAService.GerarSugestao(Chamado.Descricao);
        }

        private void SalvarChamado(object obj)
        {
            ChamadoRepository.Adicionar(new Chamado
            {
                Usuario   =  Sessao.UsuarioLogado,
                Titulo    =  Chamado.Titulo,
                Descricao =  Chamado.Descricao,
                Categoria =  Chamado.Categoria,
                Prioridade = Chamado.Prioridade
            });

            MessageBox.Show("Chamado salvo com sucesso!");
            Chamado = new Chamado();
            SugestaoIA = string.Empty;
        }
    }
}