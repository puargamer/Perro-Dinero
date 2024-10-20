using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUtility : MonoBehaviour
{
    private Dictionary<MaterialType, Sprite> spriteDictionary;

    [System.Serializable] // why did i need this
    public class MaterialSprite
    {
        public MaterialType type;
        public Sprite sprite;
    }

    public List<MaterialSprite> materialSprites;

    private void Awake()
    {
        spriteDictionary = new Dictionary<MaterialType, Sprite>();

        foreach (var materialSprite in materialSprites)
        {
            spriteDictionary[materialSprite.type] = materialSprite.sprite;
        }
    }

    public Sprite GetSprite(MaterialType type)
    {
        if (spriteDictionary.TryGetValue(type, out Sprite sprite))
        {
            return sprite;
        }
        return null; // where did it go???
    }
}
