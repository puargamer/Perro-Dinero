using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class FishFinal : Item
{
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer.sprite = base.itemData.icon;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Use()
    {
        
    }
}
