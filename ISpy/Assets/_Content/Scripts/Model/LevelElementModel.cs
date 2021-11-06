[System.Serializable]
public class LevelElementModel
{
    public string levelLabel;
    public bool locked;
    public string levelImageURL;

    public LevelElementModel(string levelLabel, bool locked, string levelImageURL)
    {
        this.levelLabel = levelLabel;
        this.locked = locked;
        this.levelImageURL = levelImageURL;
    }
}
[System.Serializable]
public class LevelElementsData
{
    public LevelElementModel[] levels;
}
