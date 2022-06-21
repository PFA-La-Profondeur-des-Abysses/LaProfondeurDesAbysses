using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fish
{
    public string name;
    public bool discovered;
    public bool pictureTaken;
    public bool fullyAnalyzed;
    public string size;
    public string robe;
    public string weight;
    
    private static Dictionary<FishNames, Fish> fishToInfo = new();

    public Fish(string name, string size = "", string robe = "", string weight = "", bool discovered = false, bool pictureTaken = false, bool fullyAnalyzed = false)
    {
        this.name = name;
        this.size = size;
        this.robe = robe;
        this.weight = weight;
        this.discovered = discovered;
        this.pictureTaken = pictureTaken;
        this.fullyAnalyzed = fullyAnalyzed;
    }

    public static Fish GetFishFromName(FishNames name)
    {
        if(fishToInfo.Count == 0) InitialiseDictionary();
        
        return fishToInfo[name];
    }

    public static FishNames GetFishEnumFromFish(Fish fish)
    {
        if(fishToInfo.Count == 0) InitialiseDictionary();
        
        return fishToInfo.FirstOrDefault(x => x.Value == fish).Key;
    }

    public static void InitialiseDictionary()
    {
        fishToInfo = new Dictionary<FishNames, Fish>
        {
            { FishNames.DosBleu, new Fish("DosBleu", "10cm", "bleu", "50kg") },
            { FishNames.Sardine, new Fish("Sardine", "petit", "gris", "10.5g") },
            { FishNames.Poisson, new Fish("Poisson", "gros", "écaillé", "36kg") }
        };
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