using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

using AssemblyCSharp;

public class CBattleRoom : MonoBehaviour {

	bool playerActed1p;
	bool playerActed2p;

	ResourceManager m_resourceManager;
	GameController m_gameController;
	InputController m_inputController;

	void Awake(){
		m_resourceManager = new ResourceManager ();
		m_resourceManager.setResourceManager(10,0); //maxColorN=10, maxBuffN=0;
		m_gameController=new GameController();
		m_gameController.initGameController ();
		m_inputController = new InputController ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		m_gameController.onUpdate ();
//		if(playerActed1p && playerActed2p)
//		{
//			generateBlock ();
		//		}
	}
	
	void LateUpdate()
	{
	}

	void OnGUI()  
	{  
		// 전체 보드를 다시 그려야 할 경우만  drawBoard()를 호출하려 하였으나, 여의치 않았음;
		if (m_gameController.haveToReDraw) {
				//Debug.Log ("have to Redraw!");
				drawBoard ();
		}
		// 고로 위와는 무관하게 OnGui시마다 drawBoard() 를 호출.;
		drawBoard ();
	}  
	void drawBoard()
	{	
		unsafe{
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), m_resourceManager.bg);  
	//2p button group(upper user is 2p!!)
			GUI.BeginGroup (new Rect (0, 0, Screen.width, m_gameController.buttonHeight));
			for (int i=0; i< m_gameController.maxButtonN ; i++) {
				Rect buttonRect = new Rect ( m_gameController.buttonSpace * i, 0, m_gameController.buttonWidth, m_gameController.buttonHeight);
				GUI.DrawTexture (buttonRect, m_resourceManager.buttonImg[m_gameController.buttonTable2p[i]]);   
				if (GUI.Button (buttonRect, "", GUIStyle.none)) {
					on_click (2,i); 
				}  
			}
			GUI.EndGroup ();
	//블록그룹
			GUI.BeginGroup (new Rect (0, Screen.height / 2 - (m_gameController.blockHeight/ 2), Screen.width,m_gameController.blockHeight));
			for (int i=0; i<m_gameController.maxBlockN; i++) {
				Rect blockRect = new Rect ( m_gameController.blockSpace * i, 0, m_gameController.buttonWidth, m_gameController.buttonHeight);
				GUI.DrawTexture (blockRect,	m_resourceManager.getBlocImg(m_gameController.blockTable[i]));  
			}
			GUI.EndGroup ();
			
	//1p button group ( downer user is 1p!!)
			GUI.BeginGroup (new Rect (0, Screen.height - (m_gameController.buttonHeight), Screen.width, m_gameController.buttonHeight));
	for (int i=0; i< m_gameController.maxButtonN; i++) {
				Rect buttonRect = new Rect (m_gameController.buttonSpace * i, 0, m_gameController.buttonWidth, m_gameController.buttonHeight);
				GUI.DrawTexture (buttonRect, m_resourceManager.buttonImg[m_gameController.buttonTable1p[i]]);  
				if (GUI.Button (buttonRect, "", GUIStyle.none)) {
						on_click (1,i);  
						;
				}  
			}
			GUI.EndGroup ();
			m_gameController.setReDraw(false);
			//Debug.Log("reDrawed!!!");
		}
	}
	void on_click(int player, int buttonIndex)
	{
		m_gameController.setActed (player, buttonIndex);
//		int selectedColor;
//		if (buttonIndex < m_gameController.maxButtonN) { 
//				//if(playerActed1p)return;
//			selectedColor = m_gameController.buttonTable1p [buttonIndex];
//		} else {
//				//if(playerActed2p)return;
//			selectedColor = m_gameController.buttonTable2p [buttonIndex - m_gameController.maxButtonN];
//		}
//
//		if (m_gameController.blockTable [0] == selectedColor) {
//				System.Random rnd = new System.Random ();
//			for (int i=0; i<m_gameController.maxBlockN-1; i++)
//				m_gameController.blockTable[i] = m_gameController.blockTable [i + 1];
//			m_gameController.blockTable [m_gameController.maxBlockN - 1] = rnd.Next (0, m_gameController.maxColorN);
//		}
	}
}
