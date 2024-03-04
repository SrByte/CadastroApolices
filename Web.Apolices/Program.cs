using Apolices.Web.Services.IServices;
using Apolices.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);



#pragma warning disable CS8604 // Possible null reference argument.

builder.Services.AddHttpClient<ISeguroService, SeguroService>(
	c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ApolicesAPI"])
);
#pragma warning restore CS8604 // Possible null reference argument.


builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
