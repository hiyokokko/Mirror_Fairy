using UnityEngine;
using UnityEngine.UI;
public class RankingManager : MonoBehaviour
{
	[SerializeField] InputField nameInput;
	[SerializeField] InputField passInput;
	[SerializeField] Text EntryText;
	public void Entry()
	{
		string os = TouchOperation.windows ? "Windows" : "Andloid";
		string diff = ((DiffName)SelectManager.diff).ToString();
		string name = nameInput.text;
		string pass = passInput.text;
		if (PlayerPrefs.HasKey(SelectManager.recordDataName[0]) && name != "" && pass != "")
		{
			EntryText.text = FirebaseManager.RankingDataWrite(os, diff, name, pass);
		}
	}
}
public class RankingData
{
	public string pass;
	public int kill;
	public float time;
	public RankingData(string pass, Record record)
	{
		this.pass = pass;
		kill = record.kill;
		time = record.time;
	}
}