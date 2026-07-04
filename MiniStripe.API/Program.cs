using Microsoft.EntityFrameworkCore;
using MiniStripe.Application.Commands;
using MiniStripe.Application.Queries;
using MiniStripe.Domain.Interfaces;
using MiniStripe.Infrastructure.Persistence;
using MiniStripe.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MiniStripeDbContext>(options => 
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<CreatePaymentHandler>();
builder.Services.AddScoped<ConfirmPaymentHandler>();
builder.Services.AddScoped<GetPaymentHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

