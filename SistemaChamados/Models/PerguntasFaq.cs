namespace SistemaChamados.Models
{
    public class PerguntaFaq
    {
        public int Ordem { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public string Categoria { get; set; }
        public bool AdicionarEmFaq { get; set; }
    }
}