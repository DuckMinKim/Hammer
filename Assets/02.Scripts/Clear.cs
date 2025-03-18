using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    [SerializeField] LoadImage ldImg;
    [SerializeField] float waitTime;
    [SerializeField] int sceneIndex;
    [SerializeField] AudioClip clearClip;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col != null && col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.Instance.PlaySound(clearClip);
            ldImg.FadeOut();
            StartCoroutine(Wait(waitTime, sceneIndex));

        }
    }

    IEnumerator Wait(float waitTime, int sceneIndex)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);
    }

}
