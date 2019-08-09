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
                objects[i].transform.position = objects[i].transform.position + new Vector3(-1.5f, 0.0f, 0.0f);
                objects[i].transform.DOScale(1f, 0f).SetEase(Ease.OutQuart);
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
                timer = timer - waitTime;
                objects[i].transform.DOLocalMoveX(1.5f, 1).SetEase(Ease.OutQuart);
            }
        }

        if (durationTimer > duration){
            if (Input.GetKeyDown("t") && t)
            {
                for (int i = 0; i <= objects.Count - 1; i++)
                {
                    objects[i].transform.DOScale(0.7f, 0.2f).SetEase(Ease.InQuart);
                    StartCoroutine(Hide());
                }
                t = false;
            }
        }
    }
    IEnumerator Hide()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        for (int i = 0; i <= objects.Count - 1; i++)
        {
            objects[i].SetActive(false);
        }
        durationTimer = 0.0f;
    }
}
