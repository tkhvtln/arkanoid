using UnityEngine;

public class SaveController : MonoBehaviour
{
    public Data data;

    public void Save()
    {
        string jsonDataString = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(Constants.DATA, jsonDataString);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(Constants.DATA))
        {
            string loadedString = PlayerPrefs.GetString(Constants.DATA);
            data = JsonUtility.FromJson<Data>(loadedString);
        }
        else
        {
            data = new Data();
        }
    }
}

public class Data
{
    public bool isSound;
    public int level;

    public Data()
    {
        isSound = true;
        level = 0;
    }
}
