using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public SaveDataModel loadedData;    //reference to object that stores the save data loaded in Load()

    private void OnEnable()
    {
        EventManager.SaveEvent += Save;
        EventManager.LoadEvent += Load;
    }
    private void OnDisable()
    {
        EventManager.SaveEvent -= Save;
        EventManager.LoadEvent -= Load;
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
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
        model.dayFinished = GameObject.Find("DayManager").GetComponent<DayManager>().dayFinished;
        model.money = GameObject.Find("Player").GetComponent<PlayerInventory>().money;
        model.InventoryArray = GameObject.Find("Player").GetComponent<PlayerInventory>().InventoryArray;

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
        GameObject.Find("DayManager").GetComponent<DayManager>().dayFinished = model.dayFinished;
        GameObject.Find("Player").GetComponent<PlayerInventory>().money = model.money;
        GameObject.Find("Player").GetComponent<PlayerInventory>().InventoryArray = model.InventoryArray;
    }

    public void Reset()
    {
        SaveDataModel model = new SaveDataModel();

        //save data
        string json = JsonUtility.ToJson(model);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
        Debug.Log("Saved Data in " + Application.persistentDataPath);
    }
}

//Save Data Format
//(all savable data goes here)
[System.Serializable]
public class SaveDataModel
{
    public string name;
    public DayManager.weekday currentWeekday;
    public bool dayFinished;
    public int money;
    public ItemData[] InventoryArray;
}
