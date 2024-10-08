﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokemonAPI.Data;

#nullable disable

namespace PokemonAPI.Migrations
{
    [DbContext(typeof(PokemonContext))]
    [Migration("20240925192140_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("PokemonAPI.Models.CapturedPokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MasterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PokemonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CapturedPokemons");
                });

            modelBuilder.Entity("PokemonAPI.Models.PokemonMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cpf")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PokemonMasters");
                });
#pragma warning restore 612, 618
        }
    }
}
