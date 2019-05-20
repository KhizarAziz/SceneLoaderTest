using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    

    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            SceneLoader.Instance.LoadLevel(1);
        }
    }


    public void LoadScene(int sceneIndex) {

        SceneLoader.Instance.LoadLevel(sceneIndex);

    }

}
