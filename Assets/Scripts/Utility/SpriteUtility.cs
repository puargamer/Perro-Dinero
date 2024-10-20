using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUtility : MonoBehaviour
{
    private Dictionary<MaterialType, Sprite> spriteDictionary;
    private Dictionary<CombinationType, Sprite> playerSpriteDictionary;

    [System.Serializable] // why did i need this
    public class MaterialSprite
    {
        public MaterialType type;
        public Sprite sprite;
    }

    [System.Serializable] 
    public class LittleGuySprite
    {
        public CombinationType type;
        public Sprite sprite;
    }

    public List<MaterialSprite> materialSprites;
    public List<LittleGuySprite> littleGuySprites;

    private void Awake()
    {
        spriteDictionary = new Dictionary<MaterialType, Sprite>();
        playerSpriteDictionary = new Dictionary<CombinationType, Sprite>();

        foreach (var materialSprite in materialSprites)
        {
            spriteDictionary[materialSprite.type] = materialSprite.sprite;
        }

        foreach (var lilSprite in littleGuySprites)
        {
            playerSpriteDictionary[lilSprite.type] = lilSprite.sprite;
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

    public Sprite GetSprite(CombinationType type)
    {
        if (playerSpriteDictionary.TryGetValue(type, out Sprite sprite))
        {
            return sprite;
        }
        return null;
    }
}
