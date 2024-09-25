namespace PokemonAPI.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Evolution[] Evolutions { get; set; }
        public string Sprite { get; set; }
    }

    public class Evolution
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}