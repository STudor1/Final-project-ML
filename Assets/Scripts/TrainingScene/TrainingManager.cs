using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//find the agent, replace it with ai one, destroy original 
public class TrainingManager : MonoBehaviour
{
    [SerializeField] private GameObject agentRL;
    private GameObject agentToReplace;
    private Vector3 agentPos;

    private void Awake()
    {
        agentToReplace = GameObject.Find("AgentTest(Clone)");
        agentPos = agentToReplace.transform.position;
        Debug.Log(agentToReplace.transform.position);
    }
    // Start is called before the first frame update
    void Start()
    {
        var spawnedObstacle = Instantiate(agentRL, agentPos, Quaternion.identity);
        Destroy(GameObject.Find("AgentTest(Clone)"));
    }
}
