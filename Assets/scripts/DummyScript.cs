using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
public class DummyScript : MonoBehaviour
{
    private bool iswalking = false;
    private Animator a;
    private NavMeshAgent n;
    private IAManager managerscript;

    public GameObject aim;

    [Range(0.0f, 1.0f)]
    public float attention;
    public bool doWalk = true;
    [SerializeField] public Transform movePositionTransform;

    private void Awake()
    {
        a = GetComponent<Animator>();
        n = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        managerscript = GameObject.FindWithTag("IAManager").GetComponent<IAManager>();
        managerscript.addPeople(this.GetComponent<DummyScript>());
    }

    private void Update()
    {
        if (movePositionTransform!= null && doWalk)
        {
            n.destination = movePositionTransform.position;
            iswalking = (transform.position - movePositionTransform.position).magnitude > 0.8;

        }
        a.SetBool("walking", iswalking && doWalk);
        n.isStopped = !doWalk;
        var sources = aim.GetComponent<MultiAimConstraint>().data.sourceObjects;
        sources.SetWeight(0, attention);
        aim.GetComponent<MultiAimConstraint>().data.sourceObjects = sources;
    }
}
