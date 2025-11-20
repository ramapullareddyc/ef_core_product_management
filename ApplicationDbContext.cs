using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using EFCore.Models;
using System;

namespace EFCore.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
