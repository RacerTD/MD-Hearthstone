using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenSpawnMusic : MonoBehaviour
{
    public AudioClip music;
    public EnemyFieldScript enemyField;
    bool yes = true;

    private void Awake()
    {
        enemyField = GameObject.Find("EnemyField").GetComponent<EnemyFieldScript>();
    }

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
