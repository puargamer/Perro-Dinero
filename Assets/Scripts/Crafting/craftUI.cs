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
    private List<int> scrapedCounts;
    private List<int> counts;
    private int currSelected;
    public SpriteUtility spriteManager;
    public LittleGuyFactory littleGuyFactory; // shouldnt need to be filled in inspector
    public GameObject deployUI;
    [SerializeField]
    private Transform littleGuySpawnArea;

    public Transform inventoryGridView;
    public GameObject inventoryButtonPrefab;

    public Transform recipeGridView;
    public GameObject recipeButtonPrefab;

    public TMP_Text recipeText;
    public GameObject recipePanel;
    public TMP_Text recipeButtonText;

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
        SetRecipes();
        DisplayRecipes();
        CreateRecipeButtons();
    }

    public void ScrapeFromInventory()
    {
        //scrapedCounts = new List<int>( new int[MaterialTypeHelper.Count] );
        counts = new List<int>(new int[MaterialTypeHelper.Count]);
        for (int i = 0; i < MaterialTypeHelper.Count; i++) // if we plan on adding more mats
        {
            counts[i] = Singleton.Instance.mats[i];
            //scrapedCounts[i] = Singleton.Instance.mats[i];
            textCounts[i].text = counts[i].ToString();

            //CreateInventoryEntry(i);
        }
    }

    //void CreateInventoryEntry(int index) // dynamically adds the buttons (somehow doesnt work!!!!)
    //{
    //    GameObject newButton = Instantiate(inventoryButtonPrefab, inventoryGridView);
    //    Image buttonImage = newButton.GetComponentInChildren<Image>();

    //    buttonImage.sprite = spriteUtility.GetSprite((MaterialType)index);
    //    buttonImage.preserveAspect = true;

    //    newButton.GetComponent<Button>().onClick.AddListener(() => InventoryInteract(index));
    //}
    //public void InventoryInteract(int index)
    //{
    //    MaterialType material = (MaterialType)index;

    //    if (currSelected <= 1 && counts[index] >= 1)
    //    {
    //        textCounts[index].text = (--counts[index]).ToString();
    //        craftIngredients[currSelected].texture = spriteUtility.GetSprite(material).texture; // modified line, can remove items now
    //        craftIngredients[currSelected].gameObject.SetActive(true);
    //        selectedMaterials.Add(material);
    //        currSelected++;
    //    }
    //}

    public void CloseCraft()
    {
        deployUI.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = true;
        Singleton.Instance.isMenuOpened = false;
        if (!Singleton.Instance.isMenuOpened)
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
            craftIngredients[i].gameObject.SetActive(false);
        }
        currSelected = 0;
        selectedMaterials.Clear();

        //counts = scrapedCounts;
        ScrapeFromInventory();
        //for (int i = 0; i < MaterialTypeHelper.Count; i++) // reset text
        //{
        //    textCounts[i].text = counts[i].ToString();
        //}
    }
    void CreateRecipeButtons()
    {
        List<MaterialRecipe> recipes = RecipeBook.GetAllRecipes();
        for (int i = 0; i < recipes.Count; i++)
        {
            //Debug.Log(recipes[i].FishColor);
            int index = i;
            GameObject newButton = Instantiate(recipeButtonPrefab, recipeGridView);
            newButton.GetComponentInChildren<TMP_Text>().text = $"{recipes[i].FishColor}";
            Image buttonImage = newButton.GetComponentInChildren<Image>();
            
            buttonImage.sprite = spriteManager.GetSprite(recipes[i].Result);
            newButton.GetComponent<Button>().onClick.AddListener(() => AddLittleGuyRecipe(index));
        }
    }
    public void AddLittleGuyRecipe(int index)
    {
        Debug.Log($"index is {index}");
        ResetCraft();
        MaterialRecipe currRecipe = RecipeBook.GetAllRecipes()[index];
        AddMaterial(currRecipe.MaterialOne);
        AddMaterial(currRecipe.MaterialTwo);
    }

    public void AddMaterial(MaterialType materialType)
    {
        int index = (int)materialType;
        //Debug.Log(counts[index]);

        if (currSelected <= 1 && counts[index] >= 1)
        {
            textCounts[index].text = (--counts[index]).ToString();
            craftIngredients[currSelected].texture = spriteUtility.GetSprite(materialType).texture; // modified line, can remove items now
            craftIngredients[currSelected].gameObject.SetActive(true);
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

            CombinationType combination = RecipeBook.UseRecipe(firstMaterial, secondMaterial);

            GameObject createdLittleGuy = LittleGuyFactory.Instance.CreateLittleGuy(littleGuySpawnArea.position, combination);

            UpdateLittleGuyList(combination, createdLittleGuy);

            ResetCraft();
        }
    }

    #region deprecated
    void SetRecipes()
    {
        var recipes = RecipeBook.GetFormattedValidRecipes();
        string recipeList = string.Join("\n", recipes);
        recipeText.text = recipeList;
    }

    public void DisplayRecipes()
    {
        openPanel = !openPanel;

        if (!openPanel)
        {
            recipePanel.SetActive(true);
            recipeButtonText.text = "Close Recipes";
        }
        else
        {
            recipePanel.SetActive(false);
            recipeButtonText.text = "Open Recipes";
        }
    }
    #endregion

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
