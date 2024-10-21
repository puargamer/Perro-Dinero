using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class craftUI : MonoBehaviour
{
    public GameObject player;
    public RawImage[] craftIngredients;
    public List<Texture2D> items;
    public List<TMP_Text> textCounts;
    public List<int> counts;
    private int currSelected;
    public LittleGuyFactory littleGuyFactory; // shouldnt need to be filled in inspector
    public RecipeBook recipeBook; // shouldnt need to be filled in inspector
    public GameObject deployUI;
    [SerializeField]
    private Transform littleGuySpawnArea;

    public TMP_Text recipeText;
    public GameObject recipePanel;

    private SpriteUtility spriteUtility;
    private DeploymentUI deploymentUI;

    private int totalMaterials = System.Enum.GetValues(typeof(MaterialType)).Length;

    private List<MaterialType> selectedMaterials = new List<MaterialType>(); // store curr materials used

    private bool openPanel = false;

    void Start()
    {
        spriteUtility = FindObjectOfType<SpriteUtility>();
        deploymentUI = FindObjectOfType<DeploymentUI>();
        ResetCraft();
        ScrapeFromInventory();
        DisplayRecipes();
    }

    public void ScrapeFromInventory()
    {
        for (int i = 0; i < counts.Count; i++)
        {
            counts[i] = Singleton.Instance.mats[i];
            textCounts[i].text = counts[i].ToString();   
        }
    }

    public void CloseCraft()
    {
        deployUI.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = true;
        Singleton.Instance.menuInt--;
        if (Singleton.Instance.menuInt == 0 )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        GetComponentInChildren<Canvas>().enabled = false;
        ResetCraft();
    }

    public void ResetCraft()
    {
        for (int i = 0; i < craftIngredients.Length; i++)
        {
            craftIngredients[i].texture = null;
        }
        currSelected = 0;
        selectedMaterials.Clear();
        ScrapeFromInventory();
    }

    public void AddMaterial(MaterialType materialType)
    {
        int index = (int)materialType;

        if (currSelected <= 1 && counts[index] >= 1)
        {
            textCounts[index].text = (--counts[index]).ToString();
            craftIngredients[currSelected].texture = items[index];
            selectedMaterials.Add(materialType);
            currSelected++;
        }
    }
    // LMAO
    public void AddCobweb() => AddMaterial(MaterialType.Cobweb);
    public void AddFeather() => AddMaterial(MaterialType.Feather);
    public void AddFlower() => AddMaterial(MaterialType.Flower);
    public void AddStones() => AddMaterial(MaterialType.Stones);
    public void AddTwig() => AddMaterial(MaterialType.Twig);


    //private MaterialType GetSelectedMaterial(int index) // prob not needed idk what i was doing
    //{ // if not null, return index else return wtf am i looking at
    //    return (MaterialType)(craftIngredients[index].texture != null ? index : -1);
    //}

    public void CraftGuy()
    {
        if (currSelected == 2)
        {
            MaterialType firstMaterial = selectedMaterials[0];
            MaterialType secondMaterial = selectedMaterials[1];

            Singleton.Instance.mats[(int)firstMaterial]--; // bye bye mats
            Singleton.Instance.mats[(int)secondMaterial]--;

            CombinationType combination = recipeBook.UseRecipe(firstMaterial, secondMaterial);

            GameObject createdLittleGuy = littleGuyFactory.CreateLittleGuy(littleGuySpawnArea.position, combination);

            UpdateLittleGuyList(combination, createdLittleGuy);

            ResetCraft();
        }
    }

    public void DisplayRecipes()
    {
        var recipes = recipeBook.GetValidRecipes();
        string recipeList = string.Join("\n", recipes);
        recipeText.text = recipeList;

        if (!openPanel)
        {
            recipeText.text = recipeList;
            recipePanel.SetActive(true);
        }
        else
        {
            recipePanel.SetActive(false);
        }
    }

    //private CombinationType GetCombination(MaterialType first, MaterialType second) // TODO: fix this
    //{
    //    int indexFirst = (int)first;
    //    int indexSecond = (int)second;

    //    int combinationIndex = indexFirst + indexSecond;
    //    int max = System.Enum.GetValues(typeof(CombinationType)).Length - 1;
    //    combinationIndex = (combinationIndex > max) ? max : combinationIndex;

    //    return (CombinationType)combinationIndex;
    //}

    private void UpdateLittleGuyList(CombinationType combination, GameObject littleGuy)
    {
        switch (combination)
        {
            case CombinationType.BaitA:
                Singleton.Instance.BaitALittleGuys.Add(littleGuy);
                break;
            case CombinationType.BaitB:
                Singleton.Instance.BaitBLittleGuys.Add(littleGuy);
                break;
            case CombinationType.BaitC:
                Singleton.Instance.BaitCLittleGuys.Add(littleGuy);
                break;
            case CombinationType.BaitD:
                Singleton.Instance.BaitDLittleGuys.Add(littleGuy);
                break;
            case CombinationType.BaitE:
                Singleton.Instance.BaitELittleGuys.Add(littleGuy);
                break;
            case CombinationType.BaitF:
                Singleton.Instance.BaitFLittleGuys.Add(littleGuy);
                break;
            case CombinationType.BaitG:
                Singleton.Instance.BaitGLittleGuys.Add(littleGuy);
                break;
            default:
                Debug.LogWarning("Unknown combination type: " + combination);
                break;
        }
        deploymentUI.AddToGrid(spriteUtility.GetSprite(combination), littleGuy);
        Singleton.Instance.EquipGuy(littleGuy);
    }
}
