var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddMvc(option => option.EnableEndpointRouting = true);

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Руководство для использования запросов",
        Description = "Полное руководство для использования запросов находящихся в проекте"
    });

    option.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v2",
        Title = "Руководство для использования запросов",
        Description = "Полное руководство для использования запросов находящихся в проекте"
    });

    string PathFile = Path.Combine(System.AppContext.BaseDirectory, "Restaraunt_Lobanov.xml");
    option.IncludeXmlComments(PathFile);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Запросы POST");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Запросы GET");
});

app.UseAuthorization();

app.UseStaticFiles();           
app.MapRazorPages();            

app.Run();