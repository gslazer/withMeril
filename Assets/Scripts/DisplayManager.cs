using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class DisplayManager
	{
		public int windowWidth;
		public int windowHeight;
		public float ratioW;
		public float ratioH;
		public int buttonWidth;
		public int buttonHeight;
		public int blockWidth;
		public int blockHeight;
		public int buttonSpace;
		public int blockSpace;
		public int[] buttonY;
		public int[] buttonX;
		public int blockX;
		public int blockY;
		
		public int maxBlockN;
		public int maxButtonN;
		public Rect[][] buttonRect;
		public Rect[][] buttonPushedRect;
		public Rect[] blockRect;
		
		public DisplayManager ()
		{
		}
		
		public void InitDisplayManager( int maxBlockN, int maxButtonN){
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
			buttonSpace = (int) (255*ratioW);
			buttonY= new int[3];
			buttonY[1]=(int)(594*ratioH);
			buttonY[2]=(int)(2*ratioH);
			buttonX=new int[3];
			buttonX[1]=(int)(3*ratioW);
			buttonX[2]=(int)(3*ratioW);
			blockX=(int)(6*ratioW);
			blockY=(int)(301*ratioH);
			blockSpace =(int) (117*ratioW);
			
			buttonRect=new Rect[3][];
			buttonRect[1]=new Rect[maxButtonN];
			buttonRect[2]=new Rect[maxButtonN];
			buttonPushedRect=new Rect[3][];
			buttonPushedRect[1]=new Rect[maxButtonN];
			buttonPushedRect[2]=new Rect[maxButtonN];
			blockRect=new Rect[maxBlockN];
		}
		private void SetDisplay(int maxBlockN, int maxButtonN){
			for (int i=0; i< maxButtonN ; i++) {
				buttonRect[2][i] = new Rect ( buttonX[2]+buttonSpace * i, buttonY[2], buttonWidth, buttonHeight);
				buttonPushedRect[2][i]= new Rect ( buttonX[2]+buttonSpace * i+40, buttonY[2]+40, buttonWidth-80, buttonHeight-80);
				buttonRect[1][i] = new Rect ( buttonX[1]+buttonSpace * i, buttonY[1], buttonWidth, buttonHeight);
				buttonPushedRect[1][i]= new Rect ( buttonX[1]+buttonSpace * i+40, buttonY[1]+40, buttonWidth-80, buttonHeight-80);
			}
			for (int i=0; i<maxBlockN; i++) {
				blockRect[i] = new Rect ( blockX+ blockSpace * i, blockY, blockWidth, blockHeight);
			}
		}
	}
}