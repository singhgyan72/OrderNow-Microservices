﻿using OrderNow.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderNow.Services.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
     
    }
}
