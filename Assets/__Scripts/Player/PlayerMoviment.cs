using System;
using ExtendedClasse;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMoviment : PlayerComponent
{
    [SerializeField] private float playerVelocity = 10f;

    private void Update()
    {
        if (CanDoAction())
        {
            CheckMoviment();
            return;
        }

        playerGeneral.PlayerRb.velocity = Vector2.zero;
    }

    private void CheckMoviment()
    {
        var horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        var verticalInput = CrossPlatformInputManager.GetAxisRaw("Vertical");
        var velocity = playerVelocity * Time.deltaTime;
        
        var movimentForce = new Vector2(1 * horizontalInput, 1 * verticalInput);
        if (movimentForce != Vector2.zero)
        {
            playerGeneral.PlayerRb.AddForce(movimentForce * velocity, ForceMode.Impulse);
        }
    }
}
