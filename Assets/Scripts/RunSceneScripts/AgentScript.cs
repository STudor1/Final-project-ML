using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

//This is not my code
//There are snippets from:
// https://learn.unity.com/tutorial/onactionreceived?uv=2019.3&courseId=5e470160edbc2a15578b13d7&projectId=5ec830c4edbc2a44309bf21c# 
// and from
// Some bits are added by me
// There are changes where it makes sense done by me
public class AgentScript : Agent
{
    new private Rigidbody rigidbody;
    [SerializeField] private Transform exit;
    private Vector3 startPos;
    [SerializeField] private float speed = 1f;
    //[SerializeField] private float yawSpeed = 1f;
    private float randomSpeed;

    private float valueLeft;
    private float valueRight;
    private float valueForward;
    private float valueBack;
    private float maxValue;

    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isMovingForward;
    private bool isMovingBackwards;

    private void Start()
    {
        //Debug.Log("The exit is at " + exit.position);
        startPos = transform.position;
    }

    //Initialize the agent
    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();

        //training forever
        //MaxStep = 0;
    }

    //Resets the agent when a new episode begins
    public override void OnEpisodeBegin()
    {
        //Resets the reward gained 
        SetReward(0);

        //Stop the movement when we begin a new episode
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        //Resets the position to the initial position
        transform.position = startPos; 

        //If this works I could add a random chance for the agent to spawn on a different rotation (left now is front etc)

    }

    //Index 0: move x (+1 = right, -1 = left)
    //Index 1: move z (+1 = forward, -1 = backwards)
    //Index 2: yaw angle (+1 = turn right, -1 = turn left)
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        //Vector3 move = new Vector3(moveX, transform.position.y, moveZ);
        
        transform.position += new Vector3(moveX, transform.position.y, moveZ) * Time.deltaTime * speed;

        //rigidbody.AddForce(move * Time.deltaTime * speed);

        //Get current rotation
        //Vector3 rotationVector = transform.rotation.eulerAngles;

        ////Get yaw
        //float yawChange = actions.ContinuousActions[2];

        //float yaw = rotationVector.y + yawChange * Time.fixedDeltaTime * yawSpeed;

        ////Apply the rotation
        //transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        AddReward(+0.01f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //agents rotation
        //sensor.AddObservation(transform.localRotation.normalized); //4 observations 


        //now we need to get a sense of direction


        //RaycastHit hitFront;
        //Ray frontRay = new Ray(transform.position, Vector3.forward);



        //if (Physics.Raycast(frontRay, out hitFront))
        //{
        //    Debug.DrawRay(transform.position, transform.forward, Color.red);
        //    //Debug.Log("Front " + hitFront.distance);
        //    //valueForward = hitFront.distance;
        //    Vector3 toObject = hitFront.point - transform.position;
        //    sensor.AddObservation(toObject.normalized); //3 obs
        //}

        sensor.AddObservation(exit.position); //3
        ////8 observations
        ///
        //RaycastHit hitFront;
        //Ray frontRay = new Ray(transform.position, Vector3.forward);

        //RaycastHit hitBack;
        //Ray backRay = new Ray(transform.position, Vector3.back);

        //RaycastHit hitLeft;
        //Ray leftRay = new Ray(transform.position, Vector3.left);

        //RaycastHit hitRight;
        //Ray rightRay = new Ray(transform.position, Vector3.right);

        //if (Physics.Raycast(frontRay, out hitFront))
        //{
        //    Debug.DrawRay(transform.position, transform.forward, Color.red);
        //    //Debug.Log("Front " + hitFront.distance);
        //    valueForward = hitFront.distance;
        //}

        //if (Physics.Raycast(backRay, out hitBack))
        //{
        //    Debug.DrawRay(transform.position, -transform.forward, Color.green);
        //    //Debug.Log("Back " + hitBack.distance);
        //    valueBack = hitBack.distance;
        //}

        //if (Physics.Raycast(leftRay, out hitLeft))
        //{
        //    Debug.DrawRay(transform.position, -transform.right, Color.blue);
        //    //Debug.Log("Left " + hitLeft.distance);
        //    valueLeft = hitLeft.distance;
        //}

        //if (Physics.Raycast(rightRay, out hitRight))
        //{
        //    Debug.DrawRay(transform.position, transform.right, Color.yellow);
        //    //Debug.Log("Right " + hitRight.distance);
        //    valueRight = hitRight.distance;
        //}

        //maxValue = Mathf.Max(valueForward, valueBack, valueLeft, valueRight);

        ////in my mind this would move the agent along the correct ray with the random values given by the mlagents?
        //if (maxValue == valueForward)
        //{
        //    isMovingForward = true;
        //    //moves the aganet along the forward ray
        //    //transform.position += new Vector3(moveX, transform.position.y, +moveZ) * Time.deltaTime * randomSpeed;
        //}
        //else if (maxValue == valueBack)
        //{
        //    isMovingBackwards = true;
        //    //moves the aganet along the backward ray
        //    //this should also be affected by a weight as when we are in the middle of a path we get stuck
        //    // transform.position += new Vector3(moveX, transform.position.y, -moveZ) * Time.deltaTime * randomSpeed * -2f;
        //}
        //else if (maxValue == valueLeft)
        //{
        //    isMovingLeft = true;
        //    //this should also be affected by a weight as when we are in the middle of a path we get stuck
        //    //moves the aganet along the left ray
        //    // transform.position += new Vector3(-moveX, transform.position.y, moveZ) * Time.deltaTime * randomSpeed * -2f;
        //}
        //else if (maxValue == valueRight)
        //{
        //    isMovingRight = true;
        //    //moves the aganet along the right ray
        //    // transform.position += new Vector3(+moveX, transform.position.y, moveZ) * Time.deltaTime * randomSpeed;
        //}

        //if (isMovingBackwards)
        //{
        //    if (hitFront.distance > 0.75)
        //    {
        //        isMovingForward = true;
        //        isMovingBackwards = false;
        //    }
        //}

        //if (isMovingRight)
        //{
        //    if (hitLeft.distance > 0.75)
        //    {
        //        isMovingLeft = true;
        //        isMovingRight = false;
        //    }
        //}

        ////This adds the destination it is aiming to get to
        //if (isMovingForward)
        //{
        //    Vector3 targetPosition = hitFront.point;
        //    sensor.AddObservation(targetPosition);
        //}
        //else if (isMovingBackwards)
        //{
        //    Vector3 targetPosition = hitBack.point;
        //    sensor.AddObservation(targetPosition);
        //}
        //else if (isMovingLeft)
        //{
        //    Vector3 targetPosition = hitLeft.point;
        //    sensor.AddObservation(targetPosition);
        //}
        //else if (isMovingRight)
        //{
        //    Vector3 targetPosition = hitRight.point;
        //    sensor.AddObservation(targetPosition);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<exit>(out exit exit))
        {
            //SetReward(+1f);
            AddReward(+1000f);
            EndEpisode();
        }
        if (other.TryGetComponent<wall>(out wall wall))
        {
            //SetReward(-1f);
            AddReward(-100f);
            EndEpisode();
        }
    }

}
