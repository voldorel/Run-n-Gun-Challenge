using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator _animator { get; set; }
    [SerializeField]
    private Rigidbody[] _ragdollParts;
    private float _fadeStart { get; set; }
    [SerializeField]
    private Renderer[] _renderers;
    [SerializeField]
    private Transform _gunTransform;


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
        //_gunTransform.parent = null;
        StartCoroutine(FadeToGray());
    }

    IEnumerator FadeToGray()
    {
        _fadeStart = 0;
        for (int i = 0; i < 100; i++)
        {
            foreach (Renderer renderer in _renderers)
            {
                foreach (Material material in renderer.materials)
                {
                    var grayscale = material.color.grayscale;
                    material.color = Color.Lerp(material.color, new Color(grayscale, grayscale, grayscale), 0.1f);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
