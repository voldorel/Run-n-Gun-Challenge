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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootBullet();
        }
    }

    public void ShootBullet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20))
        {
            if (hit.transform.tag.Equals("Enemy"))
            {
                EnemyBehaviour enemyBehaviour = hit.transform.GetComponentInParent<EnemyBehaviour>();
                if (enemyBehaviour != null)
                {
                    enemyBehaviour.DepartFromThisMortalCoil();
                }
            }
            Debug.Log(hit.transform.tag);
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
