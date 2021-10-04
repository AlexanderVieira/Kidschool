﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Universal.EBI.Educators.API.Data;

namespace Universal.EBI.Educators.API.Migrations
{
    [DbContext(typeof(EducatorContext))]
    partial class EducatorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Universal.EBI.Educators.API.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Complement")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("EducatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PublicPlace")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("EducatorId")
                        .IsUnique();

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("Universal.EBI.Educators.API.Models.Educator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Excluded")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("FunctionType")
                        .HasColumnType("int");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Educators");
                });

            modelBuilder.Entity("Universal.EBI.Educators.API.Models.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EducatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(13)");

                    b.Property<int>("PhoneType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EducatorId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Universal.EBI.Educators.API.Models.Address", b =>
                {
                    b.HasOne("Universal.EBI.Educators.API.Models.Educator", "Educator")
                        .WithOne("Address")
                        .HasForeignKey("Universal.EBI.Educators.API.Models.Address", "EducatorId")
                        .IsRequired();

                    b.Navigation("Educator");
                });

            modelBuilder.Entity("Universal.EBI.Educators.API.Models.Educator", b =>
                {
                    b.OwnsOne("Universal.EBI.Core.DomainObjects.Cpf", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("EducatorId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("varchar(11)")
                                .HasColumnName("Cpf");

                            b1.HasKey("EducatorId");

                            b1.ToTable("Educators");

                            b1.WithOwner()
                                .HasForeignKey("EducatorId");
                        });

                    b.OwnsOne("Universal.EBI.Core.DomainObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("EducatorId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("varchar(254)")
                                .HasColumnName("Email");

                            b1.HasKey("EducatorId");

                            b1.ToTable("Educators");

                            b1.WithOwner()
                                .HasForeignKey("EducatorId");
                        });

                    b.Navigation("Cpf");

                    b.Navigation("Email");
                });

            modelBuilder.Entity("Universal.EBI.Educators.API.Models.Phone", b =>
                {
                    b.HasOne("Universal.EBI.Educators.API.Models.Educator", "Educator")
                        .WithMany("Phones")
                        .HasForeignKey("EducatorId")
                        .IsRequired();

                    b.Navigation("Educator");
                });

            modelBuilder.Entity("Universal.EBI.Educators.API.Models.Educator", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}