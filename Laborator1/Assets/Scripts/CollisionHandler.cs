using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesar pentru încărcarea scenelor

public class CollisionHandler : MonoBehaviour
{
    private SoundManager soundManager;

    [SerializeField]
    private ParticleSystem collisionParticle;

    private float timeToReload = 0f; 
    private bool shouldReload = false; 
    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            ReloadScene();
        }

        if (shouldReload && Time.time >= timeToReload)
        {
            ReloadScene();
            shouldReload = false; 
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Planet"))
        {
            if (soundManager != null)
            {
                soundManager.PlaySound(soundManager.collisionSound);
                collisionParticle.Play();
            }

            timeToReload = Time.time + 2f;
            shouldReload = true; 
           // ReloadScene();
        }

        if (collision.collider.CompareTag("Finish"))
        {
            if (soundManager != null)
            {
                soundManager.PlaySound(soundManager.finishSound);
                collisionParticle.Play();
            }

            LoadNextLevel();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            LoadNextLevel();
        }
    }
    private void LoadNextLevel()
    {
        SceneManager.LoadScene("Lab 4 - Fly Level 2");
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }


}
