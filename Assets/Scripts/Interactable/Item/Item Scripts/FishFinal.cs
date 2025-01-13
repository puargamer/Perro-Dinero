using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFinal : Item
{
    public SpriteRenderer spriteRenderer;

    private void OnValidate()
    {
        if (!Application.isPlaying) { spriteRenderer.sprite = base.itemData.icon; }
    }

    // Start is called before the first frame update
    void Start()
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
