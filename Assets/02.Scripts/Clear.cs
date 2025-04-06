using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.IK;

public class Clear : MonoBehaviour
{
    [SerializeField] LoadImage ldImg;
    [SerializeField] float waitTime;
    int sceneIndex;
    [SerializeField] AudioClip clearClip;
    void Start()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            sceneIndex = nextSceneIndex;
        }else
            sceneIndex = 0;

        if (ldImg == null)
            ldImg = GameObject.Find("Load").GetComponent<LoadImage>();
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
