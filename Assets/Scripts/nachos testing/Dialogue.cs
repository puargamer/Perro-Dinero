using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject textbox;
    public GameObject cursor;
    public TMP_Text text;
    public string[] lines;
    public float textSpeed;
    private int index;

    private Coroutine _TypeLine;    //reference to text animation coroutine, used to skip animation

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

        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        //continue to next line
        if (text.text == lines[index])
        {
            nextLine();
        }

        //skip text animation
        else if (text.text != lines[index])
        {       //currently is getting called even when no dialogue is happening
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StopCoroutine(_TypeLine);
                text.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        text.text = "";
        StopAllCoroutines();
        textbox.SetActive(true);
        index = 0;
        _TypeLine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        cursor.SetActive(false);

        foreach (char c in lines[index].ToCharArray()) 
        {

            //normal text progression
            text.text += c;
            dialogueSounds.PlayCharSound(c);    //play sound

            squishAnimation();

            yield return new WaitForSeconds(textSpeed);
        }
    }

    void squishAnimation()
    {
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
    }

    void EndDialogue()
    {
        //reset squish
        character.transform.localScale = new Vector3(character.transform.localScale.x, originalyScale, character.transform.localScale.z);
        character.transform.position = new Vector3(character.transform.position.x, originalyPos, character.transform.position.z);

        textbox.SetActive(false);
    }

    void nextLine()
    {
        cursor.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            //display next dialogue line
            if (index < lines.Length - 1)
            {
                index++;

                text.text = "";
                _TypeLine = StartCoroutine(TypeLine());
            }

            //if no more dialogue, end
            else {EndDialogue();}
        }
    }
}
