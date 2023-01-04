using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator _animator { get; set; }
    [SerializeField]
    private Rigidbody[] _ragdollParts;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        EnableRagdoll(false);
    }

    private void EnableRagdoll(bool value)
    {
        foreach (var part in _ragdollParts)
        {
            part.isKinematic = !value;
        }
    }

    public void DepartFromThisMortalCoil()
    {
        _animator.enabled = false;
        EnableRagdoll(true);
    }
}
