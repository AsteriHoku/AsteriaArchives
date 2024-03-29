using System.Text.Json;
using AsteriaArchives.Services;
using DevJokes.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsteriaArchives.Controllers;

public class BackEndController(DevJokesService devJokesService, IHttpClientFactory clientFactory) : Controller
{
    private readonly DevJokesService _devJokesService = devJokesService;

    [Route("/Dev")]
    public async Task<IActionResult> DevJoke()
    {
        DevJoke devJoke;
        int rand = new Random().Next(0, 2);
        if (rand == 1)
        {
            using var client = clientFactory.CreateClient();
            var uri = new Uri("https://backend-omega-seven.vercel.app/api/getjoke");
            var httpResponse = await client.GetAsync(uri);
            var content = await httpResponse.Content.ReadAsStringAsync();
            devJoke = JsonSerializer.Deserialize<List<DevJoke>>(content)?[0];
        }
        else
        {
            devJoke = await devJokesService.NonAPIDevJoke();
        }

        ViewData["Message"] = "Hello from ViewData!";
        ViewBag.binary = await devJokesService.GetSetDevVM(devJoke.punchline);
        ViewBag.question = devJoke.question;
        ViewBag.punchline = devJoke.punchline;
        ViewBag.aspaction = "DevJoke";

        MemoryStream imageStream = await devJokesService.GenerateDevJokeCard(devJoke);
        ViewBag.JokeCard = imageStream;

        return View();
    }

    [Route("/Geek")]
    public async Task<IActionResult> GeekJoke()
    {
        GeekJoke geekJoke;
        int rand = new Random().Next(0, 2);
        if (rand == 1)
        {
            using var client = clientFactory.CreateClient();
            var uri = new Uri("https://geek-jokes.sameerkumar.website/api?format=json");
            var httpResponse = await client.GetAsync(uri);
            var content = await httpResponse.Content.ReadAsStringAsync();
            geekJoke = JsonSerializer.Deserialize<GeekJoke>(content);
        }
        else
        {
            geekJoke = await devJokesService.NonAPIGeekJoke();
        }
        
        MemoryStream imageStream = await devJokesService.GenerateGeekJokeCard(geekJoke);
        ViewBag.JokeCard = imageStream;

        return View(geekJoke);
    }

    [Route("/Programming")]
    public async Task<IActionResult> ProgrammingJoke()
    {
        using var client = clientFactory.CreateClient();
        var uri = new Uri("https://official-joke-api.appspot.com/jokes/programming/random#");
        var httpResponse = await client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var programmingJoke = JsonSerializer.Deserialize<List<ProgrammingJoke>>(content)?[0];
        programmingJoke.binary = await devJokesService.GetSetDevVM(programmingJoke.punchline);
        return View(programmingJoke);
    }

    [Route("/NSFWProgramming")]
    public async Task<IActionResult> NSFWProgrammingJoke()
    {
        NSFWjoke programmingJoke;
        int rand = new Random().Next(0, 6);
        if (rand == 1)
        {
            programmingJoke = await devJokesService.NonAPINSFWProgrammingJoke();
        }
        else
        {
            using var client = clientFactory.CreateClient();
            var uri = new Uri("https://v2.jokeapi.dev/joke/Programming");
            var httpResponse = await client.GetAsync(uri);
            var content = await httpResponse.Content.ReadAsStringAsync();
            programmingJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        }

        var binIn = programmingJoke?.joke ?? programmingJoke?.delivery;
        programmingJoke.lang = await devJokesService.GetSetDevVM(binIn);
        return View(programmingJoke);
    }

    [Route("/Spooky")]
    public async Task<IActionResult> SpookyJoke()
    {
        using var client = clientFactory.CreateClient();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Spooky");
        var httpResponse = await client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var spookyJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(spookyJoke);
    }

    [Route("/Pun")]
    public async Task<IActionResult> PunJoke()
    {
        using var client = clientFactory.CreateClient();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Pun");
        var httpResponse = await client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var punJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(punJoke);
    }

    [Route("/Misc")]
    public async Task<IActionResult> MiscJoke()
    {
        using var client = clientFactory.CreateClient();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Miscellaneous");
        var httpResponse = await client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var miscJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(miscJoke);
    }
}