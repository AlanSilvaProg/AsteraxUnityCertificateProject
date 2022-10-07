using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerTurret : PlayerComponent
{
    [SerializeField] private Transform bulletSpawnPoint;
    
    private void Update()
    {
        if (!CanDoAction()) return;
        
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            DispareBullet();
        }
        LookToAimPosition();
    }

    /// <summary>
    /// Instantiate a bullet from pooling system on spawn bullet point
    /// </summary>
    private void DispareBullet()
    {
        var bulletTransform = GameManager.Instance.bulletPoolSystem.GetObjectFromPool<Bullet>().transform;
        bulletTransform.SetParent(bulletSpawnPoint);
        bulletTransform.localPosition = Vector3.zero;
        bulletTransform.rotation = transform.rotation;
        bulletTransform.SetParent(null);
        bulletTransform.gameObject.SetActive(true);
    }

    /// <summary>
    /// Look to the mouse position based on the Cross Platform input system 
    /// </summary>
    private void LookToAimPosition()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
