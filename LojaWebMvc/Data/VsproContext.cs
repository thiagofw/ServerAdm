using System;
using System.Collections.Generic;
using LojaWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaWebMvc.Data;

public partial class VsproContext : DbContext
{
    public VsproContext()
    {
    }

    public VsproContext(DbContextOptions<VsproContext> options)
        : base(options)
    {
    }
    public DbSet<Department>  Department{ get; set; }
    public DbSet<Seller> Seller {get; set;}
    public DbSet<SalesRecord> SalesRecord {get; set;}
    public DbSet<Login> Login{get; set;}
    public DbSet<Server>Server{get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
