using System;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace ContactsAPI.Data;

public class ContactDbContext :  DbContext
{
	public ContactDbContext(DbContextOptions<ContactDbContext> options): base (options) {}

	public DbSet<Contacts> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

