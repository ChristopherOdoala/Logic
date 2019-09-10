﻿// <auto-generated />
using System;
using Logic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Logic.Migrations
{
    [DbContext(typeof(LogicDbContext))]
    [Migration("20190829154400_EmployeeTableUpdate")]
    partial class EmployeeTableUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Logic.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Division");

                    b.Property<Guid>("EmployeeId");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("departments");
                });

            modelBuilder.Entity("Logic.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("Logic.Model.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CurrentStage");

                    b.Property<Guid>("EmployeeId");

                    b.Property<int>("FinalStage");

                    b.Property<string>("ListOfExempted");

                    b.Property<int>("NoOfStages");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("status");
                });

            modelBuilder.Entity("Logic.Department", b =>
                {
                    b.HasOne("Logic.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Logic.Model.Status", b =>
                {
                    b.HasOne("Logic.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
