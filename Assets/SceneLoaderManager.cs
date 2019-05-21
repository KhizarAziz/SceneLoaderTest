using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderManager : MonoBehaviour
{


    private void Awake()
    {

        SceneLoaderManager[] objs = FindObjectsOfType<SceneLoaderManager>();

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        


    }
        // Update is called once per frame
        void Update()
    {

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneLoader.Instance.LoadLevel(0);

        }else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneLoader.Instance.LoadLevel(1);
        }



    }
}
