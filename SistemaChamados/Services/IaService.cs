namespace SistemaChamados.Services
{
    public static class IAService
    {
        public static string GerarSugestao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                return "Descreva o problema para gerar uma sugestão.";

            if (descricao.Contains("rede"))
                return "Verifique o cabo de rede e reinicie o roteador.";

            if (descricao.Contains("impressora"))
                return "Confira se há papel e reinicie a impressora.";
            return "Encaminhe para o suporte técnico.";

        }
    }
}