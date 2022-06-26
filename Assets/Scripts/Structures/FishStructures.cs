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
            { FishNames.Anguille, new Fish("Anguille", "gros", "écaillé", "36kg") },
            { FishNames.Baleine, new Fish("Baleine à corne", "15m", "Grise et taches blanches", "40T") },
            { FishNames.Seiche, new Fish("Seiche Banane", "50cm", "Jaune et blanche", "2Kg") },
            { FishNames.Baudroie, new Fish("Baudroie", "20cm", "Brune", "28Kg") },
            { FishNames.Calarmure, new Fish("Calarmure", "8m", "Rouge", "550Kg") },
            { FishNames.MilePalme, new Fish("Mile-Palme", "11m", "Rouge et Jaune", "740Kg") },
            { FishNames.Lancelet, new Fish("Lancelet Comète", "7m", "Bleu", "520Kg") },
            { FishNames.DosBleu, new Fish("Dos Bleu", "20cm", "argenté à bandes bleues", "50g") },
            { FishNames.Guirlande, new Fish("Guirlande", "9m", "Brune et translucide", "550Kg") },
            { FishNames.Hippocorne, new Fish("Hippocorne", "12cm", "Orange", "15g") },
            { FishNames.LimaceBoa, new Fish("Limace Boa", "1m", "Bleu tacheté de jaune", "5Kg") },
            { FishNames.Bathypugille, new Fish("Bathypugille", "10cm", "Vert et rouge", "400g") },
            { FishNames.MeduseAlgue, new Fish("Meduse Algue", "1m", "Translucide", "2Kg") },
            { FishNames.MeduseAurelia, new Fish("Meduse Aurelia", "25cm", "Translucide", "800g") },
            { FishNames.MedusePic, new Fish("Meduse à pic", "40cm", "Translucide et rouge", "2Kg") },
            { FishNames.PoissonBulle, new Fish("Poisson Bulle", "20cm", "Translucide et rose", "1,5Kg") },
            { FishNames.PoissonChirurgien, new Fish("Poisson Chirurgien", "15cm", "Bleu", "90g") },
            { FishNames.PoissonClown, new Fish("Poisson Clown", "12cm", "Orange à bandes blanches", "50g") },
            { FishNames.PoissonPapillon, new Fish("Poisson Papillon", "17cm", "Jaune à bandes bleues", "150g") },
            { FishNames.GrammaLoretto, new Fish("Gramma Lorette", "8cm", "Jaune et violet", "20g") },
            { FishNames.Rascasse, new Fish("Rascasse Volante", "40cm", "Brune à bandes blanches", "2,5Kg") },
            { FishNames.RequinRenard, new Fish("Requin Renard", "5m", "Grise", "500Kg") },
            { FishNames.RequinThermal, new Fish("Requin Thermal", "6m", "Grise", "1T") },
            { FishNames.Sardine, new Fish("Sardine", "20cm", "argenté", "30g") },
            { FishNames.Charybarvus, new Fish("Charybarvus", "17m", "violette", "1T") },
            { FishNames.Surprise, new Fish("???", "???", "???", "???") },
        };
    }
}

public enum FishNames
{
    Anguille,
    Baleine,
    Seiche,
    Baudroie,
    Calarmure,
    MilePalme,
    Lancelet,
    DosBleu,
    Guirlande,
    Hippocorne,
    LimaceBoa,
    Bathypugille,
    MeduseAlgue,
    MeduseAurelia,
    MedusePic,
    PoissonBulle,
    PoissonChirurgien,
    PoissonClown,
    PoissonPapillon,
    GrammaLoretto,
    Rascasse,
    RequinRenard,
    RequinThermal,
    Sardine,
    Charybarvus,
    Surprise

}

public enum FeedingRegime
{
    Inconnu,
    Carnivore,
    Herbivore,
    Omnivore
}