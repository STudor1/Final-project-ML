using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentBehaviour : Agent
{
    private Vector3 startPos;
    [SerializeField] private Transform exit;

    private void Start()
    {
        //Debug.Log("I'm an agent beep boop");
        //exit = LevelManager.exitTrans;
        Debug.Log(exit.position);
        startPos = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = startPos;
    }

    //inputs for the ai
    //i need the agents pos
    //need the exit pos
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position); //this sends 3 inputs 
        sensor.AddObservation(exit.position);
        

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 1f;
        transform.position += new Vector3(moveX, transform.position.y, moveZ) * Time.deltaTime * moveSpeed;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");

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

    
}
