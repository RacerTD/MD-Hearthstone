using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenSpawnMusic : MonoBehaviour
{
    public AudioClip music;
    bool yes = true;
    // Start is called before the first frame update
    void Start()
    {


        AudioManager.Instance.music = music;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (yes == true && GetComponent<EnemyFieldScript>().enemyWaveCount == 3)
        {
            Start();
        }
    }
}
