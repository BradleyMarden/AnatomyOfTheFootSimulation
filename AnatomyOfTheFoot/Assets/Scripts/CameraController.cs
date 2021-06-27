using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds all node info, e.g pos, name
struct BoneNode
{


};
public class CameraController : MonoBehaviour
{
    //Visble In Editor
    [Header("Node Input Controlls")]
    [Tooltip("Input type to move to upcoming node. NOTE: If state is 'NONE' scene will automatically progess through nodes")]
    [SerializeField] private KeyCode m_UpcomingNodeInputControl = KeyCode.RightArrow;
    [Tooltip("Input type to move to former node. NOTE: If state is 'NONE' scene will automatically progess through nodes")]
    [SerializeField] private KeyCode m_FormerNodeInputControl = KeyCode.LeftArrow;
    [Tooltip("Speed of node progression when in 'NONE' state. DEFUALT: 5")]
    [SerializeField] private int m_AutoNodeProgressSpeed = 5;




    //Hidden In Editor
    private enum CameraState
    {
        NONE = 0,
        STARTING = 1,
        TRANSITIONING = 2,
        ENDING = 3,
    }

    private static int m_NoOfNodes = 10;
    private CameraState m_CameraState;
    private BoneNode[] m_ArrayOfNodes = new BoneNode[m_NoOfNodes];


    // Start is called before the first frame update
    void Start()
    {
        //Init camera state to zero
        m_CameraState = CameraState.NONE;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && m_FormerNodeInputControl != KeyCode.None && m_UpcomingNodeInputControl != KeyCode.None)
            CheckInputControl();


        void CheckInputControl()
        {

            if (Input.GetKeyDown(m_UpcomingNodeInputControl))
            {
                //progress node.
                Debug.Log("Advance");
            }

            if (Input.GetKeyDown(m_FormerNodeInputControl))
            {
                //reverse nodes
                Debug.Log("Reverse");

            }

        }
    }
}
