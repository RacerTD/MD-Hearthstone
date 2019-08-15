using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageNumber : MonoBehaviour
{
    public GameObject explosion;
    public Image image;
    bool enable;

    private GameManager gameManager;
    private Vector3 spawnPosition;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        spawnPosition = gameManager.particlePosition[0];
        transform.DOScale(2f, 0f);
        enable = true;
    }

    void Update()
    {
        if (enable)
        {
            Vector2 newPos = spawnPosition + new Vector3(0, 0, 0);
            GameObject myObject = Instantiate(explosion, newPos, Quaternion.identity);
        }
        transform.DOScale(1f, 10f).SetEase(Ease.OutElastic);
        StartCoroutine(Fade());
        StartCoroutine(KillSystem());
    }
    IEnumerator Fade()
    {
        // fade from opaque to transparent
        // loop over 1 second backwards
        yield return new WaitForSecondsRealtime(2);
        
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            image.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
    IEnumerator KillSystem()
    {
        //yield return new WaitForSecondsRealtime(0.8f);
        enable = false;
        yield return new WaitForSecondsRealtime(3f);
        Destroy(gameObject);
    }
}
