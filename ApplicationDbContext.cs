using System;
using Microsoft.EntityFrameworkCore;
using EFCore.Models;

namespace EFCore.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        // No constructors defined in this partial class file
    }

    // Separate static class for initialization
    public static class EFCoreConfiguration
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (!_initialized)
            {
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                _initialized = true;
            }
        }
    }
}
