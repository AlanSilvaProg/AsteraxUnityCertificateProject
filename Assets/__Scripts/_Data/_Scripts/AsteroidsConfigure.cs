using UnityEngine;

[CreateAssetMenu]
public class AsteroidsConfigure : ScriptableObject
{
    public GameObject[] asteroidVariation;
    public GameObject[] explosionVfxVariation;
    public float asteroidMaxVelocity;
    public int singleAsteroidPoint = 2;
    public int compositionAsteroidPoint = 5;

    public GameObject GetSetupedAsteroid()
    {
        var asteroid = new GameObject();
        asteroid.SetActive(false);
        asteroid.layer = 10;
        // Instantiating the three parts of asteroid
        var partOne = Instantiate(asteroidVariation[Random.Range(0, asteroidVariation.Length)]);
        var partTwo = Instantiate(asteroidVariation[Random.Range(0, asteroidVariation.Length)]);
        var partThree = Instantiate(asteroidVariation[Random.Range(0, asteroidVariation.Length)]);
        partOne.transform.SetParent(asteroid.transform);
        partTwo.transform.SetParent(asteroid.transform);
        partThree.transform.SetParent(asteroid.transform);
        asteroid.AddComponent<Asteroid>().SetupAsteroid();

        return asteroid;
    }

    public GameObject GetRandomExplosionVfx()
    {
        return Instantiate(explosionVfxVariation[Random.Range(0, explosionVfxVariation.Length)]);
    }
    
    public GameObject GetSingleAsteroid()
    {
        return Instantiate(asteroidVariation[Random.Range(0, asteroidVariation.Length)]).AddComponent<Asteroid>().SetupAsteroid(true);
    }
}
