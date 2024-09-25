using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Data;
using PokemonAPI.Models;
using PokemonAPI.Services;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;
    private readonly PokemonContext _context;

    public PokemonController(PokemonService pokemonService, PokemonContext context)
    {
        _pokemonService = pokemonService;
        _context = context;
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomPokemons()
    {
        var pokemons = await _pokemonService.GetRandomPokemonsAsync();
        return Ok(pokemons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPokemonById(int id)
    {
        var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
        return Ok(pokemon);
    }

    [HttpPost("master")]
    public IActionResult RegisterPokemonMaster([FromBody] PokemonMaster master)
    {
        _context.PokemonMasters.Add(master);
        var registeredMaster = _pokemonService.RegisterPokemonMaster(master.Name, master.Age, master.Cpf);
        return Ok(registeredMaster);
    }

    [HttpPost("capture")]
    public async Task<IActionResult> CapturePokemon([FromBody] CaptureRequest request)
    {
        var captured = await _pokemonService.CapturePokemon(request.MasterId, request.PokemonId);
        return Ok(captured);
    }

    [HttpGet("captured")]
    public IActionResult GetCapturedPokemons()
    {
        var capturedPokemons = _pokemonService.GetCapturedPokemons();
        return Ok(capturedPokemons);
    }
}
