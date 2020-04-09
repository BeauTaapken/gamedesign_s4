using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SpawnInfoJson
{
    [SerializeField]
    public List<KeyValuePair<int, MonsterInfoJson>> spawninfoJsonList = new List<KeyValuePair<int, MonsterInfoJson>>();

    private string path;

    //public MonsterInfoJson MonsterInfoJson = new MonsterInfoJson();

    //public int monsterAmount;
    //public int bossAmount;

    public void AddToList(int level, MonsterInfoJson monsterInfo)
    {
        spawninfoJsonList.Add(new KeyValuePair<int, MonsterInfoJson>(level, monsterInfo));
    }

    public void SaveAsJson()
    {
        SaveJSON(spawninfoJsonList);
        //return JsonUtility.ToJson(this);
    }

    public void SetPath(string path)
    {
        this.path = path;
    }

    private void SaveJSON(List<KeyValuePair<int, MonsterInfoJson>> list)
    {
        string json = JsonUtility.ToJson(list);
        Debug.Log(json);
        string filename = Path.Combine(Application.persistentDataPath, path);
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
        File.WriteAllText(filename, json);
    }
}
