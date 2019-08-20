using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class DeathVictoryAnimation : MonoBehaviour
{
    public List<Sprite> animation = new List<Sprite>();
    int counter = 0;
    public Image toAnimate;
    float time;
    public bool activated = false;

    [Range(2, 0)]
    public float speed = 1;
    void Start()
    {
        //counter = Random.Range(0, animation.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            time += Time.deltaTime;

            if (time >= speed / 10)
            {
                toAnimate.sprite = animation[counter];
                counter++;
                time = 0;
                if (counter >= animation.Count - 1)
                {
                    counter = 0;
                }
            }
        }
    }
}
