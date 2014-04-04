using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

using AssemblyCSharp;

public class CBattleRoom : MonoBehaviour {

	bool playerActed1p;
	bool playerActed2p;

	int difficulty;
	ResourceManager resourceManager;
	GameController gameController;
	InputController inputController;
	DisplayManager displayManager;
	

	void Awake(){
		resourceManager = new ResourceManager ();
		resourceManager.setResourceManager(6,0,5); //maxColorN=10, maxBuffN=0 , maxDebuffN=5;
		gameController=new GameController();
		gameController.initGameController (6,0,5); //maxColorN=10, maxBuffN=0 , maxDebuffN=5;
		inputController = new InputController ();
		displayManager = new DisplayManager();
		displayManager.InitDisplayManager(9,3);

	}

	// Use this for initialization
	void Start () {
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		gameController.onUpdate ();
	}
	
	void LateUpdate()
	{
	}

	void OnGUI()  
	{  
		drawBoard ();
	}  

	void drawBoard()
	{	 
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), resourceManager.bg);  
		for(int player=1; player<=2; player++){
			for(int i=0; i<displayManager.maxButtonN; i++)
			{
				GUI.DrawTexture(displayManager.buttonRect[player][i], resourceManager.buttonImg[gameController.buttonTable[player][i]]);
			}
		}
		/*
		
//2p button group(upper user is 2p!!)
		GUI.BeginGroup (new Rect (0, 0, Screen.width, gameController.buttonHeight));
		for (int i=0; i< gameController.maxButtonN ; i++) {
			Rect buttonRect = new Rect ( gameController.buttonSpace * i, 0, gameController.buttonWidth, gameController.buttonHeight);
			if(gameController.clickedFrame[2][i]>gameController.frameTime-3)
				buttonRect= new Rect ( gameController.buttonSpace * i+40, 0+40, gameController.buttonWidth-80, gameController.buttonHeight-80);
			GUI.DrawTexture (buttonRect, resourceManager.buttonImg[gameController.buttonTable[2][i]]);   
			if (GUI.Button (buttonRect, "", GUIStyle.none)) {
				on_click (2,i); 
			}  
		}
		GUI.EndGroup ();
//블록그룹
		GUI.BeginGroup (new Rect (0, Screen.height / 2 - (gameController.blockHeight/ 2), Screen.width,gameController.blockHeight));
		for (int i=0; i<gameController.maxBlockN; i++) {
			Rect blockRect = new Rect ( gameController.blockSpace * i, 0, gameController.blockWidth, gameController.blockHeight);
			if(gameController.grayBlock[i])GUI.DrawTexture (blockRect,resourceManager.grayBlock);  
			else if(gameController.blockTable[i]<=10)GUI.DrawTexture (blockRect,	resourceManager.getBlocImg(gameController.blockTable[i]));  
			else if(gameController.blockTable[i]>=200)GUI.DrawTexture (blockRect,	resourceManager.debuffBlockImg[gameController.blockTable[i]-200]);  
		}
		GUI.EndGroup ();
		
//1p button group ( downer user is 1p!!)
		GUI.BeginGroup (new Rect (0, Screen.height - (gameController.buttonHeight), Screen.width, gameController.buttonHeight));
		for (int i=0; i< gameController.maxButtonN; i++) {
			Rect buttonRect = new Rect (gameController.buttonSpace * i, 0, gameController.buttonWidth, gameController.buttonHeight);
			if(gameController.clickedFrame[1][i]>gameController.frameTime-3)
				buttonRect= new Rect ( gameController.buttonSpace * i+40, 0+40, gameController.buttonWidth-80, gameController.buttonHeight-80);
			GUI.DrawTexture (buttonRect, resourceManager.buttonImg[gameController.buttonTable[1][i]]);  
			if (GUI.Button (buttonRect, "", GUIStyle.none)) {
					on_click (1,i);  
			}  
		}
		GUI.EndGroup ();
	*/
		gameController.setReDraw(false);
		//Debug.Log("reDrawed!!!");
	}
	void on_click(int player, int buttonIndex)
	{
		gameController.setActed (player, buttonIndex);
	}
}
