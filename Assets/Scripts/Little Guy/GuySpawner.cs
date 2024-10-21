using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuySpawner : MonoBehaviour
{
    public LittleGuyFactory littleGuyFactory;
    private LittleGuyNav currentLittleGuy;
    public Transform pen;

    private int increment = 0;

    public Button putIn;
    public Button takeOut;

    public bool p;
    public bool t;

    // Start is called before the first frame update
    void Start()
    {
        putIn.onClick.AddListener(PutInPen);
        takeOut.onClick.AddListener(TakeOutPen);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject temp = littleGuyFactory.CreateLittleGuy(new Vector3(0f, 1f, 0f), (CombinationType)increment);
            currentLittleGuy = temp.GetComponent<LittleGuyNav>();
            currentLittleGuy.SetPen(pen);
            if (++increment % System.Enum.GetValues(typeof(CombinationType)).Length == 0)
            {
                increment = 0;
            }

            // nuked for now
        }
    }

    public void PutInPen()
    {
        Debug.Log("putinpen called");
        currentLittleGuy.PutInPen();
    }

    public void TakeOutPen()
    {
        Debug.Log("takeout called");
        Debug.Log(currentLittleGuy.TakeOutPen());
    }

    // Update is called once per frame
    void Update()
    {
        if (p || t)
        {
            if (p)
            {
                PutInPen();
                p = !p;
            }
            if (t)
            {
                TakeOutPen();
                t = !t;
            }
        }
    }
}
