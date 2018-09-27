using UnityEngine;
/// <summary>
/// メインシーンの管理。
/// </summary>
public class MainManager : MonoBehaviour
{
	[SerializeField] GameObject[] playerFairies;
	[SerializeField] GameObject[] enemyFairies;
	Vector2 playerPos;
	Vector2 enemyPos;
	int diff;
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
		diff = SelectManager.diff;
		kill = -1;
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
		if (enemySpawn) { EnemySpawn(); }
		if (gameOver) { GameOver(); }
	}
	/// <summary>
	/// プレイヤーをスポーンさせる。
	/// </summary>
	void PlayerSpawn()
	{
		Instantiate(playerFairies[diff], playerPos, Quaternion.identity);
	}
	/// <summary>
	/// 敵キャラをスポーンさせる。
	/// </summary>
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
	/// <summary>
	/// ゲームを終了させる。
	/// </summary>
	void GameOver()
	{
		ResultManager.result = new Result(kill, float.Parse(time.ToString("F2")));
		SceneChanger.sceneChange = 3;
	}
}
