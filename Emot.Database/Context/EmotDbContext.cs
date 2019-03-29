using Emot.Common.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emot.Database.Context
{
    public class EmotDbContext : DbContext
    {
        public EmotDbContext(DbContextOptions options) : base(options)
        {
        }

        protected EmotDbContext()
        {
        }

        public DbSet<Opinion> Opinions { get; set; }
    }
}
