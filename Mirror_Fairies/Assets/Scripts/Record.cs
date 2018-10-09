using UnityEngine;
using UnityEngine.UI;
public class Record
{
	public static RecordData recordData;
	public static void RecordDisplay(Text recordKillText, Text recordTimeText)
	{
		if (PlayerPrefs.HasKey(((RecordDataName)(Select.diff * 2)).ToString()))
		{
			recordData = new RecordData(PlayerPrefs.GetInt(((RecordDataName)(Select.diff * 2)).ToString()),
										PlayerPrefs.GetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString()));
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
		if (PlayerPrefs.HasKey(((RecordDataName)(Select.diff * 2)).ToString()))
		{
			if (Record.recordData.kill < recordData.kill)
			{
				PlayerPrefs.SetInt(((RecordDataName)(Select.diff * 2)).ToString(), (int)recordData.kill);
				PlayerPrefs.SetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString(), (float)recordData.time);
				PlayerPrefs.Save();
				Record.recordData = new RecordData(recordData.kill, recordData.time);
				return true;
			}
			else if (Record.recordData.kill == recordData.kill && Record.recordData.time > recordData.time)
			{
				PlayerPrefs.SetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString(), (float)recordData.time);
				PlayerPrefs.Save();
				Record.recordData = new RecordData(recordData.kill, recordData.time);
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			PlayerPrefs.SetInt(((RecordDataName)(Select.diff * 2)).ToString(), (int)recordData.kill);
			PlayerPrefs.SetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString(), (float)recordData.time);
			PlayerPrefs.Save();
			Record.recordData = new RecordData(recordData.kill, recordData.time);
			return true;
		}
	}
}
public class RecordData
{
	public long kill;
	public double time;
	public RecordData(long kill, double time)
	{
		this.kill = kill;
		this.time = time;
	}
}
enum RecordDataName
{
	EasyKill,
	EasyTime,
	NormalKill,
	NormalTime,
	HardKill,
	HardTime
}
