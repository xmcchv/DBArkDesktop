using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ShowTitle : MonoBehaviour {
	// Use this for initialization
	void Start () {
		if(new System.Random().Next(1,3)==1)
			WavPlayer.PlayWavSound(Application.streamingAssetsPath + "/VoicePackets/远山/远山_交谈1.wav");
		else
			WavPlayer.PlayWavSound(Application.streamingAssetsPath + "/远山_标题.wav");
		//print(Application.dataPath+ @"/StreamingAssets/VoicePackets/远山/远山_交谈1.wav");
		//Debug.LogWarning("ShowTitle123123123");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
