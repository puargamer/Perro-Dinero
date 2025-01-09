using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public SaveDataModel loadedData;    //reference to object that stores the save data loaded in Load()

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        //process data
        SaveDataModel model = new SaveDataModel();
        model.name = "test name";
        model.currentWeekday = GameObject.Find("DayManager").GetComponent<DayManager>().currentWeekday;

        //save data
        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
        Debug.Log("Saved Data in " + Application.persistentDataPath);

    }

    public void Load()
    {
        //load data
        SaveDataModel model = JsonUtility.FromJson<SaveDataModel>(File.ReadAllText(Application.persistentDataPath + "/save.json"));
        Debug.Log("Loaded Data");
        loadedData = model;

        //process data
        GameObject.Find("DayManager").GetComponent<DayManager>().currentWeekday = model.currentWeekday;
    }
}

//Save Data Format
//(all savable data goes here)
[System.Serializable]
public class SaveDataModel
{
    public string name;
    public DayManager.weekday currentWeekday;
}
