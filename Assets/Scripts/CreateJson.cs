using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OVRSimpleJSON;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class CreateJson
{
    static CreateJson()
    {
        Debug.Log("now creating json");
        //List<KeyValuePair<int, string>> spawninfoJsonList = new List<KeyValuePair<int, string>>();
        //Dictionary<int, string> spawninfoJsonList = new Dictionary<int, string>();

        //JSONObject jsonObject = new JSONObject();

        string path = Application.persistentDataPath + "levels.json";

        SpawnInfoJson spawnInfoJson = new SpawnInfoJson();
        
        spawnInfoJson.SetPath(path);

        for (int i = 1; i <= 20; i++)
        {
            
            MonsterInfoJson monsterInfoJson = new MonsterInfoJson();
            //spawnInfoJson.level = i;
            switch (i)
            {
                case 5:
                    //spawnInfoJson.MonsterInfoJson.bossAmount = 0;
                    monsterInfoJson.bossAmount = 0;
                    break;
                default:
                    monsterInfoJson.bossAmount = 0;
                    //spawnInfoJson.MonsterInfoJson.bossAmount = 0;
                    break;
            }
            //spawnInfoJson.MonsterInfoJson.bossAmount = 0;
            //spawnInfoJson.MonsterInfoJson.monsterAmount = i * 3;
            monsterInfoJson.monsterAmount = i * 9;

            spawnInfoJson.AddToList(i, monsterInfoJson);

            //spawnInfoJson.spawninfoJsonList.Add(new KeyValuePair<int, string>(i, ));

            Debug.Log(JsonUtility.ToJson(spawnInfoJson));
            //jsonObject.Add("level" + i, spawnInfoJson.SaveAsJson());
            //spawninfoJsonList.Add(new KeyValuePair<int, string>(i, spawnInfoJson.SaveAsJson()));
            //spawninfoJsonList.Add(i, spawnInfoJson.SaveAsJson());
        }

        

        spawnInfoJson.SaveAsJson();



        //string json = JsonUtility.ToJson(spawninfoJsonList);

        //Debug.Log(json);

        //Debug.Log(jsonObject);

        //File.WriteAllText(path, jsonObject.ToString());

        SpawnInfoJson test = JsonUtility.FromJson<SpawnInfoJson>(File.ReadAllText(path));

        //Debug.Log(test);

        //SpawnInfoJson test = JsonUtility.FromJson<SpawnInfoJson>(path);
    }
}
