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
    private float _cameraSpeedMultiplier;
    [SerializeField]
    private float _cameraRotationSpeedMultiplier;
    [SerializeField]
    private Transform _targetTransform;
    [SerializeField]
    private Transform _targetCameraTransform;

    public CharacterAction SelectedCharacterAction;

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
            if (Vector3.Distance(_targetTransform.position, _player.transform.position) <= 0.1f)
            {
                _finishedExecution = true;
            } else
            {
                _player.transform.position = Vector3.MoveTowards(_player.transform.position, _targetTransform.position, _movementSpeed * Time.deltaTime);
                _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, _targetTransform.rotation, _rotationSpeed * Time.deltaTime);

                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, _targetCameraTransform.position, _movementSpeed * _cameraSpeedMultiplier * Time.deltaTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, _targetCameraTransform.rotation, _rotationSpeed * _cameraRotationSpeedMultiplier * Time.deltaTime);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    
    private IEnumerator Vault() // needs rewriting
    {

        _player.GetComponent<PlayerMotor>().Vault();
        for (int i = 0; i < 50; i++)
            yield return new WaitForFixedUpdate();
        _player.GetComponent<PlayerMotor>().StartSlowMotion();
        for (int i = 0; i < 10; i++)
            yield return new WaitForFixedUpdate();
        _player.GetComponent<PlayerMotor>().StopSlowMotion();

    }

    protected override void Execute()
    {
        StartCoroutine(MoveTowardTarget());
        if (SelectedCharacterAction == CharacterAction.Vault)
            StartCoroutine(Vault());
    }

    public enum CharacterAction
    {
        None,
        Vault,

    }
    
}
