using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


//what i need to do
//use raycasts observations for the agents -> i also need to add the starting pos and the reaching goal pos?
//have raycasts in the front, back, left and right side to start with
//if i use only use raycasts then no need to have collect observations as per documentation


//This might be useful
//When an agent uses the discrete action space, the values in the action array are integers that each represent a specific, discrete action.
//For example, you could define a set of discrete actions such as:

//0 = Do nothing
//1 = Move one space left
//2 = Move one space right
//3 = Move one space up
//4 = Move one space down

//What i would need to do is:
//use raycasts to get how far from a wall the agent is
//have forward have a bigger weight e.g times it by 1.5
//then have the agent move in the direction where the raycast has the biggest value
// e.g 
//  find larger raycast value
// if ray cast left then do this:
// float moveLeft = actions.DiscreteActions[1];
// transform.position += new Vector3(moveX, transform.position.y, moveZ) * Time.deltaTime * moveSpeed;
// if raycast right do this :
// etc etc

//say for a genetic algo
//step 1: initialise
//we set random speed to every agent
//step 2: run the test
//we run and test how far each agent makes it in the maze
//step 3: selection
//select the best performing x agents
//step 4: cross over
//cross over their genes (speed?)
//step 5: mutation
//slighlty adjust the genes (speed?)
//repeat step 2 to 5

//thats the GA part
//now for the NN part we just pass the stuff into the ml agents classes which are nn?
public class GeneticAlgoRay : Agent
{
    [SerializeField] private AgentsManager manager;
    [SerializeField] private Transform exit;
    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;
    private float randomSpeed = 0.75f;
    private Vector3 startPos;
    private bool firstRun = true;
    new private Rigidbody rigidbody;

    //weights
    private float wLeft;
    private float wRight;
    private float wForward;
    private float wBack;

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

    //private void Update()
    //{
    //    //transform.rotation(0f, 0f, 0f);
    //    //transform.Rotate(0f,0f,0f);

       

    //}

    //Step 1 and 5
    //This initialises the speed ONCE, using lazy initialize I keep calling this function
    public override void Initialize()
    {
        //if (firstRun)
        //{
            rigidbody = GetComponent<Rigidbody>();
            Debug.Log("Step 1");
            randomSpeed = Random.Range(speedMin, speedMax);
            firstRun = false;


        //}



    }

    //Gets called every time a new test is run
    public override void OnEpisodeBegin()
    {
        if (firstRun)
        {
            firstRun = false;
            Debug.Log("Does it get here");
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;

            transform.position = startPos; //resets pos to initial pos
            

            //Debug.Log("Begin");
            //this should ask the manager for the cross over speed?
            randomSpeed = manager.CrossOver();
            //do a mutation?
            Debug.Log("Step 5");
            randomSpeed = Random.Range(speedMin, speedMax);
            randomSpeed = randomSpeed + Random.Range(speedMin, speedMax - 0.5f) - Random.Range(speedMin, speedMax - 0.5f); // slightly change the value 

            //Initialize(); //calling the initialize funtion every new run
        }

    }

    //inputs for the ai, how they observe the environemnt, we need our pos and exit pos
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position); //this sends 3 values
        //sensor.AddObservation(exit.position);

        RaycastHit hitFront;
        Ray frontRay = new Ray(transform.position, Vector3.forward);

        RaycastHit hitBack;
        Ray backRay = new Ray(transform.position, Vector3.back);

        RaycastHit hitLeft;
        Ray leftRay = new Ray(transform.position, Vector3.left);

        RaycastHit hitRight;
        Ray rightRay = new Ray(transform.position, Vector3.right);

        if (Physics.Raycast(frontRay, out hitFront))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            //Debug.Log("Front " + hitFront.distance);
            valueForward = hitFront.distance;
        }

        if (Physics.Raycast(backRay, out hitBack))
        {
            Debug.DrawRay(transform.position, -transform.forward, Color.green);
            //Debug.Log("Back " + hitBack.distance);
            valueBack = hitBack.distance;
        }

        if (Physics.Raycast(leftRay, out hitLeft))
        {
            Debug.DrawRay(transform.position, -transform.right, Color.blue);
            //Debug.Log("Left " + hitLeft.distance);
            valueLeft = hitLeft.distance;
        }

        if (Physics.Raycast(rightRay, out hitRight))
        {
            Debug.DrawRay(transform.position, transform.right, Color.yellow);
            //Debug.Log("Right " + hitRight.distance);
            valueRight = hitRight.distance;
        }

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
            Vector3 targetPosition = hitFront.point;
            sensor.AddObservation(targetPosition);
        }
        else if (isMovingBackwards)
        {
            Vector3 targetPosition = hitBack.point;
            sensor.AddObservation(targetPosition);
        }
        else if (isMovingLeft)
        {
            Vector3 targetPosition = hitLeft.point;
            sensor.AddObservation(targetPosition);
        }
        else if (isMovingRight)
        {
            Vector3 targetPosition = hitRight.point;
            sensor.AddObservation(targetPosition);
        }

    }


    //Step 2
    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log("Step 2");

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.position += new Vector3(moveX, transform.position.y, moveZ) * Time.deltaTime * randomSpeed;

        if (transform.position.y < -20)
        {
            SetReward(-10f);
            EndEpisode();
        }

        //every step it doesn't hit something it gets a reward
        AddReward(+0.1f);


        //with these wee need to find the biggest ray and then
        //tell the agent to go in that direction with this:
        //transform.position += new Vector3(moveX, 1, moveZ) * Time.deltaTime * randomSpeed;
        

        //discrete
        //I have 1 bracnh with all values

        //this is where i should take the decisions to go left/right etc
        //float action = actions.DiscreteActions[0];

        ////float doNothing = actions.DiscreteActions[0];
        ////float moveLeft = actions.DiscreteActions[1];
        ////float moveRight = actions.DiscreteActions[2];
        ////float moveForward = actions.DiscreteActions[3];
        ////float moveBackward = actions.DiscreteActions[4];

       

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

    public float GetSpeed()
    {
        return randomSpeed;
    }
}
