using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{


    private GameObject LoadingScreen;

    [Header("UI Refrences")]
    public Slider loadingBarUI;
    public Text tipTextUI;
    public Animator transitionAnimator;


    [Header("List of Tips to display Randomnly")]
    readonly string dummyString = "This area is to display Random Tips, you can add them on the tips variable.";
    [TextArea]
    public string[] tips;




    public static SceneLoader instance;
    public static SceneLoader Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {

        LoadingScreen = transform.GetChild(0).gameObject;
        loadingBarUI = GetComponentInChildren<Slider>();


    }



    private void Start()
    {
        instance = this;
        LoadingScreen.SetActive(false);
        if(tips.Length > 0) {
            tipTextUI.text = tips[ Random.Range(0,tips.Length-1) ];
        }
        else {
            tipTextUI.text = dummyString;
        }
    }

  


    public void LoadLevel(int index)
    {

        LoadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(index));

    }


    IEnumerator LoadAsynchronously(int sceneIndex)
    {


          AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
          operation.allowSceneActivation = false;

        
          while (!operation.isDone)
          { 
            
              if (operation.progress >= 0.9f)
              {
                // we finally do the transition
                transitionAnimator.SetTrigger("TransitionIn");
                yield return new WaitForSeconds(2f);
                operation.allowSceneActivation = true;
              }

              float progress = Mathf.Clamp01(operation.progress / .9f);
              loadingBarUI.value = progress;
              yield return null;

          }
          
    }

}

