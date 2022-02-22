using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
public class DummyScript : MonoBehaviour
{
    public GameObject aim;
    private bool walking;
    [Range(0.0f, 1.0f)]
    public float attention;
    private Animator a;
    private NavMeshAgent n;
    [SerializeField] private Transform movePositionTransform;

    private void Awake()
    {
        a = GetComponent<Animator>();
        n = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        n.destination = movePositionTransform.position;
        bool isArrived = (transform.position - movePositionTransform.position).magnitude <= 0.8;
        a.SetBool("walking",!isArrived);
        var sources = aim.GetComponent<MultiAimConstraint>().data.sourceObjects;
        sources.SetWeight(0, attention);
        aim.GetComponent<MultiAimConstraint>().data.sourceObjects = sources;
    }
}
