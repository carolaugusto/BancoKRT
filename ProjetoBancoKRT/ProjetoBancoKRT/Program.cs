using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using ProjetoBancoKRT.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var serviceUrl = configuration["AWS:ServiceURL"];
    var accessKey = configuration["AWS:AccessKey"];
    var secretKey = configuration["AWS:SecretKey"];

    var credentials = new BasicAWSCredentials(accessKey, secretKey);
    var clientConfig = new AmazonDynamoDBConfig
    {
        ServiceURL = serviceUrl
    };

    return new AmazonDynamoDBClient(credentials, clientConfig);
});
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>(sp =>
{
    var client = sp.GetRequiredService<IAmazonDynamoDB>();
    return new DynamoDBContext(client);
});


builder.Services.AddScoped<IContaBancariaService, ContaBancariaService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
