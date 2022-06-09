public class Fish
{
    public string name;
    public bool discovered;
    public bool pictureTaken;
    public bool fullyAnalyzed;

    public Fish(string name, bool discovered, bool pictureTaken, bool fullyAnalyzed)
    {
        this.name = name;
        this.discovered = discovered;
        this.pictureTaken = pictureTaken;
        this.fullyAnalyzed = fullyAnalyzed;
    }
}

public enum FishNames
{
    DosBleu,
    Sardine,
    Poisson,
    Requin
}

public enum FeedingRegime
{
    Inconnu,
    Carnivore,
    Herbivore,
    Omnivore
}