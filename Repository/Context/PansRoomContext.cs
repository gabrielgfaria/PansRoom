﻿using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Models;

#nullable disable

namespace Repository.Context
{
    public partial class PansRoomContext : DbContext
    {
        private string _relativePath = @"../pansroom.db";

        public PansRoomContext()
        {
            CreateDb();
        }

        public PansRoomContext(DbContextOptions<PansRoomContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Disc> Discs { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var relativeConnectionString = $"Data Source={_relativePath}";

            var builder = new SqliteConnectionStringBuilder(relativeConnectionString);
            builder.DataSource = Path.GetFullPath(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    builder.DataSource));

            var connectionString = builder.ToString();

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.ToTable("Collection");

                entity.HasOne(d => d.Disc)
                    .WithMany()
                    .HasForeignKey(d => d.DiscId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Collectio__DiscI__2E1BDC42");
            });

            modelBuilder.Entity<Disc>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Discs)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Discs__ArtistId__2C3393D0");
            });

            modelBuilder.Entity<WishList>(entity =>
            {
                entity.ToTable("WishList");

                entity.HasOne(d => d.Disc)
                    .WithMany()
                    .HasForeignKey(d => d.DiscId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WishList__DiscId__2D27B809");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        //Creates a .db file if one doesn´t already exists
        private void CreateDb()
        {
            if (!File.Exists(_relativePath))
                base.Database.Migrate();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
