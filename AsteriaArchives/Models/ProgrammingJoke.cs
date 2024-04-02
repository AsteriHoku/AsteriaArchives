namespace DevJokes.Models;

public class ProgrammingJoke
{
    public string setup { get; set; }
    public string punchline { get; set; }
    public string binary { get; set; }

    public ProgrammingJoke()
    {
        setup = "Programming Joke API Failed.";
        punchline = "Sorry.";
    }
}