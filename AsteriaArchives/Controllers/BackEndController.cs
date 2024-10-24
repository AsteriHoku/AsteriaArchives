using System.Text.Json;
using AsteriaArchives.Services;
using DevJokes.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsteriaArchives.Controllers;

public class BackEndController(DevJokesService devJokesService,
    RgbBinaryService rgbService,
    IHttpClientFactory clientFactory) : Controller {
    
    [Route("/RgbBinary")]
    public async Task<IActionResult> RgbBinary()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> GetRgbBinary([FromBody] InputModel input)
    {
        var imgBytes = await rgbService.Create(input.textInput);
        return File(imgBytes, "image/png");
    }
    
    [Route("/Dev")]
    public async Task<IActionResult> DevJoke() {
        DevJoke devJoke;
        int rand = new Random().Next(0, 2);
        if (rand == 1) {
            using var client = clientFactory.CreateClient();
            var res = await client.GetAsync(new Uri("https://backend-omega-seven.vercel.app/api/getjoke"));
            if (res.IsSuccessStatusCode) {
                var content = await res.Content.ReadFromJsonAsync<List<DevJoke>>();
                devJoke = content?[0];
            }
            else {
                Console.WriteLine("DevJoke API failed. Using backup.");
                devJoke = await devJokesService.NonAPIDevJoke();
            }
        }
        else {
            devJoke = await devJokesService.NonAPIDevJoke();
        }

        ViewBag.binary = await devJokesService.GetSetDevVM(devJoke.punchline);
        ViewBag.question = devJoke.question;
        ViewBag.punchline = devJoke.punchline;
        ViewBag.aspaction = "DevJoke";

        MemoryStream imageStream = await devJokesService.GenerateDevJokeCard(devJoke);
        ViewBag.JokeCard = imageStream;

        return View();
    }

    [Route("/Geek")]
    public async Task<IActionResult> GeekJoke() {
        GeekJoke geekJoke;
        int rand = new Random().Next(0, 2);
        if (rand == 1) {
            using var client = clientFactory.CreateClient();
            var uri = new Uri("https://geek-jokes.sameerkumar.website/api?format=json");
            var res = await client.GetAsync(uri);
            if (res.IsSuccessStatusCode)
            {
                geekJoke = await res.Content.ReadFromJsonAsync<GeekJoke>();
            }
            else {
                Console.WriteLine("GeekJoke API failed. Using backup.");
                geekJoke = await devJokesService.NonAPIGeekJoke();
            }
        }
        else {
            geekJoke = await devJokesService.NonAPIGeekJoke();
        }

        MemoryStream imageStream = await devJokesService.GenerateGeekJokeCard(geekJoke);
        ViewBag.JokeCard = imageStream;

        return View(geekJoke);
    }

    [Route("/Programming")]
    public async Task<IActionResult> ProgrammingJoke() {
        ProgrammingJoke programmingJoke;
        using var client = clientFactory.CreateClient();
        var res = await client.GetAsync(new Uri("https://official-joke-api.appspot.com/jokes/programming/random#"));
        if (res.IsSuccessStatusCode)
        {
            var content = await res.Content.ReadFromJsonAsync<List<ProgrammingJoke>>();
            programmingJoke = content?[0];
        }
        else {
            Console.WriteLine("ProgrammingJoke API failed. No backup.");
            programmingJoke = new ProgrammingJoke();
        }

        programmingJoke.binary = await devJokesService.GetSetDevVM(programmingJoke.punchline);
        return View(programmingJoke);
    }

    [Route("/NSFWProgramming")]
    public async Task<IActionResult> NSFWProgrammingJoke() {
        NSFWjoke programmingJoke;
        int rand = new Random().Next(0, 6);
        if (rand == 1) {
            programmingJoke = await devJokesService.NonAPINSFWProgrammingJoke();
        }
        else {
            using var client = clientFactory.CreateClient();
            var httpResponse = await client.GetAsync(new Uri("https://v2.jokeapi.dev/joke/Programming"));
            if (httpResponse.IsSuccessStatusCode) {
                var content = await httpResponse.Content.ReadAsStringAsync();
                programmingJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
            }
            else {
                Console.WriteLine("NSFWProgrammingJoke API failed. Using backup.");
                programmingJoke = await devJokesService.NonAPINSFWProgrammingJoke();
            }
        }

        var binIn = programmingJoke?.joke ?? programmingJoke?.delivery;
        programmingJoke.lang = await devJokesService.GetSetDevVM(binIn);
        return View(programmingJoke);
    }

    [Route("/Spooky")]
    public async Task<IActionResult> SpookyJoke() {
        using var client = clientFactory.CreateClient();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Spooky");
        var httpResponse = await client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var spookyJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(spookyJoke);
    }

    //revisit
    [Route("/Pun")]
    public async Task<IActionResult> PunJoke() {
        using var client = clientFactory.CreateClient();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Pun");
        var httpResponse = await client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var punJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(punJoke);
    }

    //revisit
    [Route("/Misc")]
    public async Task<IActionResult> MiscJoke() {
        using var client = clientFactory.CreateClient();
        var uri = new Uri("https://v2.jokeapi.dev/joke/Miscellaneous");
        var httpResponse = await client.GetAsync(uri);
        var content = await httpResponse.Content.ReadAsStringAsync();
        var miscJoke = JsonSerializer.Deserialize<NSFWjoke>(content);
        return View(miscJoke);
    }
    
    public class InputModel
    {
        public string textInput { get; set; }
    }
}