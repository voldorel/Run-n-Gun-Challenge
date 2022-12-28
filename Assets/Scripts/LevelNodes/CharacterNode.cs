using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterNode : MonoBehaviour
{
    public CharacterNode NextNode;

    protected bool _finishedExecution { get; set; }
    protected bool _calledNext { get; set; }
    protected abstract void Execute();

    private void Awake()
    {
        _finishedExecution = false;
        _calledNext = false;
    }

    public void Update()
    {
        if (_finishedExecution && !_calledNext)
        {
            if (NextNode != null)
            {
                NextNode.TriggerNode();
                _finishedExecution = false;
                _calledNext = true;
            }
        }
    }
    public void OnDrawGizmos()
    {
        if (NextNode != null)
        {
            //Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, NextNode.transform.position);
        }
    }

    public void TriggerNode()
    {
        Execute();
    }
}
