using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NameTooltip : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    //public GameObject box;
    bool visible = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TooltipSwitch()
    {
        visible = !visible;

        if (visible)
        {
            moveNameTip();
        }
        else
        {
            HideTip();
        }
    }
    
    public void DeactivateToolTip()
    {
        if (visible)
        {
            HideTip();
            visible = false;
        }
    }

    private void moveNameTip()
    {

        for (int i = 0; i <= objects.Count - 1; i++)
        {
            objects[i].transform.position = objects[i].transform.position + new Vector3(-20f, 0, 0);
            objects[i].transform.DOScale(1f, 0f).SetEase(Ease.OutQuart);
            objects[i].SetActive(true);
            objects[i].transform.DOLocalMove(new Vector3(15, -26, 0), 0.7f).SetEase(Ease.OutQuart);
        }
        /*
        box.transform.DOScale(1f, 0f).SetEase(Ease.OutQuart);
        box.transform.position = box.transform.position + new Vector3(0, -20f, 0);
        box.SetActive(true);
        box.transform.DOLocalMove(new Vector3(447, 600, 0), 0.7f).SetEase(Ease.OutQuart);
        */
    }

    private void HideTip()
    {
        for (int i = 0; i <= objects.Count - 1; i++)
        {
            objects[i].transform.DOScale(0.7f, 0.2f).SetEase(Ease.InQuart);
            //StartCoroutine(Hide());
        }
        //box.transform.DOScale(0.7f, 0.2f).SetEase(Ease.InQuart);
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        for (int i = 0; i <= objects.Count - 1; i++)
        {
            objects[i].SetActive(false);
        }
        //box.SetActive(false);
    }
}
