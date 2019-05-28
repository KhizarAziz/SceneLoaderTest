using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{

public float m_DefaultLength = 500.0f;
public GameObject m_Dot;
public VRInputModule m_InputModule;

private LineRenderer m_LineRenderer = null;


    // Start is called before the first frame update
    void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }


    private void UpdateLine(){

        ///use default or diatamce
        PointerEventData data = m_InputModule.GetData();
        float targetLenght = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;


        //Rayvast
        RaycastHit hit = CreateRayCast();
        
        
        //Default
        Vector3 endPostion = transform.position + (transform.forward * targetLenght);

//        / Or based Hiyt
        if(hit.collider !=null)
        endPostion = hit.point;

        //set linerenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPostion);
        //set Postion of the daot;
        m_Dot.transform.position = endPostion;
    }

    private RaycastHit CreateRayCast(){

        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);


        return hit;

    }


}
