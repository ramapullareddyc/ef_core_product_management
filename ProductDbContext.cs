using System;
using Microsoft.EntityFrameworkCore;
using EFCore.Models;

namespace EFCore.DataAccess
{
    public partial class ProductDbContext : DbContext
    {
        private static readonly bool _timestampBehaviorSet = SetLegacyTimestampBehavior();

        private static bool SetLegacyTimestampBehavior()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            return true;
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
