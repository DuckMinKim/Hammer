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

        // 풀에 사용 가능한 오디오가 있으면 가져오기
        if (audioPool.Count > 0)
        {
            audioSource = audioPool.Dequeue();
        }
        else
        {
            GameObject newSound = Instantiate(soundPrefab);
            audioSource = newSound.GetComponent<AudioSource>();
            newSound.transform.parent = transform; // 계층 구조 정리
        }

        audioSource.clip = clip;
        audioSource.Play();

        // 일정 시간이 지나면 다시 풀에 반환
        StartCoroutine(ReturnToPool(audioSource, clip.length));
    }

    private System.Collections.IEnumerator ReturnToPool(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.Stop();
        audioPool.Enqueue(source); // 다시 풀에 저장
    }
}
