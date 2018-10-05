using UnityEngine;
using UnityEngine.UI;
public class Record
{
	public static RecordData recordData;
	public static bool noRecordData;
	public static void RecordDisplay(Text recordKillText, Text recordTimeText)
	{
		recordData = new RecordData(PlayerPrefs.GetInt(((RecordDataName)(Select.diff * 2)).ToString()),
									PlayerPrefs.GetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString()));
		if (noRecordData = recordData.kill == 0 && recordData.time == 0.0f ? true : false)
		{
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
		if (!noRecordData)
		{
			if (Record.recordData.kill < recordData.kill)
			{
				PlayerPrefs.SetInt(((RecordDataName)(Select.diff * 2)).ToString(), recordData.kill);
				PlayerPrefs.SetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString(), recordData.time);
				PlayerPrefs.Save();
				Record.recordData = new RecordData(recordData.kill, recordData.time);
				return true;
			}
			else if (Record.recordData.kill == recordData.kill && Record.recordData.time > recordData.time)
			{
				PlayerPrefs.SetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString(), recordData.time);
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
			PlayerPrefs.SetInt(((RecordDataName)(Select.diff * 2)).ToString(), recordData.kill);
			PlayerPrefs.SetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString(), recordData.time);
			PlayerPrefs.Save();
			Record.recordData = new RecordData(recordData.kill, recordData.time);
			return true;
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
enum RecordDataName
{
	EasyKill,
	EasyTime,
	NormalKill,
	NormalTime,
	HardKill,
	HardTime
}
