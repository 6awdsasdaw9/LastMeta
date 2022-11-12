using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemies
{
  public class EnemuBehaviour : MonoBehaviour
  {
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> points;

    private void FixedUpdate()
    {
      if (Input.GetKeyDown("z"))
      {
        MoveTo(points[0]);
      }
      if (Input.GetKeyDown("x"))
      {
        MoveTo(points[1]);
      }

    
    }


    private void MoveTo(Transform targetPos)
    {
      if (Vector2.Distance(transform.position, targetPos.position) > agent.stoppingDistance)
      {
        agent.SetDestination(targetPos.position);
      }
    
    }
  }
}
