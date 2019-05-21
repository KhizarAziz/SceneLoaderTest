using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothCanvas : MonoBehaviour
{

    [Tooltip ("Player camera which is responsible to interact with canvas. if you leave this field empty, it will find the first camera in the scene")]
    public Transform playerCamera;
    [Tooltip("The slight elastic movement of the canvas related to camera movement. Increase the value to make the canvas act fast")]
    [Range(1,20)]
    public float CanvasRigidity=12;

    
    private void Awake()
    {
 
        if (playerCamera == null )
        {
           playerCamera = FindObjectOfType<Camera>().transform;
        }
         

    } 

    // Update is called once per frame
    void Update()
    {
        
        //canvas is following the player's view with a slight Lerp motion
        FollowPlayerView();
    }


    void FollowPlayerView() {

        transform.position = playerCamera.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, playerCamera.rotation, CanvasRigidity * Time.deltaTime);


    }


}
