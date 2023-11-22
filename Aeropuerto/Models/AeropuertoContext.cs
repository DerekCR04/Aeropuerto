using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aeropuerto.Models;

public partial class AeropuertoContext : DbContext
{
    public AeropuertoContext()
    {
    }

    public AeropuertoContext(DbContextOptions<AeropuertoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aerolinea> Aerolineas { get; set; }

    public virtual DbSet<Avione> Aviones { get; set; }

    public virtual DbSet<Destino> Destinos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Pasajero> Pasajeros { get; set; }

    public virtual DbSet<Piloto> Pilotos { get; set; }

    public virtual DbSet<Vuelo> Vuelos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aerolinea>(entity =>
        {
            entity.HasKey(e => e.AerolineaId).HasName("PK__aeroline__039221DD8E4D4854");

            entity.ToTable("aerolineas");

            entity.Property(e => e.AerolineaId).HasColumnName("aerolinea_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Avione>(entity =>
        {
            entity.HasKey(e => e.AvionId).HasName("PK__aviones__3E87DB0E9C415705");

            entity.ToTable("aviones");

            entity.Property(e => e.AvionId).HasColumnName("avion_id");
            entity.Property(e => e.AerolineaId).HasColumnName("aerolinea_id");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modelo");

            entity.HasOne(d => d.Aerolinea).WithMany(p => p.Aviones)
                .HasForeignKey(d => d.AerolineaId)
                .HasConstraintName("FK__aviones__aerolin__38996AB5");
        });

        modelBuilder.Entity<Destino>(entity =>
        {
            entity.HasKey(e => e.DestinoId).HasName("PK__destinos__3B72E2A8E7558CD1");

            entity.ToTable("destinos");

            entity.Property(e => e.DestinoId).HasColumnName("destino_id");
            entity.Property(e => e.NombreCiudad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_ciudad");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pais");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK__empleado__6FBB65FDF4837F51");

            entity.ToTable("empleados");

            entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cargo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasMany(d => d.Vuelos).WithMany(p => p.Empleados)
                .UsingEntity<Dictionary<string, object>>(
                    "TrabajanEn",
                    r => r.HasOne<Vuelo>().WithMany()
                        .HasForeignKey("VueloId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__trabajan___vuelo__4D94879B"),
                    l => l.HasOne<Empleado>().WithMany()
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__trabajan___emple__4CA06362"),
                    j =>
                    {
                        j.HasKey("EmpleadoId", "VueloId").HasName("PK__trabajan__3E07469AD5255370");
                        j.ToTable("trabajan_en");
                        j.IndexerProperty<int>("EmpleadoId").HasColumnName("empleado_id");
                        j.IndexerProperty<int>("VueloId").HasColumnName("vuelo_id");
                    });
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.HorarioId).HasName("PK__horarios__5A3872283ED05032");

            entity.ToTable("horarios");

            entity.Property(e => e.HorarioId).HasColumnName("horario_id");
            entity.Property(e => e.HoraLlegada).HasColumnName("hora_llegada");
            entity.Property(e => e.HoraSalida).HasColumnName("hora_salida");
            entity.Property(e => e.VueloId).HasColumnName("vuelo_id");

            entity.HasOne(d => d.Vuelo).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.VueloId)
                .HasConstraintName("FK__horarios__vuelo___47DBAE45");
        });

        modelBuilder.Entity<Pasajero>(entity =>
        {
            entity.HasKey(e => e.PasajeroId).HasName("PK__pasajero__8B0A15E59B188D4E");

            entity.ToTable("pasajeros");

            entity.Property(e => e.PasajeroId).HasColumnName("pasajero_id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.DestinoId).HasColumnName("destino_id");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Destino).WithMany(p => p.Pasajeros)
                .HasForeignKey(d => d.DestinoId)
                .HasConstraintName("FK__pasajeros__desti__403A8C7D");
        });

        modelBuilder.Entity<Piloto>(entity =>
        {
            entity.HasKey(e => e.PilotoId).HasName("PK__pilotos__BE4F4C67B605DF08");

            entity.ToTable("pilotos");

            entity.Property(e => e.PilotoId).HasColumnName("piloto_id");
            entity.Property(e => e.AerolineaId).HasColumnName("aerolinea_id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Aerolinea).WithMany(p => p.Pilotos)
                .HasForeignKey(d => d.AerolineaId)
                .HasConstraintName("FK__pilotos__aerolin__3B75D760");

            entity.HasMany(d => d.Avions).WithMany(p => p.Pilotos)
                .UsingEntity<Dictionary<string, object>>(
                    "PilotosAvione",
                    r => r.HasOne<Avione>().WithMany()
                        .HasForeignKey("AvionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__pilotos_a__avion__5165187F"),
                    l => l.HasOne<Piloto>().WithMany()
                        .HasForeignKey("PilotoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__pilotos_a__pilot__5070F446"),
                    j =>
                    {
                        j.HasKey("PilotoId", "AvionId").HasName("PK__pilotos___4DA731D77D7D2B03");
                        j.ToTable("pilotos_aviones");
                        j.IndexerProperty<int>("PilotoId").HasColumnName("piloto_id");
                        j.IndexerProperty<int>("AvionId").HasColumnName("avion_id");
                    });
        });

        modelBuilder.Entity<Vuelo>(entity =>
        {
            entity.HasKey(e => e.VueloId).HasName("PK__vuelos__1BC23674AA37D9F3");

            entity.ToTable("vuelos");

            entity.Property(e => e.VueloId).HasColumnName("vuelo_id");
            entity.Property(e => e.AerolineaId).HasColumnName("aerolinea_id");
            entity.Property(e => e.AvionId).HasColumnName("avion_id");
            entity.Property(e => e.DestinoId).HasColumnName("destino_id");

            entity.HasOne(d => d.Aerolinea).WithMany(p => p.Vuelos)
                .HasForeignKey(d => d.AerolineaId)
                .HasConstraintName("FK__vuelos__aeroline__440B1D61");

            entity.HasOne(d => d.Avion).WithMany(p => p.Vuelos)
                .HasForeignKey(d => d.AvionId)
                .HasConstraintName("FK__vuelos__avion_id__4316F928");

            entity.HasOne(d => d.Destino).WithMany(p => p.Vuelos)
                .HasForeignKey(d => d.DestinoId)
                .HasConstraintName("FK__vuelos__destino___44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
