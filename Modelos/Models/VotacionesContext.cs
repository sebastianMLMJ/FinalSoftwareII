using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Modelos.Models;

public partial class VotacionesContext : DbContext
{
    public VotacionesContext()
    {
    }

    public VotacionesContext(DbContextOptions<VotacionesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidato> Candidatos { get; set; }

    public virtual DbSet<FaseCrearCandidato> FaseCrearCandidatos { get; set; }

    public virtual DbSet<FaseVotacione> FaseVotaciones { get; set; }

    public virtual DbSet<Fraude> Fraudes { get; set; }

    public virtual DbSet<Votacione> Votaciones { get; set; }

    public virtual DbSet<Votante> Votantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=127.0.0.1;userid=root;password=root123;database=votaciones;TreatTinyAsBoolean=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.HasKey(e => e.IdCandidato).HasName("PRIMARY");

            entity.ToTable("candidatos");

            entity.HasIndex(e => e.Nombre, "nombre").IsUnique();

            entity.Property(e => e.IdCandidato).HasColumnName("id_candidato");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Partido)
                .HasMaxLength(100)
                .HasColumnName("partido");
        });

        modelBuilder.Entity<FaseCrearCandidato>(entity =>
        {
            entity.HasKey(e => e.IdFase).HasName("PRIMARY");

            entity.ToTable("fase_crear_candidatos");

            entity.Property(e => e.IdFase).HasColumnName("id_fase");
            entity.Property(e => e.Activa)
                .HasColumnType("tinyint(1)")
                .HasColumnName("activa");
        });

        modelBuilder.Entity<FaseVotacione>(entity =>
        {
            entity.HasKey(e => e.IdFaseVotaciones).HasName("PRIMARY");

            entity.ToTable("fase_votaciones");

            entity.Property(e => e.IdFaseVotaciones).HasColumnName("id_faseVotaciones");
            entity.Property(e => e.Activa)
                .HasColumnType("tinyint(1)")
                .HasColumnName("activa");
        });

        modelBuilder.Entity<Fraude>(entity =>
        {
            entity.HasKey(e => e.IdFraude).HasName("PRIMARY");

            entity.ToTable("fraude");

            entity.Property(e => e.IdFraude).HasColumnName("id_fraude");
            entity.Property(e => e.Fraudes).HasColumnName("fraudes");
        });

        modelBuilder.Entity<Votacione>(entity =>
        {
            entity.HasKey(e => e.IdVoto).HasName("PRIMARY");

            entity.ToTable("votaciones");

            entity.HasIndex(e => e.IdCandidato, "fk_voto_candidato");

            entity.HasIndex(e => e.IdVotante, "fk_voto_votante");

            entity.Property(e => e.IdVoto).HasColumnName("id_voto");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdCandidato).HasColumnName("id_candidato");
            entity.Property(e => e.IdVotante).HasColumnName("id_votante");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("ip");

            entity.HasOne(d => d.IdCandidatoNavigation).WithMany(p => p.Votaciones)
                .HasForeignKey(d => d.IdCandidato)
                .HasConstraintName("fk_voto_candidato");

            entity.HasOne(d => d.IdVotanteNavigation).WithMany(p => p.Votaciones)
                .HasForeignKey(d => d.IdVotante)
                .HasConstraintName("fk_voto_votante");
        });

        modelBuilder.Entity<Votante>(entity =>
        {
            entity.HasKey(e => e.IdVotante).HasName("PRIMARY");

            entity.ToTable("votante");

            entity.Property(e => e.IdVotante).HasColumnName("id_votante");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
