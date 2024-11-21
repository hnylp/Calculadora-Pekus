namespace WebApplication1.Models
{
    public class Operacao
    {
        public int Id { get; set; }
        public float A { get; set; }
        public float B { get; set; }
        public string OperacaoSinal { get; set; } = string.Empty;
        public float Resultado { get; set; }
        public DateTime HoraOperacao { get; set; }

    }
}