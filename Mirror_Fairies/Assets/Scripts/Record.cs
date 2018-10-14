using UnityEngine;
using UnityEngine.UI;
public class Record
{
	public static RecordData recordData;
	public static void RecordDisplay(Text recordKillText, Text recordTimeText)
	{
		if (PlayerPrefs.HasKey(((DiffName)(Select.diff * 2)).ToString() + "Kill"))
		{
			recordData = new RecordData(PlayerPrefs.GetInt(((DiffName)(Select.diff * 2)).ToString() + "Kill"),
										PlayerPrefs.GetFloat(((DiffName)(Select.diff * 2)).ToString() + "Time"));
			recordKillText.text = "RECORD KILL _ " + recordData.kill;
			recordTimeText.text = "RECORD TIME _ " + recordData.time;
		}
		else
		{
			recordKillText.text = "RECORD KILL _ NO DATA";
			recordTimeText.text = "RECORD TIME _ NO DATA";
		}
	}
	public static bool RecordUpdate(RecordData recordData)
	{
		if (PlayerPrefs.HasKey(((DiffName)(Select.diff * 2)).ToString() + "Kill"))
		{
			if (Record.recordData.kill < recordData.kill)
			{
				PlayerPrefs.SetInt(((DiffName)(Select.diff * 2)).ToString() + "Kill", recordData.kill);
				PlayerPrefs.SetFloat(((DiffName)(Select.diff * 2)).ToString() + "Time", recordData.time);
				PlayerPrefs.Save();
				Record.recordData = new RecordData(recordData.kill, recordData.time);
				return true;
			}
			else if (Record.recordData.kill == recordData.kill && Record.recordData.time > recordData.time)
			{
				PlayerPrefs.SetFloat(((DiffName)(Select.diff * 2)).ToString() + "Time", recordData.time);
				PlayerPrefs.Save();
				Record.recordData = new RecordData(recordData.kill, recordData.time);
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (recordData.kill >= 1)
		{
			PlayerPrefs.SetInt(((DiffName)(Select.diff * 2)).ToString() + "Kill", recordData.kill);
			PlayerPrefs.SetFloat(((DiffName)(Select.diff * 2)).ToString() + "Time", recordData.time);
			PlayerPrefs.Save();
			Record.recordData = new RecordData(recordData.kill, recordData.time);
			return true;
		}
		else
		{
			return false;
		}
	}
}
public class RecordData
{
	public int kill;
	public float time;
	public RecordData(int kill, float time)
	{
		this.kill = kill;
		this.time = time;
	}
}
