﻿// <auto-generated />
using System;
using El_Muro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace El_Muro.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20231120233701_PrimeraMigracion")]
    partial class PrimeraMigracion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Comentario", b =>
                {
                    b.Property<int>("ComentarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ComentarioTexto")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<DateTime>("Fecha_Creacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Fecha_Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MensajeId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("ComentarioId");

                    b.HasIndex("MensajeId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("Mensaje", b =>
                {
                    b.Property<int>("MensajeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha_Creacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Fecha_Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MensajeTexto")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("MensajeId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Mensajes");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<DateTime>("Fecha_Creacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Fecha_Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Comentario", b =>
                {
                    b.HasOne("Mensaje", "Mensaje")
                        .WithMany()
                        .HasForeignKey("MensajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mensaje");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Mensaje", b =>
                {
                    b.HasOne("Usuario", null)
                        .WithMany("AssocMensajes")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Navigation("AssocMensajes");
                });
#pragma warning restore 612, 618
        }
    }
}
