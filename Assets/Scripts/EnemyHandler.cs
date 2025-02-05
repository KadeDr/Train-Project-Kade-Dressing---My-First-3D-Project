using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHandler : MonoBehaviour
{
    public bool dead;
    [SerializeField] float waitTime;
    [SerializeField] AudioClip deathSound;
    [SerializeField] ParticleSystem crashFX;
    AudioSource audioSource;
    int currentLevel;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter(Collider other)
    {
        if (dead) return;
        if (other.transform.parent.tag == "Obstacle")
        {
            crashFX.Play();
            StartCoroutine(LoadLevel(currentLevel));
            audioSource.Stop();
            audioSource.PlayOneShot(deathSound);
            dead = true;
        }
    }

    IEnumerator LoadLevel(int level)
    {
        yield return new WaitForSeconds(waitTime);
        if (level >= SceneManager.sceneCountInBuildSettings)
        {
            print("Dont break my game!!!");
            yield break;
        }
        SceneManager.LoadScene(level);
    }

}
