﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PgccApi.Models;

namespace PgccApi.Migrations
{
    [DbContext(typeof(PgccContext))]
    partial class PgccContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("PgccApi.Entities.Competition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Blurb");

                    b.Property<bool>("HasLeagueTable");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("PgccApi.Entities.Enquiry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Message");

                    b.Property<string>("Name");

                    b.Property<DateTime>("When");

                    b.HasKey("Id");

                    b.ToTable("Enquiries");
                });

            modelBuilder.Entity("PgccApi.Entities.Fixture", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CompetitionId");

                    b.Property<int?>("Ends1");

                    b.Property<int?>("Ends2");

                    b.Property<string>("Round");

                    b.Property<long>("SeasonId");

                    b.Property<int?>("Shots1");

                    b.Property<int?>("Shots2");

                    b.Property<long?>("Team1Id");

                    b.Property<string>("Team1OtherName");

                    b.Property<long?>("Team2Id");

                    b.Property<string>("Team2OtherName");

                    b.Property<DateTime>("When");

                    b.Property<bool>("isFinal");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("Team1Id");

                    b.HasIndex("Team2Id");

                    b.ToTable("Fixtures");
                });

            modelBuilder.Entity("PgccApi.Entities.NewsItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<DateTime>("When");

                    b.HasKey("Id");

                    b.ToTable("NewsItems");
                });

            modelBuilder.Entity("PgccApi.Entities.Rink", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CompetitionId");

                    b.Property<string>("Lead");

                    b.Property<long>("SeasonId");

                    b.Property<string>("Second");

                    b.Property<string>("Skip");

                    b.Property<string>("Third");

                    b.Property<short>("WasWinningRink")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Rinks");
                });

            modelBuilder.Entity("PgccApi.Entities.Season", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("PgccApi.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Token");

                    b.Property<DateTime?>("TokenExpiry");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PgccApi.Entities.Fixture", b =>
                {
                    b.HasOne("PgccApi.Entities.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PgccApi.Entities.Season", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PgccApi.Entities.Rink", "Team1")
                        .WithMany()
                        .HasForeignKey("Team1Id");

                    b.HasOne("PgccApi.Entities.Rink", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2Id");
                });

            modelBuilder.Entity("PgccApi.Entities.Rink", b =>
                {
                    b.HasOne("PgccApi.Entities.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PgccApi.Entities.Season", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
