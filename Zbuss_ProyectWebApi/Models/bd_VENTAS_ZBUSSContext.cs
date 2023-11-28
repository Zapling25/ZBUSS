﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSZbuss_Proyect.Models
{
    public partial class bd_VENTAS_ZBUSSContext : DbContext
    {
        public bd_VENTAS_ZBUSSContext()
        {
        }

        public bd_VENTAS_ZBUSSContext(DbContextOptions<bd_VENTAS_ZBUSSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAsientosBus> TbAsientosBus { get; set; }
        public virtual DbSet<TbBus> TbBus { get; set; }
        public virtual DbSet<TbDetalleVenta> TbDetalleVenta { get; set; }
        public virtual DbSet<TbDetalleViaje> TbDetalleViaje { get; set; }
        public virtual DbSet<TbPasajero> TbPasajero { get; set; }
        public virtual DbSet<TbRoles> TbRoles { get; set; }
        public virtual DbSet<TbUsuarios> TbUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-1NF3EJV;Initial Catalog=bd_VENTAS_ZBUSS;Integrated Security=SSPI; User ID=sa;Password=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbAsientosBus>(entity =>
            {
                entity.HasKey(e => e.Idasiento)
                    .HasName("PK_ID_ASIENTO");

                entity.ToTable("tb_ASIENTOS_BUS");

                entity.Property(e => e.Idasiento).HasColumnName("idasiento");

                entity.Property(e => e.CodAsiento).HasColumnName("cod_asiento");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Idbus).HasColumnName("idbus");

                entity.Property(e => e.Inclinacion)
                    .IsRequired()
                    .HasColumnName("inclinacion")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.PisoBus).HasColumnName("piso_bus");

                entity.Property(e => e.Precio)
                    .HasColumnName("precio")
                    .HasColumnType("numeric(4, 2)");
            });

            modelBuilder.Entity<TbBus>(entity =>
            {
                entity.HasKey(e => e.Idbus)
                    .HasName("PK_ID_BUS");

                entity.ToTable("tb_BUS");

                entity.Property(e => e.Idbus).HasColumnName("idbus");

                entity.Property(e => e.Capacidad).HasColumnName("capacidad");

                entity.Property(e => e.Pisos).HasColumnName("pisos");

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasColumnName("placa")
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbDetalleVenta>(entity =>
            {
                entity.HasKey(e => e.Idventa)
                    .HasName("PK_ID_VENTA");

                entity.ToTable("tb_DETALLE_VENTA");

                entity.Property(e => e.Idventa).HasColumnName("idventa");

                entity.Property(e => e.Descuento)
                    .HasColumnName("descuento")
                    .HasColumnType("numeric(8, 2)");

                entity.Property(e => e.FechaVenta)
                    .HasColumnName("fecha_venta")
                    .HasColumnType("date");

                entity.Property(e => e.Idasiento).HasColumnName("idasiento");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.Igv)
                    .HasColumnName("igv")
                    .HasColumnType("numeric(3, 1)");

                entity.Property(e => e.MetodoPago)
                    .IsRequired()
                    .HasColumnName("metodo_pago")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal)
                    .HasColumnName("sub_total")
                    .HasColumnType("numeric(4, 2)");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("numeric(8, 2)");
            });

            modelBuilder.Entity<TbDetalleViaje>(entity =>
            {
                entity.HasKey(e => e.Idviaje)
                    .HasName("PK_ID_VIAJE");

                entity.ToTable("tb_DETALLE_VIAJE");

                entity.Property(e => e.Idviaje).HasColumnName("idviaje");

                entity.Property(e => e.DuracionViaje).HasColumnName("duracion_viaje");

                entity.Property(e => e.FechaSalida)
                    .HasColumnName("fecha_salida")
                    .HasColumnType("date");

                entity.Property(e => e.HoraLlegada).HasColumnName("hora_llegada");

                entity.Property(e => e.HoraSalida).HasColumnName("hora_salida");

                entity.Property(e => e.Idbus).HasColumnName("idbus");

                entity.Property(e => e.NroAsientos).HasColumnName("nro_asientos");

                entity.Property(e => e.PuntoLlegada)
                    .IsRequired()
                    .HasColumnName("punto_llegada")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PuntoPartida)
                    .IsRequired()
                    .HasColumnName("punto_partida")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdbusNavigation)
                    .WithMany(p => p.TbDetalleViaje)
                    .HasForeignKey(d => d.Idbus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BUS_DETALLE_VIAJE");
            });

            modelBuilder.Entity<TbPasajero>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PK_ID_USUARIOZ");

                entity.ToTable("tb_PASAJERO");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.ApeMaterno)
                    .IsRequired()
                    .HasColumnName("ape_materno")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ApePaterno)
                    .IsRequired()
                    .HasColumnName("ape_paterno")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasColumnName("celular")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnName("correo")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNac)
                    .HasColumnName("fecha_nac")
                    .HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NroDocumento)
                    .IsRequired()
                    .HasColumnName("nro_documento")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnName("sexo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TpoDocumento)
                    .IsRequired()
                    .HasColumnName("tpo_documento")
                    .HasMaxLength(22)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbRoles>(entity =>
            {
                entity.ToTable("tb_ROLES");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<TbUsuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("tb_USUARIOS");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasColumnName("celular")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnName("contrasena")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnName("correo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnName("nombres")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NroDocumento)
                    .IsRequired()
                    .HasColumnName("nro_documento")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDoc)
                    .IsRequired()
                    .HasColumnName("tipo_doc")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.TbUsuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROL_USUARIOS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
