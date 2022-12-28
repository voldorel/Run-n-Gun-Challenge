using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Animator _animationController;

    public float SlowMotionTimescale;

    private float _startTimescale { get; set; }
    private float _startFixedDeltaTime { get; set; }

    private void Start()
    {
        _startTimescale = Time.timeScale;
        _startFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animationController.SetTrigger("Dive");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _animationController.SetTrigger("Vault");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartSlowMotion();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StopSlowMotion();
        }
    }

    public void StartSlowMotion()
    {
        Time.timeScale = SlowMotionTimescale;
        Time.fixedDeltaTime = _startFixedDeltaTime * SlowMotionTimescale;
    }

    public void Vault() //only for test
    {
        _animationController.SetTrigger("Vault");
    }

    public void StopSlowMotion()
    {
        Time.timeScale = _startTimescale;
        Time.fixedDeltaTime = _startFixedDeltaTime;
    }
}
