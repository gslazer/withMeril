using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

using AssemblyCSharp;

public class CBattleRoom : MonoBehaviour {

	bool playerActed1p;
	bool playerActed2p;

	int difficulty;
	ResourceManager m_resourceManager;
	GameController m_gameController;
	InputController m_inputController;

	void Awake(){
		m_resourceManager = new ResourceManager ();
		m_resourceManager.setResourceManager(6,0,5); //maxColorN=10, maxBuffN=0 , maxDebuffN=5;
		m_gameController=new GameController();
		m_gameController.initGameController (6,0,5); //maxColorN=10, maxBuffN=0 , maxDebuffN=5;
		m_inputController = new InputController ();

	}

	// Use this for initialization
	void Start () {
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		m_gameController.onUpdate ();
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
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), m_resourceManager.bg);  
//2p button group(upper user is 2p!!)
		GUI.BeginGroup (new Rect (0, 0, Screen.width, m_gameController.buttonHeight));
		for (int i=0; i< m_gameController.maxButtonN ; i++) {
			Rect buttonRect = new Rect ( m_gameController.buttonSpace * i, 0, m_gameController.buttonWidth, m_gameController.buttonHeight);
			if(m_gameController.clickedFrame[2][i]>m_gameController.frameTime-3)
				buttonRect= new Rect ( m_gameController.buttonSpace * i+40, 0+40, m_gameController.buttonWidth-80, m_gameController.buttonHeight-80);
			GUI.DrawTexture (buttonRect, m_resourceManager.buttonImg[m_gameController.buttonTable[2][i]]);   
			if (GUI.Button (buttonRect, "", GUIStyle.none)) {
				on_click (2,i); 
			}  
		}
		GUI.EndGroup ();
//블록그룹
		GUI.BeginGroup (new Rect (0, Screen.height / 2 - (m_gameController.blockHeight/ 2), Screen.width,m_gameController.blockHeight));
		for (int i=0; i<m_gameController.maxBlockN; i++) {
			Rect blockRect = new Rect ( m_gameController.blockSpace * i, 0, m_gameController.blockWidth, m_gameController.blockHeight);
			if(m_gameController.grayBlock[i])GUI.DrawTexture (blockRect,m_resourceManager.grayBlock);  
			else if(m_gameController.blockTable[i]<=10)GUI.DrawTexture (blockRect,	m_resourceManager.getBlocImg(m_gameController.blockTable[i]));  
			else if(m_gameController.blockTable[i]>=200)GUI.DrawTexture (blockRect,	m_resourceManager.debuffBlockImg[m_gameController.blockTable[i]-200]);  
		}
		GUI.EndGroup ();
		
//1p button group ( downer user is 1p!!)
		GUI.BeginGroup (new Rect (0, Screen.height - (m_gameController.buttonHeight), Screen.width, m_gameController.buttonHeight));
		for (int i=0; i< m_gameController.maxButtonN; i++) {
			Rect buttonRect = new Rect (m_gameController.buttonSpace * i, 0, m_gameController.buttonWidth, m_gameController.buttonHeight);
			if(m_gameController.clickedFrame[1][i]>m_gameController.frameTime-3)
				buttonRect= new Rect ( m_gameController.buttonSpace * i+40, 0+40, m_gameController.buttonWidth-80, m_gameController.buttonHeight-80);
			GUI.DrawTexture (buttonRect, m_resourceManager.buttonImg[m_gameController.buttonTable[1][i]]);  
			if (GUI.Button (buttonRect, "", GUIStyle.none)) {
					on_click (1,i);  
			}  
		}
		GUI.EndGroup ();
		m_gameController.setReDraw(false);
		//Debug.Log("reDrawed!!!");
	}
	void on_click(int player, int buttonIndex)
	{
		m_gameController.setActed (player, buttonIndex);
	}
}
