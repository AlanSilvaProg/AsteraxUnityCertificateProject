using System.Threading.Tasks;
using UnityEngine;
#if  UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(ScreenSaver))]
public class PlayerGeneral : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Collider playerCol;
    [SerializeField] private GameObject[] spaceshipParts;

    [Header("After damage")]
    [Tooltip("After hitted by asteroid, player will be invulnerable during this time in milliseconds")]
    [SerializeField]
    private int millisecondsInvisible = 4500;

    public Rigidbody PlayerRb => playerRb;
    public bool FreeToDoAction => GameManager.Instance.LifeRemaining > 0 && GameManager.Instance.GameState == GameStates.RUNNING;

    private void OnValidate()
    {
        if (playerRb == null)
            playerRb = GetComponent<Rigidbody>();
        if (playerCol == null)
            playerCol = GetComponent<Collider>();
    }

    public async void CauseDamage()
    {
        if (!playerCol.enabled) return;
        GameManager.Instance.LifeRemaining--;
        SpawnHitVfx();
        if (FreeToDoAction)
        {
            await SafeDuringTime();
        }
        else
        {
            GameManager.Instance.CallGameOver();
        }
    }
    
    private void SpawnHitVfx()
    {
        var vfx = GameManager.Instance.playerHitVfxPoolSystem.GetObjectFromPool();
        vfx.transform.SetParent(transform);
        vfx.transform.localPosition = Vector3.zero;
        vfx.SetActive(true);
    }

    private async Task SafeDuringTime()
    {
        playerCol.enabled = false;
        var count = 0;
        do
        {
            await Task.Delay(250);
#if  UNITY_EDITOR
            if (!EditorApplication.isPlaying) return;
#endif
            if (GameManager.InGameCancellationToken.IsCancellationRequested) break;
            foreach (var part in spaceshipParts)
            {
                part.SetActive(!part.activeSelf);
            }
            count += 250;
        } while (count < millisecondsInvisible);

        foreach (var part in spaceshipParts)
        {
            part.SetActive(true);
            SpawnHitVfx();
        }

        playerCol.enabled = true;
    }
}