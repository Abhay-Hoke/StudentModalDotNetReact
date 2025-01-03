﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentAdmissionPortal.Data;

#nullable disable

namespace StudentAdmissionPortal.Migrations
{
    [DbContext(typeof(StudentModalDbContext))]
    [Migration("20241110123142_initalcreate")]
    partial class initalcreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentAdmissionPortal.Models.FamilyMembers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("NationalityId")
                        .HasColumnType("int");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NationalityId");

                    b.HasIndex("StudentId");

                    b.ToTable("FamilyMembers");
                });

            modelBuilder.Entity("StudentAdmissionPortal.Models.Nationality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Nationality");
                });

            modelBuilder.Entity("StudentAdmissionPortal.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("NationalityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NationalityId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("StudentAdmissionPortal.Models.FamilyMembers", b =>
                {
                    b.HasOne("StudentAdmissionPortal.Models.Nationality", "Nationality")
                        .WithMany("FamilyMembers")
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StudentAdmissionPortal.Models.Student", "Student")
                        .WithMany("FamilyMembers")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nationality");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentAdmissionPortal.Models.Student", b =>
                {
                    b.HasOne("StudentAdmissionPortal.Models.Nationality", "Nationality")
                        .WithMany("Students")
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Nationality");
                });

            modelBuilder.Entity("StudentAdmissionPortal.Models.Nationality", b =>
                {
                    b.Navigation("FamilyMembers");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentAdmissionPortal.Models.Student", b =>
                {
                    b.Navigation("FamilyMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
