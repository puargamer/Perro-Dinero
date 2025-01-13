using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Visual representation of a Quest; contains Quest data and fields to visually represent data
public class QuestVisual : MonoBehaviour
{
    //Data
    public Quest quest;

    [Header("Visuals")]
    new public TMP_Text name;
    public TMP_Text combinationType;
    public TMP_Text reward;
    public TMP_Text complete;
    public TMP_Text claimed;

    public Image fishImage;

    public GameObject claimButton;
    public GameObject itemToRemove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (quest.complete)
        {
            claimButton.SetActive(true);
            claimed.text = "CLAIMED";
        }
        else 
        { 
            claimButton.SetActive(false);
        }
    }

    public void UpdateVisual(Quest _quest)
    {
        quest = _quest;

        //name.text = quest.name;
        combinationType.text = quest.combinationType.ToString();
        reward.text = "Reward: $" + quest.reward.ToString();
        fishImage.sprite = SpriteUtility.Instance.GetSprite(quest.combinationType);
        claimed.text = "CLAIM";
        //complete.text = "Complete: " + quest.complete.ToString();
        //claimed.text = "Claimed: " + quest.claimed.ToString();
    }

    //claim the quest reward and mark it as claimed
    public void ClaimButton()
    {
        if (quest.complete && !quest.claimed)
        {
            quest.claimed = true;
            UpdateVisual(quest);
            GameObject.Find("Player").GetComponent<PlayerInventory>().ChangeMoney(quest.reward);

            for (int j = 0; j < GameObject.Find("Player").GetComponent<PlayerInventory>().InventoryArray.Length; j++)
            {
                if (GameObject.Find("Player").GetComponent<PlayerInventory>().InventoryArray[j] is Fish_ItemData cuh)
                {
                    if (cuh.combinationType == quest.combinationType)
                    {
                        GameObject.Find("Player").GetComponent<PlayerInventory>().RemoveItem(GameObject.Find("Player").GetComponent<PlayerInventory>().InventoryArray[j]);
                        break;
                    }
                }
            }

            
        }
    }
}
