using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Steammostra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    // Construtor que injeta o ApplicationDbContext para acesso ao banco de dados.
    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    // Propriedade para armazenar a lista de cadastro
    public IList<Logins> Login { get; set; }

    public async Task<IList<Logins>> GetLoginsAsync()
    {
        return await _context.Logins.ToListAsync();
    }

    // M�todo ass�ncrono chamado quando a p�gina � acessada via GET.
    public async Task OnGetAsync(IList<Logins> logins)
    {
        // Carrega todos os cadastros do banco de dados.
        Login = logins;
    }

    // M�todo ass�ncrono chamado quando um formul�rio de adi��o � submetido.
    public async Task<IActionResult> OnPostAddAsync(Logins newmostrasteam)
    {
        if (!ModelState.IsValid)
        {
            // Retorna a mesma p�gina se o modelo n�o for v�lido.
            return Page();
        }

        // Adiciona o novo estudante ao contexto do banco de dados.
        _context.Logins.Add(newmostrasteam);
        await _context.SaveChangesAsync(); // Salva as mudan�as no banco de dados.

        // Redireciona para a mesma p�gina para refletir a adi��o.
        return RedirectToPage();
    }

    // M�todo ass�ncrono chamado quando um formul�rio de dele��o � submetido.
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // Encontra o estudante pelo ID.
        var mostrasteam = await _context.Logins.FindAsync(id);

        if (mostrasteam != null)
        {
            // Remove o estudante do contexto do banco de dados.
            _context.Logins.Remove(mostrasteam);
            await _context.SaveChangesAsync(); // Salva as mudan�as no banco de dados.
        }

        // Redireciona para a mesma p�gina para refletir a remo��o.
        return RedirectToPage();
    }
}
