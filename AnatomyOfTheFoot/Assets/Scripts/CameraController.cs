using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

//Holds all node info, e.g pos, name
struct BoneNode
{
  //  List<TimelineAsset> s_Parts;



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
    [Tooltip("AutoRotate on start, otherwise will start progressing into first node.")]
    [SerializeField] private bool m_AutoRotateOnStart = false;
    [Tooltip("AutoRotate speed, DEFAULT: 30.0f")]
    [SerializeField] private float m_RotateSpeed = 30.0f;

    [Header("Subjects")]
    [SerializeField] private GameObject m_Leg;
    [SerializeField] private GameObject m_Foot;
    [SerializeField] private TimelineAsset m_TOne;
    [SerializeField] private TimelineAsset m_TTwo;
    [SerializeField] private TimelineAsset m_TThree;
    [SerializeField] private TimelineAsset m_TFour;
    [SerializeField] private TimelineAsset m_TFive;
    [SerializeField] private TimelineAsset m_TSix;
    [SerializeField] PlayableDirector m_Director;
    [SerializeField] private float m_Speed = 30;


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
    private int pressed = 0;
    private Quaternion m_LegAtStart;
    private Quaternion m_FootAtStart;
    private bool m_PauseLerping = false;
    void Start()
    {
        //Init camera state to zero
        m_CameraState = CameraState.NONE;
        //m_Director = GetComponent<DirectorControlPlayable>();
        m_LegAtStart = m_Leg.transform.rotation;
        m_FootAtStart = m_Foot.transform.rotation;
    }

    void Update()
    {
        
        //Allows manual controll of node traversal
        if (Input.anyKeyDown && m_FormerNodeInputControl != KeyCode.None && m_UpcomingNodeInputControl != KeyCode.None)
           // if (m_Director.state != PlayState.Playing)
                CheckInputControl();
        if (m_Director.state != PlayState.Playing)
        {
            m_AutoRotateOnStart = true;
            m_PauseLerping = false;
        }
        else if (m_Director.state == PlayState.Playing && !m_PauseLerping)
        {
            m_AutoRotateOnStart = false;
            m_Leg.transform.rotation = Quaternion.Lerp(m_Leg.transform.rotation, m_LegAtStart, m_Speed * Time.deltaTime);
            m_Foot.transform.rotation = Quaternion.Lerp(m_Foot.transform.rotation, m_FootAtStart, m_Speed * Time.deltaTime);
        }
        //AutoRotates subjects
        if (m_AutoRotateOnStart)
            AutoRotateObject();

       
    }
    private void CheckInputControl()
    {
        

        if (Input.GetKeyDown(m_UpcomingNodeInputControl))
        {
            //resets timeline and moves all objects back to original position
            m_Director.time = 0;
            m_Director.Evaluate();
            if (pressed != 6) //max
                pressed++;
            PlayTimelineAnim(pressed);
            //progress node.
            Debug.Log("Advance");
        }

        if (Input.GetKeyDown(m_FormerNodeInputControl))
        {
            //resets timeline and moves all objects back to original position
            m_Director.time = 0;
            m_Director.Evaluate();
            if (pressed != 1)//min
                pressed--;
            PlayTimelineAnim(pressed);
            //reverse nodes
            Debug.Log("Reverse");
        }
    }

    private void PlayTimelineAnim(int nodeToPlay)
    {
        switch (nodeToPlay)
        {
            case 1:
                m_Director.playableAsset = m_TOne;
                m_PauseLerping = false;
                m_Director.Play();
                break;
            case 2:

                m_Director.playableAsset = m_TTwo;
                m_PauseLerping = true;
                m_Director.Play();
                break;
            case 3:
                m_Director.playableAsset = m_TThree;
                m_PauseLerping = false;
                m_Director.Play();
                break;
            case 4:
                m_Director.playableAsset = m_TFour;
                m_PauseLerping = false;
                m_Director.Play();
                break;
            case 5:
                m_Director.playableAsset = m_TFive;
                m_PauseLerping = false;
                m_Director.Play();
                break;
            case 6:
                m_Director.playableAsset = m_TSix;
                m_PauseLerping = false;
                m_Director.Play();
                break;
        }
    }
        private void AutoRotateObject() 
    {
        m_Foot.transform.Rotate(0, 0, m_RotateSpeed * Time.deltaTime);
        m_Leg.transform.Rotate(0,0, m_RotateSpeed * Time.deltaTime);
    }
}
