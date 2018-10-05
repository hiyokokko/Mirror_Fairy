using UnityEngine;
using UnityEngine.UI;
public class Ranking : MonoBehaviour
{
	[SerializeField] InputField nameInput;
	[SerializeField] InputField passInput;
	[SerializeField] Text EntryText;
	public void Entry()
	{
		string os = TouchOperation.windows ? "Windows" : "Andloid";
		string diff = ((DiffName)Select.diff).ToString();
		string name = nameInput.text;
		string pass = passInput.text;
		if (Record.recordData.kill >= 1 && name != "" && pass != "")
		{
			FirebaseManager.RankingDataWrite(os, diff, name, pass).ContinueWith(task =>
			{
				EntryText.text = task.Result;
			});
		}
	}
}
