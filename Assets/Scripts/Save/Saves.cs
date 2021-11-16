using UnityEngine;

class Saves
{
    public const string SaveName = "Save";

    public Save Load()
    {
        var json = PlayerPrefs.GetString(SaveName);
        var save = JsonUtility.FromJson<Save>(json);

        return save ?? new Save();
    }
    public void Save(Save save)
    {
        var json = JsonUtility.ToJson(save);

        PlayerPrefs.SetString(SaveName, json);
    }
}