﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Functions.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("PersonId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            Description = "...",
                            PersonId = 1,
                            Price = 10
                        },
                        new
                        {
                            OrderId = 2,
                            Description = "...",
                            PersonId = 2,
                            Price = 20
                        },
                        new
                        {
                            OrderId = 3,
                            Description = "...",
                            PersonId = 4,
                            Price = 30
                        },
                        new
                        {
                            OrderId = 4,
                            Description = "...",
                            PersonId = 5,
                            Price = 40
                        },
                        new
                        {
                            OrderId = 5,
                            Description = "...",
                            PersonId = 1,
                            Price = 50
                        },
                        new
                        {
                            OrderId = 6,
                            Description = "...",
                            PersonId = 6,
                            Price = 60
                        },
                        new
                        {
                            OrderId = 7,
                            Description = "...",
                            PersonId = 7,
                            Price = 70
                        },
                        new
                        {
                            OrderId = 8,
                            Description = "...",
                            PersonId = 1,
                            Price = 80
                        },
                        new
                        {
                            OrderId = 9,
                            Description = "...",
                            PersonId = 8,
                            Price = 90
                        },
                        new
                        {
                            OrderId = 10,
                            Description = "...",
                            PersonId = 9,
                            Price = 100
                        },
                        new
                        {
                            OrderId = 11,
                            Description = "...",
                            PersonId = 1,
                            Price = 110
                        },
                        new
                        {
                            OrderId = 12,
                            Description = "...",
                            PersonId = 2,
                            Price = 120
                        },
                        new
                        {
                            OrderId = 13,
                            Description = "...",
                            PersonId = 2,
                            Price = 130
                        },
                        new
                        {
                            OrderId = 14,
                            Description = "...",
                            PersonId = 3,
                            Price = 140
                        },
                        new
                        {
                            OrderId = 15,
                            Description = "...",
                            PersonId = 1,
                            Price = 150
                        },
                        new
                        {
                            OrderId = 16,
                            Description = "...",
                            PersonId = 4,
                            Price = 160
                        },
                        new
                        {
                            OrderId = 17,
                            Description = "...",
                            PersonId = 1,
                            Price = 170
                        },
                        new
                        {
                            OrderId = 18,
                            Description = "...",
                            PersonId = 1,
                            Price = 180
                        },
                        new
                        {
                            OrderId = 19,
                            Description = "...",
                            PersonId = 5,
                            Price = 190
                        },
                        new
                        {
                            OrderId = 20,
                            Description = "...",
                            PersonId = 6,
                            Price = 200
                        },
                        new
                        {
                            OrderId = 21,
                            Description = "...",
                            PersonId = 1,
                            Price = 210
                        },
                        new
                        {
                            OrderId = 22,
                            Description = "...",
                            PersonId = 7,
                            Price = 220
                        },
                        new
                        {
                            OrderId = 23,
                            Description = "...",
                            PersonId = 7,
                            Price = 230
                        },
                        new
                        {
                            OrderId = 24,
                            Description = "...",
                            PersonId = 8,
                            Price = 240
                        },
                        new
                        {
                            OrderId = 25,
                            Description = "...",
                            PersonId = 1,
                            Price = 250
                        },
                        new
                        {
                            OrderId = 26,
                            Description = "...",
                            PersonId = 1,
                            Price = 260
                        },
                        new
                        {
                            OrderId = 27,
                            Description = "...",
                            PersonId = 9,
                            Price = 270
                        },
                        new
                        {
                            OrderId = 28,
                            Description = "...",
                            PersonId = 9,
                            Price = 280
                        },
                        new
                        {
                            OrderId = 29,
                            Description = "...",
                            PersonId = 9,
                            Price = 290
                        },
                        new
                        {
                            OrderId = 30,
                            Description = "...",
                            PersonId = 2,
                            Price = 300
                        },
                        new
                        {
                            OrderId = 31,
                            Description = "...",
                            PersonId = 3,
                            Price = 310
                        },
                        new
                        {
                            OrderId = 32,
                            Description = "...",
                            PersonId = 1,
                            Price = 320
                        },
                        new
                        {
                            OrderId = 33,
                            Description = "...",
                            PersonId = 1,
                            Price = 330
                        },
                        new
                        {
                            OrderId = 34,
                            Description = "...",
                            PersonId = 1,
                            Price = 340
                        },
                        new
                        {
                            OrderId = 35,
                            Description = "...",
                            PersonId = 5,
                            Price = 350
                        },
                        new
                        {
                            OrderId = 36,
                            Description = "...",
                            PersonId = 1,
                            Price = 360
                        },
                        new
                        {
                            OrderId = 37,
                            Description = "...",
                            PersonId = 5,
                            Price = 370
                        },
                        new
                        {
                            OrderId = 38,
                            Description = "...",
                            PersonId = 1,
                            Price = 380
                        },
                        new
                        {
                            OrderId = 39,
                            Description = "...",
                            PersonId = 1,
                            Price = 390
                        },
                        new
                        {
                            OrderId = 40,
                            Description = "...",
                            PersonId = 1,
                            Price = 400
                        });
                });

            modelBuilder.Entity("Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            Name = "Ayşe"
                        },
                        new
                        {
                            PersonId = 2,
                            Name = "Hilmi"
                        },
                        new
                        {
                            PersonId = 3,
                            Name = "Raziye"
                        },
                        new
                        {
                            PersonId = 4,
                            Name = "Süleyman"
                        },
                        new
                        {
                            PersonId = 5,
                            Name = "Fadime"
                        },
                        new
                        {
                            PersonId = 6,
                            Name = "Şuayip"
                        },
                        new
                        {
                            PersonId = 7,
                            Name = "Lale"
                        },
                        new
                        {
                            PersonId = 8,
                            Name = "Jale"
                        },
                        new
                        {
                            PersonId = 9,
                            Name = "Rıfkı"
                        },
                        new
                        {
                            PersonId = 10,
                            Name = "Muaviye"
                        });
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("Person", "Person")
                        .WithMany("Orders")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Person", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
