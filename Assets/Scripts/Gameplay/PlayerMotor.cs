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

    [SerializeField]
    private ParticleSystem _BloodParticlePrefab;

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
                    StartCoroutine(SlowMotionEffect());
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(50 * ray.direction.normalized, hit.transform.position, ForceMode.Impulse);
                    var bloodParticle = Instantiate(_BloodParticlePrefab, hit.transform);
                    bloodParticle.transform.position = hit.transform.position;
                    bloodParticle.transform.LookAt(Camera.main.transform);
                    Destroy(bloodParticle, 1f);
                }
            }
//            Debug.Log(hit.transform.tag);
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


    public void WallRun()
    {
        _animationController.SetTrigger("WallRun");
    }


    public IEnumerator SlowMotionEffect()
    {
        StopSlowMotion();
        Time.timeScale = 0.8f;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        StartSlowMotion();
    }

    public void StopSlowMotion()
    {
        Time.timeScale = _startTimescale;
        Time.fixedDeltaTime = _startFixedDeltaTime;
    }
}
