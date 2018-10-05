using UnityEngine;
public class Main : MonoBehaviour
{
	[SerializeField] GameObject[] playerFairies;
	[SerializeField] GameObject[] enemyFairies;
	Vector2 playerPos;
	Vector2 enemyPos;
	public static int kill;
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
		kill = -1;
		time = 0.0f;
		enemyNum = 6;
		enemyArrayPoint = enemyNum * Select.diff;
		enemySpawn = true;
		enemySpawnWait = 5.0f;
		enemySpawnTime = 0.0f;
		gameOver = false;
		PlayerSpawn();
	}
	void Update ()
	{
		time += Time.deltaTime;
		if (enemySpawn) { EnemySpawn(); }
		if (gameOver) { GameOver(); }
	}
	void PlayerSpawn()
	{
		Instantiate(playerFairies[Select.diff], playerPos, Quaternion.identity);
	}
	void EnemySpawn()
	{
		enemySpawnTime += Time.deltaTime;
		if (enemySpawnTime >= enemySpawnWait)
		{
			kill++;
			Instantiate(enemyFairies[enemyArrayPoint + kill], enemyPos, Quaternion.identity);
			enemySpawnTime = 0.0f;
			enemySpawn = false;
		}
	}
	void GameOver()
	{
		Result.resultRecordData = new RecordData(kill = kill >= 0 ? kill : 0, float.Parse(time.ToString("F2")));
		SceneChanger.sceneChange = 3;
		gameOver = false;
	}
}
