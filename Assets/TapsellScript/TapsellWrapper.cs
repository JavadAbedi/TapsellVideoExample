using UnityEngine;
using System.Collections;
using System.Runtime;
using System;
using System.Collections.Generic;

public class TapsellWrapper : MonoBehaviour{
	Action<String, String> action;

	public void notifyCtaAvailability(String str){
		DeveloperInterface.getInstance().notifyCtaAvailability (str);
	}

	public void notifyVideo(String str){
		DeveloperInterface.getInstance().notifyVideo(str);
	}

	private Boolean setBoolState(Char chr){
		return (chr == 't');
	}
}
