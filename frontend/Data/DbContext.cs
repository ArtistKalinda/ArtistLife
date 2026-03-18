using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Contracts;
using WorldOfArt.Models;

namespace WorldOfArt.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}
