using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class fishDap : MonoBehaviour
{
    public float difficulty = 1f;
    private float dapDirection;
    private Vector3 yPos;
    public bool stillDapping;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dapDodge());
    }

    // Update is called once per frame
    void Update()
    {
        if (stillDapping)
        {
            yPos = transform.position;
            yPos.y = Mathf.Clamp(transform.position.y, -.9f, -.2f);
            transform.position = Vector3.Lerp(yPos, transform.position + new Vector3(0f, dapDirection, 0f) * Time.deltaTime, .1f);
            transform.position += new Vector3(.15f, 0f, 0f) * Time.deltaTime * difficulty;
        }
    }

    public IEnumerator dapDodge()
    {
        while (true)
        {
            dapDirection = Random.Range(-5f, 5f);
            yield return new WaitForSeconds(Random.Range(.1f, 3f));
        }
    }
}
