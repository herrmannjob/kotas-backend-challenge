namespace PokemonAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using PokemonAPI.Models;
    using Newtonsoft.Json.Linq;

    public class PokemonService
    {
        private readonly HttpClient _httpClient;
        private readonly List<PokemonMaster> _masters;
        private readonly List<CapturedPokemon> _capturedPokemons;
        private static int _nextMasterId = 1;


        public PokemonService()
        {
            _httpClient = new HttpClient();
            _masters = new List<PokemonMaster>();
            _capturedPokemons = new List<CapturedPokemon>();
        }

        public PokemonMaster RegisterPokemonMaster(string name, int age, string cpf)
        {
            var pokemonMaster = new PokemonMaster
            {
                Id = _nextMasterId++,
                Name = name,
                Age = age,
                Cpf = cpf
            };

            _masters.Add(pokemonMaster);
            return pokemonMaster;
        }

        public async Task<CapturedPokemon> CapturePokemon(int masterId, int pokemonId)
        {
            var pokemon = await GetPokemonByIdAsync(pokemonId) ?? throw new Exception("Pokémon não encontrado.");
            var capturedPokemon = new CapturedPokemon
            {
                Id = _capturedPokemons.Count + 1,
                PokemonId = pokemon.Id,
                MasterId = masterId,
                Name = pokemon.Name,
                Sprite = pokemon.Sprite
            };

            _capturedPokemons.Add(capturedPokemon);

            return capturedPokemon;
        }

        public List<CapturedPokemon> GetCapturedPokemons()
        {
            return _capturedPokemons;
        }

        public async Task<Pokemon[]> GetRandomPokemonsAsync(int count = 10)
        {
            var pokemons = new List<Pokemon>();

            for (int i = 0; i < count; i++)
            {
                var randomId = new Random().Next(1, 1025);
                var pokemon = await GetPokemonByIdAsync(randomId);
                pokemons.Add(pokemon);
            }

            return pokemons.ToArray();
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JObject pokemonData = JObject.Parse(content);

                Console.WriteLine(pokemonData.ToString());

                var sprites = pokemonData["sprites"];
                if (sprites != null)
                {
                    string spriteUrl = (string)sprites["front_default"];

                    string base64Sprite = await ConvertImageToBase64(spriteUrl);

                    return new Pokemon
                    {
                        Id = (int)pokemonData["id"],
                        Name = (string)pokemonData["name"],
                        Sprite = base64Sprite
                    };
                }
                else
                {
                    throw new Exception("A propriedade 'sprites' não foi encontrada no JSON retornado.");
                }
            }
            else
            {
                throw new Exception("Erro ao chamar a PokeAPI.");
            }
        }

        private static async Task<string> ConvertImageToBase64(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new InvalidOperationException("A URL da imagem está vazia ou nula.");
            }

            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                throw new InvalidOperationException("A URL da imagem deve ser absoluta.");
            }
            using var client = new HttpClient();
            var imageBytes = await client.GetByteArrayAsync(imageUrl);
            return Convert.ToBase64String(imageBytes);
        }
    }


}