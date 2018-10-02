using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
public class FirebaseManager : MonoBehaviour
{
	public static DatabaseReference databaseReference;
	void Start()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://mirrorfairies.firebaseio.com/");
		databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
	}
}
