using UnityEngine;
using System;
using System.Collections.Generic;

public class DeveloperInterface{
	public static int VideoPlay_TYPE_SKIPPABLE = 31;
	public static int VideoPlay_TYPE_NON_SKIPPABLE = 32;
	private static TapsellObject tapsellObject;
	Dictionary<int, Action<Boolean, Boolean>> actionPool = new Dictionary<int, Action<Boolean, Boolean>>();
	Action<Boolean, Boolean, int> directAdAction;
	int MAX_TYPE_COUNT = 100;
	static DeveloperInterface instance;
	AndroidJavaObject developerInterface;

	public static DeveloperInterface getInstance(){
		if (instance == null) {
			tapsellObject = new TapsellObject ();
			instance = new DeveloperInterface ();
			instance.setJavaObject();
		}
		return instance;
	}

	public void init(string key){
		developerInterface.Call("init", key);
	}
	
	public void setJavaObject(){
		AndroidJavaClass jc = new AndroidJavaClass("ir.tapsell.tapsellvideosdk.developer.DeveloperInterface");
		developerInterface = jc.CallStatic<AndroidJavaObject>("getInstance");
	}

	public void checkCtaAvailability(int minimumAward, int type, Action<Boolean, Boolean> action){
		developerInterface.Call("checkCtaAvailability", minimumAward, type);
		if (actionPool.ContainsKey (minimumAward * MAX_TYPE_COUNT + type))
			actionPool.Remove (minimumAward * MAX_TYPE_COUNT + type);
		actionPool.Add (minimumAward * MAX_TYPE_COUNT + type, action);
	}

	public void showNewVideo(int minimumAward, int type, Action<Boolean, Boolean, int> action){
		developerInterface.Call("showNewVideo", minimumAward, type);
		directAdAction = action;
	}

	public void notifyCtaAvailability(String ans){
		Boolean first, second;
		if (ans.Length == 0) {
			return;
		}
		first = setBoolState (ans [0]);
		second = setBoolState (ans [1]);
		String str = ans.Substring (2);
		Int32 key = Int32.Parse(str);
		if (actionPool.ContainsKey(key)){
			actionPool[key](first, second);
		}
	}

	public void notifyVideo(String ans){
		Boolean first, second;
		if (ans.Length == 0) {
			directAdAction(false, false, 0);
			return;
		}
		first = setBoolState (ans [0]);
		second = setBoolState (ans [1]);
		String str = ans.Substring (2);
		Int32 award = Int32.Parse(str);
		directAdAction(first, second, award);
	}

	private Boolean setBoolState(Char chr){
		return (chr == 't');
	}

	public void setAppUserId(String appUserId) {
		developerInterface.Call ("setAppUserId", appUserId);
	}
}
