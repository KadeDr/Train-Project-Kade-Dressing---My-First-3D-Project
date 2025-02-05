using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundEnding : MonoBehaviour
{
    
    [SerializeField] ParticleSystem WinFX;
    int level;
    EnemyHandler enemyHandler;

    void Awake()
    {
        enemyHandler = GetComponent<EnemyHandler>();
        level = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Finish") return;
        level++;
        WinFX.Play();
        if (level >= SceneManager.sceneCountInBuildSettings) return;
        SceneManager.LoadScene(level);
    }

}
