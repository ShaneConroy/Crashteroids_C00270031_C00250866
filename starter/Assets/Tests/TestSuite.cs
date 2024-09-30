using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    private Game game;


    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();

    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game.gameObject);
    }

    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float initialYPos = asteroid.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = game.GetShip().transform.position;
        yield return new WaitForSeconds(0.1f);
        Assert.True(game.isGameOver);
    }

    //1
    [Test]
    public void NewGameRestartsGame()
    {
        //2
        game.isGameOver = true;
        game.NewGame();
        //3
        Assert.False(game.isGameOver);
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]

    public IEnumerator StartingNewGameSetsScoreToZero()
    {
        game.score = 1;
        game.NewGame();


        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(game.score, 0);
    }

    [UnityTest]

    public IEnumerator ShipMovesCorrectly()
    {

        Ship ship = game.GetShip();
        Vector3 pos = ship.transform.position;
        ship.MoveLeft();

        Assert.Greater(pos.x, ship.transform.position.x);

        pos = ship.transform.position;
        ship.MoveRight();

        Assert.Less(pos.x, ship.transform.position.x);
        yield return new WaitForSeconds(0.1f);

    }

    [UnityTest]
    public IEnumerator SmallAsteroidSpawn()
    {
        GameObject laser = game.GetShip().SpawnLaser();
        GameObject largeAsteroid = game.GetSpawner().SpawnAsteroid();

        largeAsteroid.transform.position = Vector3.zero;
        laser.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.1f);

        GameObject spawnedSmallAsteroid = GameObject.Find("Small Asteroid(Clone)");

        Assert.IsNotNull(spawnedSmallAsteroid);


    }

    [UnityTest]

    public IEnumerator PowerUpFall()
    {
        int speed = 2;
        GameObject testObject = new GameObject();
        testObject.transform.position = Vector3.zero;
        testObject.transform.Translate(Vector3.down * Time.deltaTime * speed);

        Assert.Less(testObject.transform.position.y, 0);
        yield return new WaitForSeconds(0.1f);
    }
    [UnityTest]
    public IEnumerator PowerUpSpawn()
    {
        GameObject powerUp = game.GetSpawner().SpawnPowerUp();

        Assert.IsNotNull(powerUp);

        yield return new WaitForSeconds(0.1f);
    }
    

}
