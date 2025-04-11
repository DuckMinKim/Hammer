using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject soundPrefab;
    Queue<AudioSource> audioPool = new Queue<AudioSource>();
    public static SoundManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void PlaySound(AudioClip clip)
    {
        AudioSource audioSource;

        

        if (audioPool.Count > 0)
        {
            audioSource = audioPool.Dequeue();
        }
        else
        {
            GameObject newSound = Instantiate(soundPrefab);
            audioSource = newSound.GetComponent<AudioSource>();
            newSound.transform.parent = transform; 
        }

        if (audioSource == null || clip == null)
        {
            return;
        }

        audioSource.clip = clip;
        audioSource.Play();


        StartCoroutine(ReturnToPool(audioSource, clip.length));
    }

    private System.Collections.IEnumerator ReturnToPool(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.Stop();
        audioPool.Enqueue(source); 
    }
}
