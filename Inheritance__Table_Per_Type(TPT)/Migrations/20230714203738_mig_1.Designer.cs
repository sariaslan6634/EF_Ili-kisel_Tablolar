﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Inheritance__Table_Per_Type_TPT_.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230714203738_mig_1")]
    partial class mig_1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Customer", b =>
                {
                    b.HasBaseType("Person");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.HasBaseType("Person");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("Technician", b =>
                {
                    b.HasBaseType("Employee");

                    b.Property<string>("Branch")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Technicians", (string)null);
                });

            modelBuilder.Entity("Customer", b =>
                {
                    b.HasOne("Person", null)
                        .WithOne()
                        .HasForeignKey("Customer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.HasOne("Person", null)
                        .WithOne()
                        .HasForeignKey("Employee", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Technician", b =>
                {
                    b.HasOne("Employee", null)
                        .WithOne()
                        .HasForeignKey("Technician", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
