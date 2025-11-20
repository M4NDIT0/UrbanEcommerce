using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Server.Modelos;

public partial class DbtiendaBlazorContext : DbContext
{
    public DbtiendaBlazorContext()
    {
    }

    public DbtiendaBlazorContext(DbContextOptions<DbtiendaBlazorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoVariante> ProductoVariantes { get; set; }

    public virtual DbSet<ProductoImagen> ProductoImagenes { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10E6DCE8EE");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__AAA5CEC2D008617A");

            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleVe__IdPro__286302EC");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__IdVen__276EDEB3");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__2EC8D2AC277522E5");

            entity.ToTable("Persona");

            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210A276D374");

            entity.ToTable("Producto");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Imagen).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioOferta).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__IdCate__1367E606");
        });

        modelBuilder.Entity<ProductoVariante>(entity =>
        {
            entity.HasKey(e => e.IdVariante).HasName("PK__Producto__B223ACACA09B8B64");

            entity.ToTable("ProductoVariante");

            entity.Property(e => e.NombreColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CodigoHex)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SKU)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Variantes)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__ProductoV__IdPro__2B3F6F97");
        });

        modelBuilder.Entity<ProductoImagen>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__Producto__B6D29ADBF1C8E8A3");

            entity.ToTable("ProductoImagen");

            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EsPrincipal)
                .HasDefaultValue(false);
            entity.Property(e => e.Orden)
                .HasDefaultValue(0);

            entity.HasOne(d => d.IdVarianteNavigation).WithMany(p => p.Imagenes)
                .HasForeignKey(d => d.IdVariante)
                .HasConstraintName("FK__ProductoI__IdVar__2E1BDC42");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__BC1240BD5E47FA0D");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK__Venta__IdPersona__239E4DCF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
