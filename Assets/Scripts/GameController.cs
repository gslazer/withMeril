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
		public int maxBuffN;
		public int maxDebuffN;

		public int maxBlockN;
		public int maxButtonN;

		public int[][] buttonTable;
		public int[] blockTable;


		public int buttonSpace;
		public int blockSpace;

		public bool haveToReDraw;
		public bool[] grayBlock;

		bool playerActed1p;
		bool playerActed2p;
		public int pushed1p;
		public int pushed2p;

		public int difficulty;
		public float gameTime;
		public float lastHitTime;
		public int frameTime;
		public int[][] clickedFrame;

		public GameController ()
		{
		}
		public void initGameController(int maxColorN, int maxBuffN, int maxDebuffN)
		{
			InitDisplay(9,maxColorN/2);
			InitGameData(maxColorN,maxBuffN,maxDebuffN);
			gameTime = Time.fixedTime;
			lastHitTime = gameTime;
			frameTime = 0;
		}
		public void onUpdate(){
			if(gameTime< Time.fixedTime+0.05f){
				gameTime = Time.fixedTime;
				frameTime++;
				if (IsReshaped())
					haveToReDraw = true;
				if (playerActed1p || playerActed2p) {
					pushProcess ();
					playerActed1p=false;
					playerActed2p=false;
				}
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
		}
		private void generateDebuff()
		{
			System.Random rnd = new System.Random ();
			for(int i=0; i<maxBlockN-1; i++)
			{
				blockTable [i] = blockTable[i+1];
			}
			blockTable [maxBlockN - 1] = 200+rnd.Next(0,maxDebuffN) ; //여기에 디버프 들어가야함.
		}
		private void generateBlock()
		{
			System.Random rnd = new System.Random ();
			for(int i=0; i<maxBlockN-1; i++)
			{
				blockTable [i] = blockTable[i+1];
				grayBlock[i]=grayBlock[i+1];
			}
			grayBlock[0]=false;
			grayBlock [maxBlockN - 1] = grayBlock [maxBlockN];
			grayBlock [maxBlockN] = false;
			blockTable [maxBlockN - 1] = rnd.Next (0, maxColorN);
		}
		private bool checkSameColor(int selectedColor){
			if (selectedColor == blockTable [0])
					return true;
			else
					return false;
		}
		private bool checkInTime(){
			if((gameTime-lastHitTime)<1.8f)
				return true;
			else
				return false;
		}

		void debuffReverse ()
		{
			int temp;
			for (int i=1; i<maxBlockN/2; i++) {
				temp=blockTable[i];
				blockTable[i]=blockTable[maxBlockN-i];
				blockTable[maxBlockN-1]=temp;
			}
		}

		void debuffButtonReverse ()
		{
			int temp;
			for(int player=1; player<=2; player++){
				for (int i=0; i<maxButtonN/2; i++) {
					temp=buttonTable[player][i];
					buttonTable[player][i]=buttonTable[player][maxButtonN-i-1];
					buttonTable[player][maxButtonN-i-1]=temp;
				}
			}
			Debug.Log ("Button Reversed!");
		}

		void debuffDoubleColor ()
		{
		}

		void debuffButtonChange ()
		{
			int temp;
			for (int i=0; i<maxButtonN; i++) {
				temp=buttonTable[1][i];
				buttonTable[1][i]=buttonTable[2][i];
				buttonTable[2][i]=temp;
			}
		}

		void debuffGray ()
		{
			for (int i=1; i<=maxBlockN; i++)
				grayBlock [i] = true;
		}

		void deBuffProcess (int deBuffN)
		{
			switch(deBuffN)
			{
				case 0: 
					debuffButtonReverse ();
					break;
				case 1:
					debuffButtonChange();
					break;
				case 2:
					debuffDoubleColor();
					break;
				case 3:
					debuffGray();
					break;
				case 4:
					debuffReverse();
					break;
			}
		}

		private void pushProcess(){
			bool check = false;
			check = checkInTime ();

			if (playerActed1p)
				clickedFrame [1] [pushed1p] = frameTime;
			if (playerActed2p)
				clickedFrame [2] [pushed2p] = frameTime;

			if (blockTable [0] >= 100) {
				deBuffProcess(blockTable[0]-200);
			}
			else{
				if (playerActed1p)	check = check & checkSameColor (buttonTable[1][pushed1p]);
				if (playerActed2p)	check = check & checkSameColor(buttonTable[2][pushed2p]);	
			}
			if (check) {
				generateBlock ();
				//추후 스코어링 추가;
			}
			else
				generateDebuff();
			lastHitTime=gameTime;
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
		private void InitGameData(int maxColorN, int maxBuffN, int maxDebuffN){
			this.maxColorN = maxColorN;
			this.maxBuffN = maxBuffN;
			this.maxDebuffN = maxDebuffN;

			buttonTable = new int[3][];
			buttonTable[1] = new int[maxButtonN];
			buttonTable[2] = new int[maxButtonN];
			clickedFrame = new int[3][];
			clickedFrame [1] = new int[maxButtonN];
			clickedFrame [2] = new int[maxButtonN];
			blockTable= new int[maxBlockN];
			grayBlock = new bool[maxBlockN+1];
			
			for(int i=0; i<maxButtonN; i++)
			{
				buttonTable[1][i]=i;
				buttonTable[2][i]=i+maxButtonN;
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