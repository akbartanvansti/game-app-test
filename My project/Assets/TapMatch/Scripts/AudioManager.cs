using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioSource sfxSource;


    public AudioClip button;
    public AudioClip end;
    public AudioClip gameplay;//music
    public AudioClip lobby;//music
    public AudioClip scoring;

    public void Start()
    {
        musicSource.clip = lobby;
        musicSource.Play();
    }
}
