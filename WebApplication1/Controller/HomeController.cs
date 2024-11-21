using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private static int contadorOperacao = 0;
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {

            _context = context;
        }


        public ActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Index(float A, float B, string sinal)
        {
            contadorOperacao++;

            float resultado = sinal switch
            {
                "Adição" => Somar(A, B),
                "Subtração" => Subtrair(A, B),
                "Multiplicação" => Multiplicar(A, B),
                "Divisão" => Dividir(A, B),
                _ => 0
            };
      

            var novaOperacao = new Operacao
            {
                A = A,
                B = B,
                OperacaoSinal = sinal,
                Resultado = resultado,
                HoraOperacao = DateTime.Now,


            };

            _context.Operacoes.Add(novaOperacao);
            _context.SaveChanges();

            int idOperacao = novaOperacao.Id;

            TempData["Mensagem"] = "Cálculo realizado com sucesso!";
            TempData["IdOperacao"] = idOperacao;

            return RedirectToAction("Index");


        }

        public IActionResult Resultado(int? idOperacao)
        {
            
                var todasOperacoes = _context.Operacoes
                                             .OrderByDescending(o => o.HoraOperacao)
                                             .ToList();
                ViewData["TodasOperacoes"] = todasOperacoes;
       
            return View();
        }


        private float Somar(float A, float B) => A + B;
        private float Subtrair(float A, float B) => A - B;
        private float Multiplicar(float A, float B) => A * B;
        private float Dividir(float A, float B) => B != 0 ? A / B : float.NaN; // Retorna ERRO por tentar dividir por zero

    }
}   