using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterNode : MonoBehaviour
{
    public CharacterNode NextNode;

    protected bool _finishedExecution { get; set; }
    protected abstract void Execute();

    private void Awake()
    {
        _finishedExecution = false;
    }

    public void Update()
    {
        if (_finishedExecution)
        {
            if (NextNode != null)
            {
                NextNode.TriggerNode();
            }
        }
    }

    public void TriggerNode()
    {
        Execute();
    }
}
