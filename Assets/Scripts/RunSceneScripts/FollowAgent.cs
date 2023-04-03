using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAgent : MonoBehaviour
{
    [SerializeField] private Transform agent; //when we want anything to do with position we need the transform not the game object
    public float xRoti = 10f; //cam angles by 10 in x axis
    private bool isAgent = false;
    private float xRot = 10f; //cam angles by 10 in x axis
    private float yRot, zRot;
    //We want z = 0 meaning it is on the agent, y 0.75 so it sits a little above the agent and x -1 so it is behind it 
    private Vector3 offset = new Vector3(-1f, 0.75f, 0f);

    //I will use this to get the agent and get the camera to follow it
    //camera has to always be behind the agent and you set the rotation of the camera to follow the agent
    //set cam y to 90 if agent is placed on the left side wall
    private void Awake()
    {
        yRot = transform.localEulerAngles.y;
        zRot = transform.localEulerAngles.z;
        transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
    }

    void Update()
    {
        if (isAgent == false)
        {
            //Debug.Log("Finding agent");
            findAgent();
        }
        else
        {
            transform.position = agent.position + offset; //x, y, z 
        }
    }

    //Finds the agent in the scene
    //There's always going to be an agent (need to add this later)
    private void findAgent()
    {
        agent = GameObject.Find("Agent3D(Clone)").transform;
        //Debug.Log("Agent found");
        isAgent = true;
    }
}
