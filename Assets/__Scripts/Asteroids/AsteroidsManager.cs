using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
#if  UNITY_EDITOR
using UnityEditor;
#endif

public class AsteroidsManager : SingletonMonoBehaviour<AsteroidsManager>
{
    [SerializeField] private AsteroidsConfigure asteroidData;
    [SerializeField] private int spawningRate = 3;
    [SerializeField] private int maxAsteroidsOnScreen = 4;
    private int counter = 0;
    private int asteroidCount = 0;

    public AsteroidsConfigure Configure => asteroidData;
    public static event Action AsteroidSpawned;

    private bool CanRun => GameManager.Instance.GameState == GameStates.RUNNING;
    
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => CanRun);
        StartSequence();
    }

    private async void StartSequence()
    {
        while (asteroidCount >= maxAsteroidsOnScreen * (GameManager.Instance.CurrentStage >= GameManager.Instance.GameSettings.MaxLevelMultiplier ? 
                   GameManager.Instance.GameSettings.MaxLevelMultiplier : GameManager.Instance.CurrentStage))
        {
            counter = 0;
            await Task.Yield();
        }
        
        while (!CanRun)
        {
#if  UNITY_EDITOR
            if (!EditorApplication.isPlaying) return;
#endif
            await Task.Yield();
        }
        await Task.Delay(1000 * spawningRate);
        
        while (!CanRun)
        {
#if  UNITY_EDITOR
            if (!EditorApplication.isPlaying) return;
#endif
            await Task.Yield();
        }
#if  UNITY_EDITOR
        if (!EditorApplication.isPlaying) return;
#endif
        if (GameManager.InGameCancellationToken.IsCancellationRequested) return;
        CheckSequence();
        StartSequence();
    }

    private void CheckSequence()
    {
        counter++;
        if (counter >= spawningRate)
        {
            counter = 0;
            SpawnNewAsteroid();
        }
    }

    private void SpawnNewAsteroid()
    {
        var asteroid = GameManager.Instance.asteroidCompositionPoolSystem.GetObjectFromPool<Asteroid>();
        if (asteroid == null)
        {
            asteroid = asteroidData.GetSetupedAsteroid().GetComponent<Asteroid>();
            asteroid.Destroyed += AsteroidDestroyed;
        }
        else
            asteroid.SetupAsteroid();

        asteroidCount++;
        asteroid.gameObject.SetActive(true);
        AsteroidSpawned?.Invoke();
    }

    private void AsteroidDestroyed(Vector3 position)
    {
        asteroidCount--;
        var asteroidOne = GameManager.Instance.singleAsteroidPoolSystem.GetObjectFromPool<Asteroid>() ??
                          asteroidData.GetSingleAsteroid().GetComponent<Asteroid>();
        var asteroidTwo = GameManager.Instance.singleAsteroidPoolSystem.GetObjectFromPool<Asteroid>() ??
                          asteroidData.GetSingleAsteroid().GetComponent<Asteroid>();
        var asteroidThree = GameManager.Instance.singleAsteroidPoolSystem.GetObjectFromPool<Asteroid>() ??
                          asteroidData.GetSingleAsteroid().GetComponent<Asteroid>();
        
        SetupingPosition(asteroidOne.transform, position);
        SetupingPosition(asteroidTwo.transform, position);
        SetupingPosition(asteroidThree.transform, position);
    }
    
    private void SetupingPosition(Transform transformElement, Vector3 asteroidCenter)
    {
        var newPosition = Vector3.zero;
        newPosition.x = Random.Range(asteroidCenter.x + -1 * transformElement.localScale.x / 2, asteroidCenter.x + transformElement.localScale.x / 2);
        newPosition.y = Random.Range(asteroidCenter.y + -1 * transformElement.localScale.y / 2, asteroidCenter.y + transformElement.localScale.y / 2);
        transformElement.position = newPosition;
        transformElement.gameObject.SetActive(true);
    }
}