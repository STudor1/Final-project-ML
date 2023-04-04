using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;


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

//thats the GA part
//now for the NN part we just pass the stuff into the ml agents classes which are nn?
public class GeneticAlgo : Agent
{
    [SerializeField] private AgentsManager manager;
    [SerializeField] private Transform exit;
    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;
    private float randomSpeed;
    private Vector3 startPos;
    private bool firstRun = true;

    private void Start()
    {
        //Debug.Log("The exit is at " + exit.position);
        startPos = transform.position;
    }

    //Step 1 and 5
    //This initialises the speed ONCE, using lazy initialize I keep calling this function
    public override void Initialize()
    {
        if (firstRun)
        {
            Debug.Log("Step 1");
            randomSpeed = Random.Range(speedMin, speedMax);
            //firstRun = false;
        }
        else
        {
            //this should ask the manager for the cross over speed?
            randomSpeed = manager.CrossOver();
            //do a mutation?
            Debug.Log("Step 5");
            randomSpeed = randomSpeed + Random.Range(speedMin, speedMax - 1.5f); // slightly change the value 
        }

        
    }

    //Gets called every time a new test is run
    public override void OnEpisodeBegin()
    {
        if (firstRun)
        {
            firstRun = false;
        }
        else
        {
            Debug.Log("Begin");
            Initialize(); //calling the initialize funtion every new run
            transform.position = startPos; //resets pos to initial pos
        }
        
    }

    //Step 2
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("Step 2");
        //discrete
        //I have 1 bracnh with all values

        //this is where i should take the decisions to go left/right etc
        float action = actions.DiscreteActions[0];

        //float doNothing = actions.DiscreteActions[0];
        //float moveLeft = actions.DiscreteActions[1];
        //float moveRight = actions.DiscreteActions[2];
        //float moveForward = actions.DiscreteActions[3];
        //float moveBackward = actions.DiscreteActions[4];

        switch (action)
        {
            case 0:
                //Do nothing
                break;
            case 1:
                //Take a left
                MoveLeft();
                break;
            case 2:
                //Take a left
                MoveRight();
                break;
            case 3:
                //Take a left
                MoveForward();
                break;
            case 4:
                //Take a left
                MoveBackward();
                break;
            default:
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<exit>(out exit exit))
        {
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<wall>(out wall wall))
        {
            SetReward(-1f);
            EndEpisode();
        }
    }

    public float GetSpeed()
    {
        return randomSpeed;
    }

    private void MoveLeft()
    {
        transform.position += new Vector3(transform.position.x - 1 , transform.position.y, transform.position.z) * Time.deltaTime * randomSpeed;
    }

    private void MoveRight()
    {
        transform.position += new Vector3(transform.position.x + 1, transform.position.y, transform.position.z) * Time.deltaTime * randomSpeed;

    }

    private void MoveForward()
    {
        transform.position += new Vector3(transform.position.x, transform.position.y, transform.position.z + 1) * Time.deltaTime * randomSpeed;

    }

    private void MoveBackward()
    {
        transform.position += new Vector3(transform.position.x, transform.position.y, transform.position.z - 1) * Time.deltaTime * randomSpeed;
    }
}
