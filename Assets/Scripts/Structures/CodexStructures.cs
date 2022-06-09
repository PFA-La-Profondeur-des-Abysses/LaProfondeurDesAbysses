using UnityEngine;

public class Page
{
    public string name;
    public FeedingRegime feedingRegime;
    public string description;
    public Sprite picture;

    public Page(string name, FeedingRegime feedingRegime, string description, Sprite picture)
    {
        this.name = name;
        this.feedingRegime = feedingRegime;
        this.description = description;
        this.picture = picture;
    }
}