using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : CharacterNode
{
    private GameObject _player { get; set; }
    [SerializeField]
    private Transform _startingPosition;

    private void Start()
    {
        try
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
        catch (System.Exception execption)
        {
            Debug.Log(execption + " player model should be assigned player tag");
        }
        base.TriggerNode();
    }

    protected override void Execute()
    {
        _player.transform.position = new Vector3(_startingPosition.position.x, _player.transform.position.y, _startingPosition.position.z);
        _player.transform.rotation = _startingPosition.rotation;
        _finishedExecution = true;
    }
}
