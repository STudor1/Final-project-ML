using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

/// <summary>
/// this is not entirely my code it is from 
/// https://www.youtube.com/watch?v=AxKBKJKBeII
/// and altered, some bits are added from:
/// 
/// and some are my own
/// 
/// new approach
/// only move forward with w
/// turn with a and d 
/// </summary>
public class NewAgent : Agent
{
    [SerializeField] private Transform exit;
    [SerializeField] private float speed = 1f; //I might use this to set the speed
    private Vector3 startPos;
    new private Rigidbody rigidbody;
    [SerializeField] private Transform frontRay;
    [SerializeField] private Transform leftRay;
    [SerializeField] private Transform rightRay;
    [SerializeField] private Transform backRay;
    private RaycastHit hitFront;
    private RaycastHit hitLeft;
    private RaycastHit hitRight;
    private RaycastHit hitBack;

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
        startPos = transform.position;
    }

    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

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

    public override void CollectObservations(VectorSensor sensor)
    {
        //Calculate the distances to see how far from the exit the agent is
        float xDistanceToExit = transform.position.x - exit.position.x;
        float zDistanceToExit = transform.position.z - exit.position.z;

        //We add them to the algorithm so that it knows when it is getting closer to the exit
        sensor.AddObservation(xDistanceToExit); //size of 1
        sensor.AddObservation(zDistanceToExit); //size of 1


        //Adding the distances of the rays until they hit an object
        Physics.Raycast(frontRay.position, frontRay.forward, out hitFront);
        sensor.AddObservation(hitFront.distance); //size of 1
        Physics.Raycast(leftRay.position, leftRay.forward, out hitLeft);
        sensor.AddObservation(hitLeft.distance); //size of 1
        Physics.Raycast(rightRay.position, rightRay.forward, out hitRight);
        sensor.AddObservation(hitRight.distance); //size of 1
        Physics.Raycast(backRay.position, backRay.forward, out hitBack);
        sensor.AddObservation(hitBack.distance); //size of 1

        //6 total observations
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //int actionToTake = actions.DiscreteActions[0];
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        //We punish the agent if it does noting 
        AddReward(-0.001f);

        //switch (actionToTake)
        //{
        //    //case 0:
        //    //    break; //if 0 then we do nothing 
        //    case 0:
        //        MoveForward(0.01f); //if 1 then we move forward(z) 
        //        break;
        //    case 1:
        //        AgentRotate(5f); //if 2 we rotate to the right (y) 45 degrees 
        //        break;
        //    case 2:
        //        AgentRotate(-5f); //if 3 we rotate to the left (y) 45 degrees 
        //        break;
        //}

        valueForward = hitFront.distance;
        valueBack = hitBack.distance;
        valueLeft = hitLeft.distance;
        valueRight = hitRight.distance;
        
        maxValue = Mathf.Max(valueForward, valueBack, valueLeft, valueRight);

        //in my mind this would move the agent along the correct ray with the random values given by the mlagents?
        if (maxValue == valueForward)
        {
            isMovingForward = true;
            //moves the aganet along the forward ray
            //transform.position += new Vector3(moveX, transform.position.y, +moveZ) * Time.deltaTime * randomSpeed;
        }
        else if (maxValue == valueBack)
        {
            isMovingBackwards = true;
            //moves the aganet along the backward ray
            //this should also be affected by a weight as when we are in the middle of a path we get stuck
            // transform.position += new Vector3(moveX, transform.position.y, -moveZ) * Time.deltaTime * randomSpeed * -2f;
        }
        else if (maxValue == valueLeft)
        {
            isMovingLeft = true;
            //this should also be affected by a weight as when we are in the middle of a path we get stuck
            //moves the aganet along the left ray
            // transform.position += new Vector3(-moveX, transform.position.y, moveZ) * Time.deltaTime * randomSpeed * -2f;
        }
        else if (maxValue == valueRight)
        {
            isMovingRight = true;
            //moves the aganet along the right ray
            // transform.position += new Vector3(+moveX, transform.position.y, moveZ) * Time.deltaTime * randomSpeed;
        }

        if (isMovingBackwards)
        {
            if (hitFront.distance > 0.75)
            {
                isMovingForward = true;
                isMovingBackwards = false;
            }
        }

        if (isMovingRight)
        {
            if (hitLeft.distance > 0.75)
            {
                isMovingLeft = true;
                isMovingRight = false;
            }
        }

        //This adds the destination it is aiming to get to
        if (isMovingForward)
        {
            transform.position += new Vector3(transform.position.x, transform.position.y, moveZ) * Time.deltaTime * speed;
        }
        else if (isMovingBackwards)
        {
            transform.position += new Vector3(transform.position.x, transform.position.y, -moveZ) * Time.deltaTime * speed;
        }
        else if (isMovingLeft)
        {
            transform.position += new Vector3(-moveX, transform.position.y, transform.position.z) * Time.deltaTime * speed;

        }
        else if (isMovingRight)
        {
            transform.position += new Vector3(moveX, transform.position.y, transform.position.z) * Time.deltaTime * speed;

        }


        //add an if stat to check if the agent is out of boundry, not useful for testing 
        //this is more the case of polishing the algo when we let the players create a maze
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Forward");
            MoveForward(0.1f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            AgentRotate(-45f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            AgentRotate(45f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<exit>(out exit exit))
        {
            //SetReward(+1f);
            AddReward(+100f);
            EndEpisode();
        }
        if (other.TryGetComponent<wall>(out wall wall))
        {
            //SetReward(-1f);
            AddReward(-100f);
            EndEpisode();
        }
    }

    private void MoveForward(float z)
    {
        transform.Translate(0f, 0f, z);
    }

    private void AgentRotate(float y)
    {
        transform.Rotate(0f, y, 0f);
    }
}
