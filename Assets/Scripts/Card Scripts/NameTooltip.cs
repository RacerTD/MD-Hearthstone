using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NameTooltip : MonoBehaviour
{
    public GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveNameTip()
    {
        box.transform.DOScale(1f, 0f).SetEase(Ease.OutQuart);
        box.transform.position = box.transform.position + new Vector3(0, -20f, 0);
        box.SetActive(true);
        box.transform.DOLocalMove(new Vector3(447, 600, 0), 0.7f).SetEase(Ease.OutQuart);
    }

    public void HideTip()
    {
        box.transform.DOScale(0.7f, 0.2f).SetEase(Ease.InQuart);
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        box.SetActive(false);
    }
}
