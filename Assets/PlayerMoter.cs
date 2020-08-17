 using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMoter : MonoBehaviour{

    NavMeshAgent Agent;

    
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    /*public void MoveToPoint(Vector3 point)
    {
        Agent.SetDestination(point);
    }*/
}
//ok should we keep it for makeing a simple way of mangeing a way of places we can go and we cannot