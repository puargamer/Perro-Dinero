using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LittleGuysMenu : MonoBehaviour
{
    public TMP_Text LittleGuyAText;
    public TMP_Text LittleGuyBText;
    public TMP_Text LittleGuyCText;
    public TMP_Text LittleGuyDText;
    public TMP_Text LittleGuyEText;
    public TMP_Text LittleGuyFText;
    public TMP_Text LittleGuyGText;


    void Update()
    {
        LittleGuyAText.text = "" + Singleton.Instance.BaitALittleGuys.Count;
        LittleGuyBText.text = "" + Singleton.Instance.BaitBLittleGuys.Count;
        LittleGuyCText.text = "" + Singleton.Instance.BaitCLittleGuys.Count;
        LittleGuyDText.text = "" + Singleton.Instance.BaitDLittleGuys.Count;
        LittleGuyEText.text = "" + Singleton.Instance.BaitELittleGuys.Count;
        LittleGuyFText.text = "" + Singleton.Instance.BaitFLittleGuys.Count;
        LittleGuyGText.text = "" + Singleton.Instance.BaitGLittleGuys.Count;
    }
}
