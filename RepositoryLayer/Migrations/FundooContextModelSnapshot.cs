﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer.Services;

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(FundooContext))]
    partial class FundooContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RepositoryLayer.Services.Entity.Label", b =>
                {
                    b.Property<int>("LabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LabelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NoteId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LabelId");

                    b.HasIndex("NoteId");

                    b.HasIndex("UserId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("RepositoryLayer.Services.Entity.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchieve")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReminder")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Remainder")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NoteId");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("RepositoryLayer.Services.Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RepositoryLayer.Services.Entity.Label", b =>
                {
                    b.HasOne("RepositoryLayer.Services.Entity.Note", "notes")
                        .WithMany()
                        .HasForeignKey("NoteId");

                    b.HasOne("RepositoryLayer.Services.Entity.User", "users")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("notes");

                    b.Navigation("users");
                });

            modelBuilder.Entity("RepositoryLayer.Services.Entity.Note", b =>
                {
                    b.HasOne("RepositoryLayer.Services.Entity.User", "users")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("users");
                });
#pragma warning restore 612, 618
        }
    }
}
