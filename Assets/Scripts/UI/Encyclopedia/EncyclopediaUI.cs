using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class EncyclopediaItem
{
    public string name;
    public string description;
    public Sprite icon;
}
// index based, first approach i took with each holding the other class has a circular dependency
[System.Serializable]
public class CatchableFish : EncyclopediaItem
{
    public float catchChance;
    public int[] capableLures; // indices or element nums of lure
}

[System.Serializable]
public class Lure : EncyclopediaItem
{
    public int[] catchableFishIndices; // indices or element nums of fish
}

public class EncyclopediaUI : MonoBehaviour
{

    public GameObject encyclopediaUI;

    public Button lureTabButton;
    public Button fishTabButton;

    public GameObject buttonPrefab; // Button prefab for scroll
    [Header("UI Elements")]
    public GameObject basePanel;
    public Transform buttonScrollview;

    public GameObject details;
    public Image itemIconImage;
    public TMP_Text itemNameText;
    public TMP_Text itemDescText;
    //public TMP_Text fishCatchRate; // this will be displayed within the bottom half of images instead

    // probably going to go unused?
    //[Header("Fish UI Elements")]
    //public GameObject fishPanel;
    //public Transform fishButtonScroll;
    //public Image lureIcon;
    //public TMP_Text fishName;

    [Header("All Information")]
    public Lure[] lures;
    public CatchableFish[] catchableFishes;

    void Start()
    {
        lureTabButton.onClick.AddListener(ShowLures);
        fishTabButton.onClick.AddListener(ShowFish);

        // Default show the lures tab when the game starts
        ShowLures();
    }

    void CreateButtons(bool forLure)
    {
        if (forLure)
        {
            // create lure buttons
            foreach (Lure lure in lures)
            {
                //Button newButton = Instantiate(buttonPrefab, buttonScrollview).GetComponent<Button>();
                GameObject newButton = Instantiate(buttonPrefab, buttonScrollview.transform); // Vector3.zero, Quaternion.identity,
                Image buttonImage = newButton.GetComponentInChildren<Image>();

                buttonImage.sprite = lure.icon;
                buttonImage.preserveAspect = true;

                newButton.GetComponent<Button>().onClick.AddListener(() => DisplayInfo(lure, lure.catchableFishIndices));
            }
        }
        else
        {
            // create fish buttons
            foreach (CatchableFish fish in catchableFishes)
            {
                //Button newButton = Instantiate(buttonPrefab, buttonScrollview).GetComponent<Button>();
                //newButton.GetComponentInChildren<Text>().text = fish.name;
                GameObject newButton = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, buttonScrollview.transform);
                Image buttonImage = newButton.GetComponentInChildren<Image>();
                buttonImage.sprite = fish.icon;

                newButton.GetComponent<Button>().onClick.AddListener(() => DisplayInfo(fish, fish.capableLures));

                //newButton.onClick.AddListener(() => DisplayInfo(fish, fish.capableLures));
            }
        }
    }

    // show/hide panels
    void ShowLures()
    {
        ClearInfo();
        CreateButtons(true);
    }

    void ShowFish()
    {
        ClearInfo();
        CreateButtons(false);
    }

    // Display information for a selected EncyclopediaItem (can be either a Lure or CatchableFish)
    void DisplayInfo(EncyclopediaItem item, int[] relatedIndices)
    {
        // hide the buttons and also the scroll views
        // display base info

        itemNameText.text = item.name;
        itemDescText.text = item.description;

        itemIconImage.gameObject.SetActive(true);
        itemIconImage.sprite = item.icon;

        // display related items underneath, probably in another scrollview
        foreach (int index in relatedIndices)
        {
            if (item is Lure)
            {
                CatchableFish fish = catchableFishes[index];
                // Display the fish information underneath (icon and catch rates for this lure)
            }
            else if (item is CatchableFish)
            {
                Lure lure = lures[index];
                // Display the lure information underneath (icon)
            }
            else
            {
                Debug.Log("EncyclopediaUI.cs displayInfo error what did you do");
            }
        }
    }

    #region wack old display test code
    //void DisplayLureInfo(int lureIndex)
    //{
    //    // display by index
    //    Lure lure = lures[lureIndex];
    //    // display catchable fish using this lure
    //    foreach (int fishIndex in lure.catchableFishIndices)
    //    {
    //        CatchableFish fish = catchableFishes[fishIndex];
    //    }

    //void DisplayFishInfo(int fishIndex)
    //{
    //    // display by index
    //    CatchableFish fish = catchableFishes[fishIndex];
    //        // display lures that can catch this fish
    //    foreach (int lureIndex in fish.capableLures)
    //    {
    //        Lure lure = lures[lureIndex];
    //    }
    #endregion 

    void ClearInfo()
    {
        // reset all info ui elements
        foreach (Transform child in buttonScrollview)
        {
            Destroy(child.gameObject);
        }
        itemNameText.text = "";
        itemDescText.text = "";
        itemIconImage.gameObject.SetActive(false);
    }
    #region UI open close func
    void OpenEncyclopediaUI()
    {
        UIUtility.ToggleMenu(encyclopediaUI, true);
    }

    void CloseEncyclopediaUI()
    {
        UIUtility.ToggleMenu(encyclopediaUI, false);
    }
    #endregion
}
