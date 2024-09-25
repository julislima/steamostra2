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

            // Redirecionar para a p�gina inicial
            return RedirectToPage("/home"); // Assegure-se de que "Index" � a p�gina inicial que voc� deseja redirecionar
        }
    }
}
