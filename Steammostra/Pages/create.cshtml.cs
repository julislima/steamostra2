using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Steammostra.Models;

namespace Steammostra.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Logins Logins { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Adicionar o login ao banco de dados
            _context.Logins.Add(Logins);
            _context.SaveChanges();

            // Redirecionar para a página inicial
            return RedirectToPage("/home"); // Assegure-se de que "Index" é a página inicial que você deseja redirecionar
        }
    }
}
