using S20L.lib;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
PhoneBook first = new PhoneBook();
app.MapGet("/contactname/{name}/{item}", first.GetContactinfobyname);
app.MapGet("/contactnumber/{number}/{item}", first.GetContactinfobynumber);
app.MapGet("/allcontacts/all", first.GetAllContacts);
app.MapPost("/addcontact", first.add_contact);
app.MapGet("/delete/{number}", first.Delete_contact);
app.MapPost("/edit", first.Edit_contact);
app.Run();

public class Proram1
{

}
