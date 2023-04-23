using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentRLRun : Agent
{
    //[SerializeField] private Transform exit;
    private Transform exit;
    [SerializeField] private float speed = 1f; //I might use this to set the speed
    private Vector3 startPos;
    new private Rigidbody rigidbody;
    private float yawSpeed = 100f;

    private void Awake()
    {
        exit = GameObject.Find("Exit3D(Clone)").transform;
    }

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
    }

    /// <summary>
    /// This is where we give the agent a sense of the environment around it
    /// We want to pass in the agent's rotation and the Exit position 
    /// There rest of the data is passed thru using the rays, no need to pass it in here
    /// </summary>
    /// <param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localRotation.normalized); //4 observations 
        sensor.AddObservation(exit.position); //3
    }

    //Index 0: move x (+1 = right, -1 = left)
    //Index 1: move z (+1 = forward, -1 = backwards)
    //Index 2: yaw angle (+1 = turn right, -1 = turn left)
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        transform.position += movement * speed * Time.deltaTime;
        //Vector3 move = new Vector3(moveX, transform.position.y, moveZ);

        //transform.position += new Vector3(moveX, transform.position.y, moveZ) * Time.deltaTime * speed;

        //rigidbody.AddForce(move * Time.deltaTime * speed);

        //Get current rotation
        Vector3 rotationVector = transform.rotation.eulerAngles;

        ////Get yaw
        float yawChange = actions.ContinuousActions[2];

        float yaw = rotationVector.y + yawChange * Time.fixedDeltaTime * yawSpeed;

        ////Apply the rotation
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        AddReward(-0.01f); //punish for not doing anything
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

}
