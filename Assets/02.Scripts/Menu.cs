using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class Menu : MonoBehaviour
{
    [SerializeField] LoadImage ldImg;
    [SerializeField] float waitTime;
    int sceneIndex;
    [SerializeField] AudioClip clearClip;

    public InputActionAsset inputActions;

    private InputAction leftTriggerAction;
    void Start()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            sceneIndex = nextSceneIndex;
        }
        else
            sceneIndex = 0;

    }

    private void OnEnable()
    {
        var actionMap = inputActions.FindActionMap("Player");
        leftTriggerAction = actionMap.FindAction("Fire1");

        leftTriggerAction.performed += OnLeftTriggerPressed;
        leftTriggerAction.Enable();
    }
    private void OnLeftTriggerPressed(InputAction.CallbackContext context)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject.name == "PlayBtn")
            {
                ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
                break;
            }
            else if(result.gameObject.name == "ExitBtn")
            {
                ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
                break;
            }
        }
    }

    private void OnDisable()
    {
        leftTriggerAction.performed -= OnLeftTriggerPressed;
    }

    IEnumerator Wait(float waitTime, int sceneIndex)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator ExitGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Application.Quit();
    }

    public void PlayGame()
    {
        SoundManager.Instance.PlaySound(clearClip);


        ldImg.FadeOut();
        StartCoroutine(Wait(waitTime, sceneIndex));

        //SceneManager.LoadScene(stageIndex);
    }

    public void ExitGame()
    {
        ldImg.FadeOut();
        StartCoroutine(ExitGame(waitTime));
        
    }
}
