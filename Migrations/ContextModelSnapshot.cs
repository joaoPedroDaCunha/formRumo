﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rumo.Data;

#nullable disable

namespace Rumo.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Rumo.Models.Aet", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Sigla")
                        .HasColumnType("text");

                    b.Property<string>("VehicleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Aets");
                });

            modelBuilder.Entity("Rumo.Models.Vehicle", b =>
                {
                    b.Property<string>("Plate")
                        .HasColumnType("text");

                    b.Property<string>("Chassis")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("DuoDate")
                        .HasColumnType("date");

                    b.Property<string>("Mark")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Renavam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Situation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Plate");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Rumo.Models.Verser", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AetId")
                        .HasColumnType("uuid");

                    b.Property<string>("VehicleId")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("AetId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Versers");
                });

            modelBuilder.Entity("Rumo.Models.Aet", b =>
                {
                    b.HasOne("Rumo.Models.Vehicle", "Vehicle")
                        .WithMany("Aets")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Rumo.Models.Verser", b =>
                {
                    b.HasOne("Rumo.Models.Aet", "Aet")
                        .WithMany("Versers")
                        .HasForeignKey("AetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rumo.Models.Vehicle", "Vehicle")
                        .WithMany("Versers")
                        .HasForeignKey("VehicleId");

                    b.Navigation("Aet");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Rumo.Models.Aet", b =>
                {
                    b.Navigation("Versers");
                });

            modelBuilder.Entity("Rumo.Models.Vehicle", b =>
                {
                    b.Navigation("Aets");

                    b.Navigation("Versers");
                });
#pragma warning restore 612, 618
        }
    }
}
