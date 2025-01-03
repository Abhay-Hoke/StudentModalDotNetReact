﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudentAdmissionPortal.Models;

namespace StudentAdmissionPortal.Data;

public partial class StudentModalDbContext : DbContext
{
    public StudentModalDbContext()
    {
    }

    public StudentModalDbContext(DbContextOptions<StudentModalDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQlLocalDb;Initial Catalog=StudentModalDb1;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
                .HasMany(s => s.FamilyMembers)
                .WithOne(fm => fm.Student)
                .HasForeignKey(fm => fm.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FamilyMembers>()
            .HasOne(fm => fm.Nationality)
            .WithMany(n => n.FamilyMembers)
            .HasForeignKey(fm => fm.NationalityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Nationality)
            .WithMany(n => n.Students)
            .HasForeignKey(s => s.NationalityId)
            .OnDelete(DeleteBehavior.Cascade);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<Student> Student { get; set; } = default!;

    public DbSet<Nationality> Nationality { get; set; } = default!;

    public DbSet<FamilyMembers> FamilyMembers { get; set; } = default!;
}
