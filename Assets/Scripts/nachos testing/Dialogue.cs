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

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (text.text == lines[index])
        {
            nextLine();
        }
    }

    void StartDialogue()
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
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator EndDialogue()
    {
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
