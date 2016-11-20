using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;

public class Test : MonoBehaviour {
	// Use this for initialization
	
	void Start () {
		// Set your key
		DeveloperInterface.getInstance ().init ("ekdcaoonjrofaqipsbnffdlnrdafefalbhcmastitqhbffkhdcoqahdilnqrabcsiahoon");
		DeveloperInterface.getInstance ().setAppUserId ("USER_ID");

		// Check ready Video
		// For Test Video: minimumAward = -2
		DeveloperInterface.getInstance().checkCtaAvailability (-2, 0, (Boolean connected, Boolean isAvailable) => {
			Debug.Log("Tapsell: " + connected + " " + isAvailable);
		});
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(50, 50, 100, 100), "Tapsell")){
			 // Show Tapsell Video
			 // For Test Video: minimumAward = -2
			 DeveloperInterface.getInstance().showNewVideo (-2, 0, (Boolean connected, Boolean isAvailable, int award) => {
				Debug.Log("test " + connected + " " + isAvailable + " " + award);
			 });
		}
	}
}
