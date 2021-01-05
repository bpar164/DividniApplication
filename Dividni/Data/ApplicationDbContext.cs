using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dividni.Models;

namespace Dividni.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Simple> Simple { get; set; }

        public DbSet<Dividni.Models.Advanced> Advanced { get; set; }

        public DbSet<Dividni.Models.Assessment> Assessment { get; set; }

        public DbSet<Dividni.Models.QuestionBank> QuestionBank { get; set; }
    }
}
