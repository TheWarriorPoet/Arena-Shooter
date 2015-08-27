using UnityEngine;
using System.Collections;

public class Intercept : Behaviour
{
    //Vector3 targetPos;
    GameObject Player;
    // Use this for initialization
    public Intercept(Agent aAgent) : base(aAgent)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public override void Update()
    {

        Vector3 tempVec = (Player.transform.position - agent.gameObject.transform.position);
        tempVec.Normalize();
        Vector3 targetPos = Player.transform.position + ((Player.GetComponent<CharacterController>().velocity * tempVec.magnitude)/1.5f);

        agent.gameObject.GetComponent<NavMeshAgent>().destination = targetPos;
    }
}
