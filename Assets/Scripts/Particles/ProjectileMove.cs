using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ProjectileMove : MonoBehaviour
{
    private Projectile projectile;
    public Image img;
    public Vector3 targetLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0, 1, 0);
        StartCoroutine(Fade());
        StartCoroutine(Kill());
    }
    IEnumerator Kill()
    {
        transform.DOMove(targetLocation, 1);
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
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}