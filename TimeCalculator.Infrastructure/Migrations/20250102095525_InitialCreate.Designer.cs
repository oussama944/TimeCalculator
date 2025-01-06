﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeCalculator.Infrastructure.Data;

#nullable disable

namespace TimeCalculator.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250102095525_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("TimeCalculator.Domain.Entities.TimeEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("AfternoonEnd")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("AfternoonStart")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("MinimumLunchBreak")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("MorningEnd")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("MorningStart")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Date")
                        .IsUnique();

                    b.ToTable("TimeEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
