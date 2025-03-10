﻿// <auto-generated />
using System;
using Meeting.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Meeting.Migrations
{
    [DbContext(typeof(MeetingDbcontext))]
    [Migration("20240521222701_CreateDB")]
    partial class CreateDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Meeting.Models.Corporate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Clint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Decion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descussion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("M_Place")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Side")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Corporates");
                });

            modelBuilder.Entity("Meeting.Models.Experience", b =>
                {
                    b.Property<int>("ExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CorporateId")
                        .HasColumnType("int");

                    b.Property<int>("Quantiy")
                        .HasColumnType("int");

                    b.Property<string>("Service")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("ExperienceId");

                    b.HasIndex("CorporateId");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("Meeting.Models.cutomer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cutomers");
                });

            modelBuilder.Entity("Meeting.Models.Experience", b =>
                {
                    b.HasOne("Meeting.Models.Corporate", "Corporates")
                        .WithMany("Experiences")
                        .HasForeignKey("CorporateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Corporates");
                });

            modelBuilder.Entity("Meeting.Models.Corporate", b =>
                {
                    b.Navigation("Experiences");
                });
#pragma warning restore 612, 618
        }
    }
}
