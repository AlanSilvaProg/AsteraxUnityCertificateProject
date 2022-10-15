using System;
using UnityEngine;

[RequireComponent(typeof(ScreenSaver))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet Configuration")]
    [SerializeField] private Rigidbody bulletRb;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float lifeTime = 2f;
    
    [Header("Trail")]
    [SerializeField] private GameObject bulletTrail;
    private float timeAlive = 0;

    private ScreenSaver _screenSaver;
    private bool _wrapped = false;

    private void Awake()
    {
        if (!_screenSaver)
            _screenSaver = GetComponent<ScreenSaver>();
        _screenSaver.Wrapped += () =>
        {
            PlayerProgressRegistry.bulletWraps++;
            _wrapped = true;
        };
    }

    private void OnEnable()
    {
        timeAlive = 0;
        DispareBullet();
    }

    private void OnDisable()
    {
        bulletRb.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (isActiveAndEnabled)
            CountTimeAlive();
    }

    private void CountTimeAlive()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifeTime)
        {
            DestroyBullet();
        }
    }

    private void DispareBullet()
    {
        _wrapped = false;
        bulletTrail.transform.SetParent(null);
        bulletTrail.SetActive(true);
        bulletRb.AddForce(transform.up * bulletVelocity, ForceMode.Impulse);
        PlayerProgressRegistry.bulletsFired++;
    }

    private void DestroyBullet()
    {
        bulletTrail.SetActive(false);
        bulletTrail.transform.SetParent(transform);
        bulletTrail.transform.localPosition = Vector3.zero;
        GameManager.Instance.bulletPoolSystem.PutObjectIntoPool(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;
        if (_wrapped)
            PlayerProgressRegistry.bulletWrapsWithHit++;
        DestroyBullet();
    }
}
