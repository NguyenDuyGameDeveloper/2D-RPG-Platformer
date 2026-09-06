using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int golds;
    public int skillPoints;

    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, int> inventory;
    public SerializableDictionary<string, int> material;
    public List<string> equipmentId;

    public SerializableDictionary<string, bool> checkPoints;
    public string closestCheckPointId;

    public SerializableDictionary<string, float> volumeSettings;
    public GameData()
    {
        this.golds = 0;
        this.skillPoints = 0;
        this.skillTree = new SerializableDictionary<string, bool>();
        this.inventory = new SerializableDictionary<string, int>();
        this.material = new SerializableDictionary<string, int>();
        this.equipmentId = new List<string>();

        this.checkPoints = new SerializableDictionary<string, bool>();
        this.closestCheckPointId = string.Empty;

        this.volumeSettings = new SerializableDictionary<string, float>();
    }
}
