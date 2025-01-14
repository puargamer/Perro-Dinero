using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewDialogue : MonoBehaviour
{
    public GameObject canvas;

    public TMP_Text text;
    public string[] lines;
    public float textSpeed;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        //StartDialogue(lines);
    }

    private void OnEnable()
    {
        EventManager.DialogueEvent += StartDialogue;
        EventManager.EndDialogueEvent += EndDialogue;
    }

    private void OnDisable()
    {
        EventManager.DialogueEvent -= StartDialogue;
        EventManager.EndDialogueEvent -= EndDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(text.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
    }

    void StartDialogue(string[] stringArray)
    {
        canvas.SetActive(true);
        lines = stringArray;

        text.text = "";
        index = 0;
        StartCoroutine(TypeLine());
    }

    //force ends dialogue
    void EndDialogue()
    {
        canvas.SetActive(false);
        lines = null;
        text.text = "";
        index = 0;
        StopAllCoroutines();
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            canvas.SetActive(false);
        }
    }
}
