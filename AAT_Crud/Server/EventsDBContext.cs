using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AAT_Crud.Entities;

namespace AAT_Crud
{
    public class EventsDBContext : IdentityDbContext
    {
        public virtual DbSet<EventRegistrationEntity> EventRegistration { get; set; }
        public virtual DbSet<EventsEntity> Events { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public EventsDBContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
