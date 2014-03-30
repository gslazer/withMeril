using UnityEngine;
using System;
namespace AssemblyCSharp
{

	public class GameController
	{

		public float ratioW = 1.0f;  
		public float ratioH = 1.0f;

		public int windowWidth;
		public int windowHeight;

		public int buttonWidth;
		public int buttonHeight;
		public int blockWidth;
		public int blockHeight;

		public int maxColorN;
		public int maxBuffBlockN;

		public int maxBlockN;
		public int maxButtonN;

		public int[] buttonTable1p;
		public int[] buttonTable2p;
		public int[] blockTable;


		public int buttonSpace;
		public int blockSpace;

		public bool haveToReDraw;

		bool playerActed1p;
		bool playerActed2p;
		public int pushed1p;
		public int pushed2p;

		public GameController ()
		{
		}
		public void initGameController()
		{
			InitDisplay(9,5);
			InitGameData(10,0);
		}
		public void onUpdate(){
			if (IsReshaped())
				haveToReDraw = true;
			if (playerActed1p || playerActed2p) {
				pushProcess ();
				playerActed1p=false;
				playerActed2p=false;
			}
		}

		public void setReDraw(bool haveToReDraw)
		{
			this.haveToReDraw = haveToReDraw;
		}
		public bool IsReshaped(){
			if (Screen.width != windowWidth || Screen.height != windowHeight) {
				InitDisplay (maxBlockN, maxButtonN);
				return true;
			}
			else 
				return false;
		}
		public void setActed(int player, int buttonN)
		{
			if(player==1){
				playerActed1p=true;
				pushed1p=buttonN;
			}
			else if(player==2){
				playerActed2p=true;
				pushed2p=buttonN;
			}
			Debug.Log ("set Acted " + player + "," + buttonN);
		}
		private void generateBlock()
		{
			System.Random rnd = new System.Random ();
			for(int i=0; i<maxBlockN-1; i++)
			{
				blockTable [i] = blockTable[i+1];
			}
			blockTable [maxBlockN - 1] = rnd.Next (0, maxColorN);
		}
		private bool checkSameColor(int selectedColor){
			if (selectedColor == blockTable [0])
					return true;
			else
					return false;
		}
		private void pushProcess(){
			bool check = false;
			if (playerActed1p)
				check = checkSameColor (buttonTable1p [pushed1p]);
			if (check) {
				generateBlock ();
				//추후 스코어링 추가;
			}
			check=false;
			if (playerActed2p)
				check= checkSameColor(buttonTable2p[pushed2p]);	
			if (check){
				generateBlock ();
				//추후 스코어링 추가;
			}
		}
		private void InitDisplay( int maxBlockN, int maxButtonN){
			this.maxBlockN = maxBlockN;
			this.maxButtonN = maxButtonN;
			windowWidth = Screen.width;
			windowHeight = Screen.height;
			ratioW = windowWidth / 1280.0f;  
			ratioH = windowHeight / 768.0f;
			buttonWidth=(int)(253*ratioW);
			buttonHeight=(int)(205*ratioH);
			blockWidth=(int)(134*ratioW);
			blockHeight=(int)(217*ratioH);
			haveToReDraw = true;

			buttonSpace = (windowWidth-buttonWidth) / (maxButtonN - 1);
			blockSpace = (windowWidth-blockWidth) / (maxBlockN - 1);

		}
		private void InitGameData(int maxColorN, int maxBuffBlockN){
			this.maxColorN = maxColorN;
			this.maxBuffBlockN = maxBuffBlockN;

			
			buttonTable1p = new int[maxButtonN];
			buttonTable2p = new int[maxButtonN];
			blockTable= new int[maxBlockN];
			
			for(int i=0; i<maxButtonN; i++)
			{
				buttonTable1p[i]=i;
				buttonTable2p[i]=i+maxButtonN;
			}
			System.Random rnd = new System.Random ();
			for(int i=0; i<maxBlockN; i++)
			{
				blockTable [i] = rnd.Next (0, maxColorN);
			}

			playerActed1p = false;
			playerActed2p = false;
			pushed1p = 0;
			pushed2p = 0;
		}

	}
}