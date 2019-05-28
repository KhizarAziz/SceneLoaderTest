using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SceneLoader : MonoBehaviour
{


    private GameObject LoadingScreen;
    private static bool isSceneLoading = false;

    [Header("UI Refrences")]
    public Slider loadingBarUI;
    public Text tipTextUI;
    public Animator transitionAnimator;

    [Header("Others")]
    [TextArea]
    string[] tips;
    readonly string dummyTip = "This text area is to display Random Tips, You can add/remove tips in SceneLoaderTest_Data/StreamingAssets/Tips.txt";
   // public TextAsset tipsFile;


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

        InitTips();
        instance = this;
        LoadingScreen.SetActive(false);
        tipTextUI.text = tips[ Random.Range(0,tips.Length) ];
        
    }
    


    public void LoadLevel(int index)
    {
        loadingBarUI.value = 0f;
        LoadingScreen.SetActive(true);
        if(!isSceneLoading)
        StartCoroutine(LoadAsynchronously(index));

    }


    IEnumerator LoadAsynchronously(int sceneIndex)
    {

        isSceneLoading = true;

        //putting a temp delay to check loading canvas
        yield return new WaitForSeconds(3f);


        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
          operation.allowSceneActivation = false;

        
          while (!operation.isDone)
          { 
            

              float progress = Mathf.Clamp01(operation.progress / .9f);
              loadingBarUI.value = progress;
            

              if (operation.progress >= 0.9f)
              {
                // we finally do the transition
                transitionAnimator.SetTrigger("TransitionIn");
                yield return new WaitForSeconds(2f);
                operation.allowSceneActivation = true;
                isSceneLoading = false;
            }

              yield return null;

          } 
        
    }

     
    void InitTips()
    {

        string filePath = Path.Combine(Application.streamingAssetsPath, "Tips.txt");
        if (File.Exists(filePath))
        {
            tips = File.ReadAllLines(filePath);
            if (tips.Length < 1)
            {
                tips[0] = dummyTip;
            }
        }
        else {
            tips[0] = dummyTip; 
        }
        
    }





}

