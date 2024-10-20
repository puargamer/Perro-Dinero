using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaterialsMenu : MonoBehaviour
{
    public TMP_Text Material0Text;
    public TMP_Text Material1Text;
    public TMP_Text Material2Text;
    public TMP_Text Material3Text;
    public TMP_Text Material4Text;



    void Update()
    {
        Material0Text.text = "" + Singleton.Instance.mats[0];
        Material1Text.text = "" + Singleton.Instance.mats[1];
        Material2Text.text = "" + Singleton.Instance.mats[2];
        Material3Text.text = "" + Singleton.Instance.mats[3];
        Material4Text.text = "" + Singleton.Instance.mats[4];
    }
}
