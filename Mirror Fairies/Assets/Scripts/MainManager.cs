using UnityEngine;
public class MainManager : MonoBehaviour
{
	[SerializeField] GameObject[] playerFairies;
	[SerializeField] GameObject[] enemyFairies;
	Vector2 playerPos;
	Vector2 enemyPos;
	int diff;
	public static int down;
	float time;
	public static int enemyNum;
	int enemyArrayPoint;
	public static bool enemySpawn;
	float enemySpawnWait;
	float enemySpawnTime;
	public static bool gameOver;
	void Start ()
	{
		playerPos = new Vector2(-13.0f, 0.0f);
		enemyPos = new Vector2(13.0f, 0.0f);
		diff = SelectManager.diff;
		down = -1;
		time = 0.0f;
		enemyNum = 6;
		enemyArrayPoint = enemyNum * diff;
		enemySpawn = true;
		enemySpawnWait = 5.0f;
		enemySpawnTime = 0.0f;
		gameOver = false;
		PlayerSpawn();
	}
	void Update ()
	{
		time += Time.deltaTime;
		EnemySpawn();
		GameOver();
	}
	void PlayerSpawn()
	{
		Instantiate(playerFairies[diff], playerPos, Quaternion.identity);
	}
	void EnemySpawn()
	{
		if (enemySpawn)
		{
			enemySpawnTime += Time.deltaTime;
			if (enemySpawnTime >= enemySpawnWait)
			{
				down++;
				Instantiate(enemyFairies[enemyArrayPoint + down], enemyPos, Quaternion.identity);
				enemySpawnTime = 0.0f;
				enemySpawn = false;
			}
		}
	}
	void GameOver()
	{
		if (gameOver)
		{
			ResultManager.result = new Result(down, time);
			SceneChanger.sceneChange = 3;
		}
	}
}
