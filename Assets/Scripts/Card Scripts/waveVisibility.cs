﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveVisibility : MonoBehaviour
{
    public List<Sprite> animation = new List<Sprite>();
    int counter = 0;
    public Image toAnimate;
    float time;
    [Range(2, 0)]
    public float speed = 1;
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= speed / 10)
        {
            toAnimate.sprite = animation[counter];
            counter++;
            time = 0;
            if (counter >= animation.Count - 1)
            {
                Destroy(gameObject);
            }
        }
    }
}