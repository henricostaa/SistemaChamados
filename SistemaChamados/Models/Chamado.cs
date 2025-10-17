using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SistemaChamados.Models
{
    public class Chamado
    {
        [Key]
        public int ChamadoId { get; set; }

        [Required, MaxLength(200)]
        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        public string? Categoria { get; set; }

        public string? Prioridade { get; set; }

        private string? _status;
        public string? Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));




        public DateTime DataAbertura { get; set; } = DateTime.Now;

        public DateTime? DataAtribuicao { get; set; }

        public DateTime? DataFechamento { get; set; }

        public string? NomeArquivoAnexo { get; set; }

        public string? CaminhoArquivoAnexo { get; set; }

        // RELACIONAMENTO COM USUÁRIO (quem foi atribuído)
        public int? AtribuidoAId { get; set; }
        [ForeignKey("AtribuidoAId")]
        public Usuario? AtribuidoA { get; set; }

        // RELACIONAMENTO COM USUÁRIO SOLICITANTE (quem criou o chamado)
        public int SolicitanteId { get; set; }   // precisa existir
        [ForeignKey("SolicitanteId")]
        public Usuario? Solicitante { get; set; }

    }
}