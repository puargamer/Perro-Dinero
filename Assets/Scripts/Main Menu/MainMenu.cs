using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public UnityEngine.Material skyboxMaterial;
    [Range(0, 1)] public float blend;
    public float blendRate;
    public bool backwards;

    private void Update()
    {
        if (!backwards) { blend += blendRate * Time.deltaTime; }
        if (backwards) { blend -= blendRate * Time.deltaTime; }
        skyboxMaterial.SetFloat("_Blend", blend);

        if (blend >=1 && !backwards) { backwards = true; }
        if (blend <= 0 && backwards) { backwards = false; }
    }

    public void StartButton()
    {
        skyboxMaterial.SetFloat("_Blend", 0);
        SceneManager.LoadScene("Thanos");
    }
}
