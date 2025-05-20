using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatior : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnerParent;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Run()
    {
        for(int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Animator>();
            runnerAnimator.Play("Run");
        }
    }

    public void Idle()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Animator>();
            runnerAnimator.Play("Idle");
        }
    }
}
