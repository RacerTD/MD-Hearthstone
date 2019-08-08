using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    float waitTime = 0.01f;
    float duration1 = 0.2f;
    float duration2 = 0.4f;
    float duration3 = 0.5f;
    float duration4 = 0.6f;
    float timer = 0.0f;
    float durationTimer=0.0f;
    bool t = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown("t") && t==(false))
        {
            for (int i = 0; i <= objects.Count - 1; i++)
            {
                objects[i].transform.position = new Vector3(-1.0f, 2.0f, 0.0f);
                //objects[i].GetComponent<SpriteRenderer>().color = new Vector4(0f, 0f, 0f, 0f);
                objects[i].SetActive(true);
            }
            t = true;
            durationTimer = 0.0f;
        }
        durationTimer += Time.deltaTime;
        if (timer > waitTime && t && durationTimer < duration1)
        {
            for (int i = 0; i <= objects.Count - 1; i++)
            {
                timer = timer - waitTime;
                objects[i].transform.position = objects[i].transform.position + new Vector3(3f, 0.0f, 0.0f) * Time.deltaTime;
                //objects[i].GetComponent<SpriteRenderer>().color = new Vector4(0f, 0f, 0f, 0.3f);
            }
        }

        if (timer > waitTime && t && durationTimer < duration2)
        {
            for (int i = 0; i <= objects.Count - 1; i++)
            {
                timer = timer - waitTime;
                objects[i].transform.position = objects[i].transform.position + new Vector3(2f, 0.0f, 0.0f) * Time.deltaTime;
                //objects[i].GetComponent<SpriteRenderer>().color = new Vector4(0f, 0f, 0f, 0.6f);
            }
        }

        if (timer > waitTime && t && durationTimer < duration3)
        {
            for (int i = 0; i <= objects.Count - 1; i++)
            {
                timer = timer - waitTime;
                objects[i].transform.position = objects[i].transform.position + new Vector3(1f, 0.0f, 0.0f) * Time.deltaTime;
                //objects[i].GetComponent<SpriteRenderer>().color = new Vector4(0f, 0f, 0f, 0.8f);
            }
        }

        if (timer > waitTime && t && durationTimer < duration4)
        {
            for (int i = 0; i <= objects.Count - 1; i++)
            {
                timer = timer - waitTime;
                objects[i].transform.position = objects[i].transform.position + new Vector3(0.5f, 0.0f, 0.0f) * Time.deltaTime;
                //objects[i].GetComponent<SpriteRenderer>().color = new Vector4(0f, 0f, 0f, 1f);
            }
        }

        if (durationTimer > duration4){
            if (Input.GetKeyDown("t"))
            {
                for (int i = 0; i <= objects.Count - 1; i++)
                {
                    objects[i].SetActive(false);
                }
                t = false;
            }
        }
    }
}
