using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TMP_Text text;
    public string[] lines;
    public float textSpeed;
    private int index;

    public DialogueSounds dialogueSounds;   //sound

    [Header("squish character")]
    public Transform character;
    float originalyScale;
    float originalyPos;

    public float scaleDiff;
    public float posDiff;
    private bool flip = false;

    // Start is called before the first frame update
    void Start()
    {
        //squish
        originalyScale = character.transform.localScale.y;
        originalyPos = character.transform.position.y;

        text.text = "";
        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (text.text == lines[index])
        {
            nextLine();
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char  c in lines[index].ToCharArray()) 
        {
            text.text += c;
            dialogueSounds.PlayCharSound(c);    //play sound

            //squish
            float tempYScale = 0;
            float tempYPos = 0;

            if (flip)
            {
                tempYScale = character.transform.localScale.y - scaleDiff;
                tempYPos = character.transform.position.y - posDiff;
            }
            else
            {
                tempYScale = character.transform.localScale.y + scaleDiff;
                tempYPos = character.transform.position.y + posDiff;
            }
            flip = !flip;

            character.transform.localScale = new Vector3(character.transform.localScale.x, tempYScale, character.transform.localScale.z);
            character.transform.position = new Vector3(character.transform.position.x, tempYPos, character.transform.position.z);


            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator EndDialogue()
    {
        //reset squish
        character.transform.localScale = new Vector3(character.transform.localScale.x, originalyScale, character.transform.localScale.z);
        character.transform.position = new Vector3(character.transform.position.x, originalyPos, character.transform.position.z);

        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }

    void nextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            //text.text = "";
            //StartCoroutine (TypeLine());
            StartCoroutine(NextLineWait());
        }
        else
        {
            StartCoroutine(EndDialogue());
        }
    }

    IEnumerator NextLineWait()
    {
        yield return new WaitForSeconds(2);
        text.text = "";
        StartCoroutine(TypeLine());
    }
}
