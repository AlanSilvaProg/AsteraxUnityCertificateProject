using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ScreenSaver))]
public class Asteroid : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigidBody;
    private bool _singleAstreroid;

    public event Action<Vector3> Destroyed;
    
    private void OnEnable()
    {
        StartWork();
    }

    private void OnDisable()
    {
        StopWork();
    }

    private void CheckReferences()
    {
        if (myRigidBody == null)
        {
            myRigidBody = GetComponent<Rigidbody>();
            myRigidBody.useGravity = false;
            myRigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }

    public GameObject SetupAsteroid(bool single = false)
    {
        _singleAstreroid = single;

        CheckReferences();

        if (!_singleAstreroid)
        {
            var partOne = transform.GetChild(0);
            var partTwo = transform.GetChild(1);
            var partThree = transform.GetChild(2);
            //Setuping the position of each part of asteroid
            SetupingPosition(partOne.transform);
            SetupingPosition(partTwo.transform);
            SetupingPosition(partThree.transform);

            void SetupingPosition(Transform transformSetuped)
            {
                var newPosition = Vector3.zero;
                newPosition.x += Random.Range(-1 * transformSetuped.localScale.x / 2, transformSetuped.localScale.x / 2);
                newPosition.x += Random.Range(-1 * transformSetuped.localScale.x / 2, transformSetuped.localScale.x / 2);
                newPosition.y += Random.Range(-1 * transformSetuped.localScale.y / 2, transformSetuped.localScale.y / 2);
                newPosition.y += Random.Range(-1 * transformSetuped.localScale.y / 2, transformSetuped.localScale.y / 2);
                transformSetuped.localPosition = newPosition;
            }
            
            transform.position = new Vector3(Random.Range(0, 2) == 0 ? 5000 : -5000,
                Random.Range(0, 2) == 0 ? 5000 : -5000, 0);
        }
        return gameObject;
    }
    
    private void DestroyAsteroid(bool ignoreScore = false)
    {
        var vfx = GameManager.Instance.explosionVfxPoolSystem.GetObjectFromPool() ?? AsteroidsManager.Instance.Configure.GetRandomExplosionVfx();
        vfx.gameObject.transform.position = transform.position;
        vfx.gameObject.SetActive(true);
        if (_singleAstreroid)
        {
            GameManager.Instance.singleAsteroidPoolSystem.PutObjectIntoPool(gameObject);
            if (!ignoreScore)
                GameManager.Instance.Score += AsteroidsManager.Instance.Configure.singleAsteroidPoint;
        }
        else
        {
            Destroyed?.Invoke(transform.position);
            GameManager.Instance.asteroidCompositionPoolSystem.PutObjectIntoPool(gameObject);
            if (!ignoreScore)
                GameManager.Instance.Score += AsteroidsManager.Instance.Configure.compositionAsteroidPoint;
        }
    }

    private void StartWork()
    {
        CheckReferences();
        transform.SetParent(null);
        var velocity = Vector3.zero;
        myRigidBody.rotation = Quaternion.identity;
        var maxVelocity = AsteroidsManager.Instance.Configure.asteroidMaxVelocity;
        velocity.x = Random.Range(-1 * maxVelocity, maxVelocity);
        velocity.y = Random.Range(-1 * maxVelocity, maxVelocity);
        myRigidBody.velocity = velocity;
        
        myRigidBody.AddTorque(Random.Range(-1 * maxVelocity, maxVelocity),Random.Range(-1 * maxVelocity, maxVelocity),Random.Range(-1 * maxVelocity, maxVelocity));
    }

    private void StopWork()
    {
        myRigidBody.velocity = Vector3.zero;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isActiveAndEnabled) return;
        
        GameObject obj = collision.gameObject;
        if (obj.TryGetComponent(out PlayerGeneral player))
        {
            var playerPos = player.transform.position;
            player.CauseDamage();
            player.PlayerRb.AddForceAtPosition(transform.position - playerPos * 100, playerPos);
            DestroyAsteroid(true);
        }
        else
        {
            DestroyAsteroid();
        }

    }
}
