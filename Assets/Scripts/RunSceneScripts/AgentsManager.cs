using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This would be where step 3 and 4 happens
//step 3: selection
//select the best performing x agents
//step 4: cross over
//cross over their genes (speed?)
public class AgentsManager : MonoBehaviour
{
    private float meanSpeed;
    private GeneticAlgo agent1;
    private GeneticAlgo agent2;
    private GeneticAlgo[] agents;

    private void Start()
    {
        agents = FindObjectsOfType<GeneticAlgo>(); //get all the agents in the scene
    }

    //Step 3
    //loop thru every agent in the scene
    //check their scores
    //select the 2 with the best scores or the ones that made it furthest in the maze (idk how to determine this yet)
    private void Selection()
    {
        Debug.Log("Step 3 " + agents.Length);

        float max1 = 0;
        float max2 = 0;
        //loop thru all agents
        //select the best 2
        //assign them to agent 1 and 2;
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].GetCumulativeReward() > max1 )
            {
                max1 = agents[i].GetCumulativeReward();
                agent1 = agents[i];
            }
            else if (agents[i].GetCumulativeReward() > max2)
            {
                max2 = agents[i].GetCumulativeReward();
                agent2 = agents[i];
            }
        }

    }

    //Step 4 
    //cross them over by
    //taking their speed and doing a mean
    public float CrossOver()
    {
        Selection();

        Debug.Log("Step 4");

        float speed1 = agent1.GetSpeed(); //breaks here
        float speed2 = agent2.GetSpeed();
        meanSpeed = (speed1+speed2) / 2f;

        return meanSpeed;
    }
}
