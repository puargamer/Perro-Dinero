using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishStand : MonoBehaviour
{
    public TMP_Text text;
    public int score;
    public FishStandHitbox hitbox;
    public Dialogue dialogue;
    public AudioSource audioSource;
    public OrderSign orderSign;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        text.text = "Score: " + score;
        
    }
}
