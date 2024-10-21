using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishStand : MonoBehaviour
{
    public TMP_Text text;
    public int score;
    public FishStandHitbox hitbox;
    public GameObject objectInHitbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (hitbox.HitboxActive)
        {
            score++;

            Destroy(objectInHitbox);

        }

        text.text = "Score: " + score;
    }
}
