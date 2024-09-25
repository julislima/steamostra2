// Importa o pacote Entity Framework Core para trabalhar com banco de dados.
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
// Importa os modelos do projeto, especificamente o ApplicationDbContext.
using Steammostra.Models;
var builder = WebApplication.CreateBuilder(args);
// Adiciona serviços ao container.
// Registra o serviço Razor Pages, que será usado para gerar e gerenciar aspáginas do aplicativo.
builder.Services.AddRazorPages();
// Configura o serviço de contexto de banco de dados com MySQL.
// Obtém a string de conexão da configuração e usa MySQL como o provedor debanco de dados.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
// Configura o pipeline de requisição HTTP.
// Se o ambiente não for de desenvolvimento, usa o manipulador de exceçõespara redirecionar para uma página de erro.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // Habilita o HSTS (HTTP Strict Transport Security) para maior segurançaem produção.
    app.UseHsts();
}
// Redireciona requisições HTTP para HTTPS automaticamente.
app.UseHttpsRedirection();
// Serve arquivos estáticos, como CSS, JavaScript e imagens.
app.UseStaticFiles();
app.UseRouting(); // Habilita o roteamento, que define como as URLs sãomapeadas para as ações.
app.UseAuthorization(); // Habilita a autorização, garantindo que o acesso arecursos seja controlado.
// Mapeia as páginas Razor para os endpoints no aplicativo.
app.MapRazorPages();
// Testa a conexão com o banco de dados antes de executar o aplicativo.
TestDatabaseConnection(app);
// Inicia a aplicação web.
app.Run();
// Função que testa a conexão com o banco de dados.
void TestDatabaseConnection(WebApplication app)
{
    // Cria um escopo para obter os serviços do container de dependência.
    using (var scope = app.Services.CreateScope())
    {

        var services = scope.ServiceProvider;
        try
        {
            // Obtém o contexto de banco de dados injetado.
            var context =
            services.GetRequiredService<ApplicationDbContext>();
            // Verifica se é possível conectar ao banco de dados.
            if (context.Database.CanConnect())
            {
                Console.WriteLine("Connection to the database successful!");
                // Conexão bem-sucedida.
            }
            else
            {
                Console.WriteLine("Failed to connect to the database."); //Falha na conexão.
            }
        }
        catch (Exception ex)
        {
            // Captura qualquer exceção e imprime uma mensagem de erro.
            Console.WriteLine($"An exception occurred: {ex.Message}");
        }
    }
}