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
    private GeneticAlgoRay agent1;
    private GeneticAlgoRay agent2;
    private GeneticAlgoRay[] agents;
    //private GeneticAlgo agent1;
    //private GeneticAlgo agent2;
    //private GeneticAlgo[] agents;
    float max1 = -10000;
    float max2 = -10000;

    private void Start()
    {
        agents = FindObjectsOfType<GeneticAlgoRay>(); //get all the agents in the scene
        //agents = FindObjectsOfType<GeneticAlgo>(); //get all the agents in the scene

    }

    //Step 3
    //loop thru every agent in the scene
    //check their scores
    //select the 2 with the best scores or the ones that made it furthest in the maze (idk how to determine this yet)
    private void Selection()
    {
        Debug.Log("Step 3 ");

        
        //loop thru all agents
        //select the best 2
        //assign them to agent 1 and 2;
        //Debug.Log(agents.Length + "long");
        for (int i = 0; i < agents.Length; i++)
        {
            //Debug.Log(agents[i].GetCumulativeReward());
            if (agents[i].GetCumulativeReward() > max1 )
            {
                max1 = agents[i].GetCumulativeReward();
                agent1 = agents[i];
                //Debug.Log(agent1 + "HERE");
            }
            else if (agents[i].GetCumulativeReward() > max2)
            {
                max2 = agents[i].GetCumulativeReward();
                agent2 = agents[i];
                //Debug.Log(agent2 + "HERE2");
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

        //This is a check to see if the speeds feed into the ML algo will be > than the max speed if
        //they are then the speed will have to be reset
        if (speed1 > 1.5)
        {
            speed1 = Random.Range(0.5f, 1.5f);
        }
        if (speed2 > 1.5)
        {
            speed2 = Random.Range(0.5f, 1.5f);
        }
        meanSpeed = (speed1+speed2) / 2f;

        return meanSpeed;
    }
}
