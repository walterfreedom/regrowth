using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System;

public class savesystem: MonoBehaviour
{
    public Dictionary<string, GameObject> saveables = new Dictionary<string, GameObject>();
    //public List<GameObject> saveables=new List<GameObject>();
    private List<SaveData> savedata = new List<SaveData>();
    public Dictionary<string,GameObject> prefabs;
    string path;
    private void Start()
    {
        path = Application.persistentDataPath + "/save.json";
    }
    [ContextMenu("Save")]
    public void SaveGameObjects()
    {
        string content = "";
        if (savedata.Count > 0)
            savedata.Clear();
        foreach (var item in saveables)
        {   
            savedata.Add(item.Value.GetComponent<Stats>().createSaveData());
        }
       
        content = content+JsonHelper.ToJson<SaveData>(savedata.ToArray());
       
        print(content);
        writeFile(content);
    }

    [ContextMenu("Load")]
   public void loadGameObjects()
    {
        string objects = readFile();
        
        List<SaveData> savedatas = JsonHelper.FromJson<SaveData>(objects).ToList<SaveData>();
        foreach(var data in savedatas)
        {
            if (saveables.ContainsKey(data.id))
            {
                saveables[data.id].GetComponent<Stats>().loadData(data); ;
            }
            else
            {
                Vector3 spawnloc = new Vector3(data.position[0], data.position[1], data.position[2]);
                var a = Instantiate(prefabs[data.prefabname], spawnloc, Quaternion.identity);
            }
        }
    }

   private void writeFile(string content)
    {
        FileStream file = new FileStream(path, FileMode.Create);
        if (File.Exists(path))
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(content);
            }
        }

    }
    
    private string readFile()
    {
        FileStream file = new FileStream(path, FileMode.Open);

        using (StreamReader reader = new StreamReader(file))
        {
            string savefile = reader.ReadToEnd();
            return savefile;
        }
    }

}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

