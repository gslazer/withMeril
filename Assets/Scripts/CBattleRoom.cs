using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class CBattleRoom : MonoBehaviour {
	Texture bg;
	Texture[] block_img;
	Texture[] button_img;
	int[] block_list;
	int[] button_list_1p;
	int[] button_list_2p;
	int max_color_n;
	int buttonWidth;
	int buttonHeight;
	int blockWidth;
	int blockHeight;
	int max_blockList;
	int max_buttonList;
	bool playerActed1p;
	bool playerActed2p;
	List<int> usedColors;

	void Awake(){
		max_color_n=10;
		block_img= new Texture[max_color_n+1];
		for(int i=0; i<max_color_n; i++ )
		{
			this.block_img[i] = Resources.Load("ingame/block/block_whole_000"+i) as Texture;
		}
		this.block_img[10] = Resources.Load("ingame/block/block_whole_0010") as Texture;
		
		button_img=new Texture[max_color_n+1];
		for(int i=0; i<max_color_n; i++)
		{
			this.button_img[i]= Resources.Load("ingame/button/button_000"+i) as Texture;
		}
		this.button_img[10] = Resources.Load("ingame/button/button_0010") as Texture;
		
		Debug.Log ("Awaked");
		
		//초기화

		this.bg = Resources.Load ("ingame/gameBg") as Texture;
		max_blockList=9;
		max_buttonList=max_color_n/2; // 버튼 추출은 max_color / 2 개를 양 플레이어가 중복되지 않게 나눠 갖는다.
		block_list=new int[max_blockList];
		button_list_1p=new int[max_buttonList];
		button_list_2p=new int[max_buttonList];
		usedColors = new List<int>();  
		System.Random rnd = new System.Random(); //random key rnd
		//먼저 block 9개를 random하게 채운다. 중복가능!
		for (int i=0; i<9; ++i)  
		{  
		    int resultColor = rnd.Next(0, max_color_n); 
			block_list[i]=resultColor;
			Debug.Log("Random Block Create : "+resultColor);
			if(!usedColors.Contains(resultColor))  usedColors.Add(resultColor);
		}
		//usedColors에서 중복되지 않게 버튼을 뽑는다

//		for(int i=0; i<3; i++)
//		{
//			// 인덱스를 랜덤으로 결정 합니다.  
//		    int index = rnd.Next(0, usedColors.Count);  
//		    int pop = usedColors[index];  
//		    Console.WriteLine(pop);
//			button_list_1p[i]=pop;
//		    // 뽑은것은 리스트에서 삭제하여 다시 나오지 않도록 합니다.  
//		    //usedColors.Remove(pop);  
//		}
//		for(int i=0; i<3; i++)
//		{
//			// 인덱스를 랜덤으로 결정 합니다.  
//		    int index = rnd.Next(0, usedColors.Count);  
//		    int pop = usedColors[index];  
//		    Console.WriteLine(pop);
//			button_list_2p[i]=pop;
//		    // 뽑은것은 리스트에서 삭제하여 다시 나오지 않도록 합니다.  
//		    // usedColors.Remove(pop);  
//		}

		//////////////////////////////////////////////////////////////////////////
		// 현재 버튼 추출의 문제점.
		// 블록의 중복 여부는 상관없음에도 버튼은 중복되지 않게 나와야 한다는 조건 이 있어,
		// 최소 서로 다른 6종류 이상의 블록이 추출되지 않았다면 버튼 구성이 불가능해집니다.
		// 고로, 중복 추출을 막는 코드 일부를 주석처리 해 두었습니다.
		//////////////////////////////////////////////////////////////////////////


		// 버튼 추출은 max_color / 2 개를 양 플레이어가 중복되지 않게 나눠 갖는다.(0~n-1, n~2n-1)
		for(int i=0; i<max_buttonList; i++)
		{
			// 인덱스를 랜덤으로 결정 합니다.  
			button_list_1p[i]=i;
			button_list_2p[i]=i+max_buttonList;
		}
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void Update () 
	{
//		if(playerActed1p && playerActed2p)
//		{
//			generateBlock ();
//		}
	}
	
	
	
	void LateUpdate()
	{
	}
	
	float ratio = 1.0f;  
	
	void OnGUI()  
	{  
	    ratio = Screen.width / 1280.0f;  
		buttonWidth=(int)253;
		buttonHeight=(int)205;
		blockWidth=(int)134;
		blockHeight=(int)217;
		//해상도 1280 768
		
		drawBoard();
	}  
	void drawBoard()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), this.bg);  
		//상단플레이어 버튼그룹

		GUI.BeginGroup(new Rect(0, 0, Screen.width, buttonHeight*ratio));
		for(int i=0; i<max_buttonList; i++)
		{
			int space=(1280-buttonWidth)/(max_buttonList-1);
			Rect buttonRect=new Rect(0+space*i*ratio, 0, buttonWidth*ratio, buttonHeight*ratio);
			GUI.DrawTexture(buttonRect, this.button_img[button_list_1p[i]]);  
			if (GUI.Button(buttonRect, "",GUIStyle.none))  
            {
                on_click(i);  
            }  
		}
      	GUI.EndGroup();
		//
		//블록그룹
		GUI.BeginGroup(new Rect(0, Screen.height/2-(blockHeight*ratio/2), Screen.width, 205));
		for(int i=0; i<9; i++)
		{
			int space=(1280-blockWidth)/8;
			GUI.DrawTexture(new Rect(0+space*i*ratio, 0, blockWidth*ratio, blockHeight*ratio), this.block_img[block_list[i]]);  
		}
		GUI.EndGroup();
		// 
		//하단플레이어 버튼그룹

		GUI.BeginGroup(new Rect(0, Screen.height-(buttonHeight*ratio), Screen.width, buttonHeight*ratio));
		for(int i=0; i<max_buttonList; i++)
		{
			int space=(1280-buttonWidth)/(max_buttonList-1);
			Rect buttonRect=new Rect(0+space*i*ratio, 0, buttonWidth*ratio, buttonHeight*ratio);
			GUI.DrawTexture(buttonRect, this.button_img[button_list_2p[i]]);  
			if (GUI.Button(buttonRect, "",GUIStyle.none))  
            {
                on_click(i+max_buttonList);  
            }  
		}
      	GUI.EndGroup();
		//
		//Debug.Log ("Ratio="+ratio);
		//Debug.Log ("drawed"+blockWidth+" "+blockWidth*ratio);
	}
	void on_click(int buttonIndex)
	{
		int selectedColor;
		if(buttonIndex<max_buttonList){ 
			//if(playerActed1p)return;
			selectedColor=button_list_1p[buttonIndex];
		}
		else {
			//if(playerActed2p)return;
			selectedColor=button_list_2p[buttonIndex-max_buttonList];
		}

//		for(int i=0; i<max_blockList; i++)
//		{
//			if(block_list[i]==selectedColor)
//			{
//				block_list[i]=10;
//			}
//		}
//		if(buttonIndex<3) {
//			button_list_1p[buttonIndex]=10;
//			playerActed1p=true;
//		}
//		else {
//			button_list_2p[buttonIndex-3]=10;
//			playerActed2p=true;
//		}
		//usedColors.Remove(selectedColor);
		if (block_list [0] == selectedColor) {
			System.Random rnd = new System.Random();
			for(int i=0; i<max_blockList-1; i++)
				block_list[i]=block_list[i+1];
			block_list[max_blockList-1]=rnd.Next(0,max_color_n);
		}
	}
//	IEnumerator generateBlock()
//	void generateBlock()
//	{
//		// yield return new WaitForSeconds(0.5f); 
//		// 딜레이를주고싶은데버그가있어주석처리
//		System.Random rnd = new System.Random();
//		for(int i=0; i<9; i++)
//		{
//			if(block_list[i]==10)
//			{
//				int resultColor = rnd.Next(0, max_color_n); 
//				block_list[i]=resultColor;
//				if(!usedColors.Contains(resultColor))usedColors.Add(resultColor);
//			}
//		}
//		for(int i=0; i<3; i++)
//		{
//			if(button_list_1p[i]==10){
//				int index = rnd.Next(0, usedColors.Count);  
//	    		int newButtonColor = usedColors[index];  
//				button_list_1p[i]=newButtonColor;
//			}
//		}
//		for(int i=0; i<3; i++)
//		{
//			if(button_list_2p[i]==10){
//				int index = rnd.Next(0, usedColors.Count);  
//	    		int newButtonColor = usedColors[index];  
//				button_list_2p[i]=newButtonColor;
//			}
//		}
//		//yield return new WaitForSeconds(0.5f); 
//		playerActed1p=false;
//		playerActed2p=false;
//	}
}
