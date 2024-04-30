var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
var employees = new List<Employee>();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});

app.MapGet("/employees", () => employees);
app.MapGet("/employees{id}", (int id) => employees.FirstOrDefault(h => h.Id == id));
app.MapPost("/employees", (Employee employee) => employees.Add(employee));
app.MapPut("/employees", (Employee employee) => {
    var index = employees.FindIndex(h => h.Id == employee.Id);
    if(index < 0)
    {
        throw new Exception("Not found");
    }
    employees[index] = employee;
});
app.MapDelete("/employees{id}", (int id) => {
    var index = employees.FindIndex(h => h.Id == id);
    if (index < 0)
    {
        throw new Exception("Not found");
    }
    employees.RemoveAt(index);
});

app.Run();

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }
    public string Position { get; set; }
}