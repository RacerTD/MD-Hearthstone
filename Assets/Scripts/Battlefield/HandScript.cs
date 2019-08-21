﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandScript : MonoBehaviour
{
    public GameObject playerField;
    public GameObject manPower;
    public GameObject mana;
    private int childCount;

    private OneCardManager curHover = null;

    //Liste aller Handkarten
    public List<OneCardManager> cards = new List<OneCardManager>();

    void Start()
    {
        GameManager.Main.cardsSideBySide = GetComponent<CardsSideBySide>();
    }

    private void Update()
    {
        if (childCount != transform.childCount)
        {
            childCount = transform.childCount;
            for (int i = childCount - 1; i > 0; i--)
            {
                transform.GetChild(i).GetComponent<Draggable>().enabled = true;
            }
        }
        cards = GetComponentsInChildren<OneCardManager>().ToList();
        FindHover();
        ZoomHover();
    }

    private void ZoomHover()
    {
        foreach (OneCardManager card in cards)
        {
            bool hovered = card == curHover;
            Vector3 targetSize = new Vector3(GameManager.Main.cardsSideBySide.scale, GameManager.Main.cardsSideBySide.scale, GameManager.Main.cardsSideBySide.scale);
            if (hovered) targetSize = targetSize * 1.6f;

            Vector3 targetRotation = card.targetRotation;
            if(hovered) targetRotation=new Vector3();

            Vector3 targetPosition = card.targetPosition;
            if (hovered) targetPosition.y += 200;

            float speed = 0.12f;
            if (hovered) speed = 0.26f;

            card.transform.localScale = Vector3.Lerp(card.transform.localScale, targetSize, speed);
            card.transform.localPosition = Vector3.Lerp(card.transform.localPosition, targetPosition, speed);
            card.transform.localRotation = Quaternion.Lerp(card.transform.localRotation, Quaternion.Euler(targetRotation), speed);
            //Debug.Log(card.transform.localScale+" -> " + targetSize + " cur:" + cards.IndexOf(curHover));
        }

        List<OneCardManager> sorted = cards.OrderBy(c => c.transform.position.x).ToList();
        for (int i = 0; i < sorted.Count; i++)
        {
            sorted[i].transform.SetSiblingIndex(i);
            sorted[i].name = "HandCard:" + i;
        }

        if (curHover != null)
        {
            curHover.transform.SetAsLastSibling();
            curHover.name = "HandCard: hover";
        }
    }

    private void FindHover()
    {
        OneCardManager oldHover = curHover;
        curHover = null;
        Vector2 mpos = Input.mousePosition;
        float y = mpos.y / transform.parent.localScale.y;

        float bestDistX = 100f;
        foreach(OneCardManager card in cards)
        {
            float distX = Mathf.Abs(card.transform.position.x - mpos.x) / transform.parent.localScale.x;
            if (y > 300 && card != oldHover) continue;
            if (y > 400 && card == oldHover) continue;
            if (card == oldHover) distX *= 0.8f;
            if (distX<bestDistX)
            {
                bestDistX = distX;
                curHover = card;
            }
        }

        //Debug.Log(y+" x:"+bestDistX+" cur:"+ cards.IndexOf(curHover));

    }
}
