using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DamageMove : MonoBehaviour
{
    private Damage damage;

    public float time_min = 1f;
    public float time_max = 3f;
    public Sprite[] sprites = new Sprite[12];
    public Image image;

    Vector3 randomDirection; 

    // Start is called before the first frame update
    void Start()
    {
        randomDirection = new Vector3(Random.Range(-5,5), Random.Range(-5, 5), 0);
        StartCoroutine(ShowRandomImage());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + randomDirection;
        StartCoroutine(Fade());
        StartCoroutine(Kill());
    }
    IEnumerator Kill()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
    }
    IEnumerator Fade()
    {
        // fade from opaque to transparent
        // loop over 1 second backwards
        yield return new WaitForSecondsRealtime(1f);
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
    public IEnumerator ShowRandomImage()
    {
        while (true)
        {
            image.sprite = sprites[Random.Range(0, sprites.Length)];
            image.enabled = true;
            yield return new WaitForSeconds(1);
            image.enabled = false;
            yield return new WaitForSeconds(Random.Range(time_min, time_max));
        }


    }
}