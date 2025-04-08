﻿// <auto-generated />
using System;
using CadastroBasico.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CadastroBasico.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250408200800_InitialCreateWithUserLevels")]
    partial class InitialCreateWithUserLevels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("CadastroBasico.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserLevelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserLevelId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CadastroBasico.Models.UserLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserLevels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Acesso total ao sistema",
                            Name = "Administrador"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Acesso às funcionalidades básicas do sistema",
                            Name = "Cliente"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Acesso às funcionalidades operacionais do sistema",
                            Name = "Funcionário"
                        });
                });

            modelBuilder.Entity("CadastroBasico.Models.User", b =>
                {
                    b.HasOne("CadastroBasico.Models.UserLevel", "UserLevel")
                        .WithMany("Users")
                        .HasForeignKey("UserLevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UserLevel");
                });

            modelBuilder.Entity("CadastroBasico.Models.UserLevel", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
