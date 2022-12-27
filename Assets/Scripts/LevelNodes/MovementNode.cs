using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementNode : CharacterNode
{
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private Transform _targetTransform;


    private GameObject _player { get; set; }

    private void Awake()
    {
        try
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        } catch (System.Exception execption) {
            Debug.Log(execption + " player model should be assigned player tag");
        }
    }

    private IEnumerator MoveTowardTarget()
    {
        while (!_finishedExecution)
        {
            if (Vector3.Distance(_targetTransform.position, _player.transform.position) <= 0.3f)
            {
                _finishedExecution = true;
            } else
            {
                _player.transform.position = Vector3.MoveTowards(_player.transform.position, _targetTransform.position, _movementSpeed * Time.deltaTime);
                _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, _targetTransform.rotation, _rotationSpeed * Time.deltaTime);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    
    

    protected override void Execute()
    {
        StartCoroutine(MoveTowardTarget());
    }

    
}
