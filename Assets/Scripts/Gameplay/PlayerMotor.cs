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
    private ParticleSystem _magParticle;
    [SerializeField]
    private ParticleSystem _bulletParticle;
    [SerializeField]
    private ParticleSystem _BloodParticlePrefab;
    [SerializeField]
    private Transform _rightWeaponEnd;

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
                    StartCoroutine(ReactOnBulletHit(hit, enemyBehaviour, ray.direction.normalized));
                }
            }
        }
    }

    private IEnumerator ReactOnBulletHit(RaycastHit hit, EnemyBehaviour enemyBehaviour, Vector3 hitDirection)
    {
        AudioManager.Instance.PlayGunShot();
        var bulletParticle = Instantiate(_bulletParticle);
        bulletParticle.transform.position = _rightWeaponEnd.position;
        bulletParticle.transform.LookAt(hit.transform);
        bulletParticle.Play();
        Destroy(bulletParticle, 1f);
        StopSlowMotion();
        Time.timeScale = 0.8f;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        StartSlowMotion();
        enemyBehaviour.DepartFromThisMortalCoil();
        
        hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(50 * hitDirection, hit.transform.position, ForceMode.Impulse);
        var bloodParticle = Instantiate(_BloodParticlePrefab, hit.transform);
        bloodParticle.transform.position = hit.transform.position;
        bloodParticle.transform.LookAt(Camera.main.transform);
        Destroy(bloodParticle, 1f);
        _magParticle.Play();
        _magParticle.Play();
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        AudioManager.Instance.PlayHitSound();
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
