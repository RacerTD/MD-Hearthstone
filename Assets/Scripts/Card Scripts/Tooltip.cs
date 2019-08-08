using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Tooltip : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    float waitTime = 0.01f;
    float duration = 1f;
    float timer = 0.0f;
    float durationTimer=0.0f;
    bool t = false;
    Transform myChild;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown("t") && t==false)
        {
            for (int i = 0; i <= objects.Count - 1; i++)
            {
                objects[i].transform.position = objects[i].transform.position + new Vector3(-0.5f, 0.0f, 0.0f);

                //myChild = transform.GetChild(0);
                //myChild.GetComponent<Image>().color = new Vector4(0,0,0,0);

                objects[i].SetActive(true);
            }
            t = true;
            durationTimer = 0.0f;
        }
        durationTimer += Time.deltaTime;

        if (timer > waitTime && t && durationTimer < duration)
        {
            for (int i = 0; i <= objects.Count - 1; i++)
            {
                //objects[i].GetComponentsInChildren<Image>()
                timer = timer - waitTime;
                objects[i].transform.DOLocalMoveX(0.5f, 1).SetEase(Ease.OutQuart);
                //myChild.GetComponent<Image>().color = new Vector4(0, 0, 0, 1) * (durationTimer / duration);
            }
        }

        if (durationTimer > duration){
            if (Input.GetKeyDown("t") && t)
            {
                for (int i = 0; i <= objects.Count - 1; i++)
                {
                    
                    objects[i].SetActive(false);
                    durationTimer = 0.0f;
                }
                t = false;
            }
        }
    }
}
