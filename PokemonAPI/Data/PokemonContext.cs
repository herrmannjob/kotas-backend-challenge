using PokemonAPI.Models;

namespace PokemonAPI.Data
{
    using Microsoft.EntityFrameworkCore;

    public class PokemonContext : DbContext
    {
        public DbSet<PokemonMaster> PokemonMasters { get; set; }
        public DbSet<CapturedPokemon> CapturedPokemons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=pokemon.db");
        }
    }


}