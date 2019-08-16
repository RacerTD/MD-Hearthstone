using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageNumber : MonoBehaviour
{
    public Image image;

    private GameManager gameManager;
    private Vector3 spawnPosition;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        //Debug.Log("Number got its number");
        spawnPosition = gameManager.particlePosition[0];
        transform.DOScale(3f, 0f);
        //enable = true;
        transform.position = spawnPosition;
        transform.DOScale(2f, 1f).SetEase(Ease.OutElastic);
        StartCoroutine(Fade());
        StartCoroutine(KillSystem());
    }
    IEnumerator Fade()
    {
        // fade from opaque to transparent
        // loop over 1 second backwards
        yield return new WaitForSecondsRealtime(1);
        
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            image.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
    IEnumerator KillSystem()
    {
        yield return new WaitForSecondsRealtime(3f);
        Destroy(gameObject);
    }
}
