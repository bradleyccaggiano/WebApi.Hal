﻿using System.Data.Entity;
using WebApi.Hal.Web.Models;

namespace WebApi.Hal.Web.Data
{
    public class BeerDbContext : DbContext, IBeerContext
    {
        public BeerDbContext(string connString) : base(connString){}

        public DbSet<Beer> Beers { get; set; }
    }

    public interface IBeerContext
    {
        DbSet<Beer> Beers { get; }
        int SaveChanges();
    }
}