// Importa o pacote Entity Framework Core para trabalhar com banco de dados.
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
// Importa os modelos do projeto, especificamente o ApplicationDbContext.
using Steammostra.Models;
var builder = WebApplication.CreateBuilder(args);
// Adiciona servi�os ao container.
// Registra o servi�o Razor Pages, que ser� usado para gerar e gerenciar asp�ginas do aplicativo.
builder.Services.AddRazorPages();
// Configura o servi�o de contexto de banco de dados com MySQL.
// Obt�m a string de conex�o da configura��o e usa MySQL como o provedor debanco de dados.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
// Configura o pipeline de requisi��o HTTP.
// Se o ambiente n�o for de desenvolvimento, usa o manipulador de exce��espara redirecionar para uma p�gina de erro.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // Habilita o HSTS (HTTP Strict Transport Security) para maior seguran�aem produ��o.
    app.UseHsts();
}
// Redireciona requisi��es HTTP para HTTPS automaticamente.
app.UseHttpsRedirection();
// Serve arquivos est�ticos, como CSS, JavaScript e imagens.
app.UseStaticFiles();
app.UseRouting(); // Habilita o roteamento, que define como as URLs s�omapeadas para as a��es.
app.UseAuthorization(); // Habilita a autoriza��o, garantindo que o acesso arecursos seja controlado.
// Mapeia as p�ginas Razor para os endpoints no aplicativo.
app.MapRazorPages();
// Testa a conex�o com o banco de dados antes de executar o aplicativo.
TestDatabaseConnection(app);
// Inicia a aplica��o web.
app.Run();
// Fun��o que testa a conex�o com o banco de dados.
void TestDatabaseConnection(WebApplication app)
{
    // Cria um escopo para obter os servi�os do container de depend�ncia.
    using (var scope = app.Services.CreateScope())
    {

        var services = scope.ServiceProvider;
        try
        {
            // Obt�m o contexto de banco de dados injetado.
            var context =
            services.GetRequiredService<ApplicationDbContext>();
            // Verifica se � poss�vel conectar ao banco de dados.
            if (context.Database.CanConnect())
            {
                Console.WriteLine("Connection to the database successful!");
                // Conex�o bem-sucedida.
            }
            else
            {
                Console.WriteLine("Failed to connect to the database."); //Falha na conex�o.
            }
        }
        catch (Exception ex)
        {
            // Captura qualquer exce��o e imprime uma mensagem de erro.
            Console.WriteLine($"An exception occurred: {ex.Message}");
        }
    }
}