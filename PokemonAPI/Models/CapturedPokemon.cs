namespace PokemonAPI.Models
{
    public class CapturedPokemon
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int MasterId { get; set; }
        public string Name { get; set; }
        public string Sprite { get; set; }

    }

}