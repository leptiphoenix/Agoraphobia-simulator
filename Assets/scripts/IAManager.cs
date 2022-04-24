using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class IAManager : MonoBehaviour
{
    [SerializeField]
    private Transform playerpos;
    private List<DummyScript> peoples;
    [Range(0, 3)]
    private int difficulty = 0;

    // Start is called before the first frame update
    void Start()
    {
        peoples = new List<DummyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1"))
        {
            setDifficulty(1);
        }
        if (Input.GetKey("2"))
        {
            setDifficulty(2);
        }
        if (Input.GetKey("3"))
        {
            setDifficulty(3);
        }
    }

    public void setDifficulty(int _difficulty)
    {
        if (difficulty != _difficulty)
        {
            Debug.Log("Setting dificulty to " + _difficulty.ToString());
            difficulty = _difficulty;
            switch (difficulty)
            {
                case < 2:
                    setAllAttention(0.0f);
                    setlocomotion(false);
                    break;
                case < 3:
                    setAllAttention(1.0f);
                    setlocomotion(false);
                    break;
                case < 4:
                    setAllAttention(1.0f);
                    setlocomotion(true);
                    break;

            }
        }
        
    }

    public void addPeople(DummyScript people)
    {
        peoples.Add(people);
    }

    public void setAllAttention(float attention)
    {
        foreach(DummyScript people in peoples)
        {
            var sources = people.aim.GetComponent<MultiAimConstraint>().data.sourceObjects;
            sources.SetTransform(0, playerpos);
            people.aim.GetComponent<MultiAimConstraint>().data.sourceObjects = sources;

            people.attention = attention;
        }
    }

    public void setlocomotion(bool domove)
    {
        foreach (DummyScript people in peoples)
        {
            people.doWalk = domove;
            people.movePositionTransform = playerpos;
        }
    }

}
