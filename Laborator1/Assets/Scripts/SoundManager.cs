using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip impulseSound;
    public AudioClip collisionSound;
    public AudioClip finishSound;

    

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySound(impulseSound);
        }
    }
 
}
