using System;
using UnityEngine;
namespace AssemblyCSharp
{

	public class ResourceManager
	{
		public Texture bg;
		public Texture[] blockImg;
		public Texture[] buttonImg;
		public Texture[] buffBlockImg; //버프버튼사용시추가

		public ResourceManager ()
		{
		}
		public void setResourceManager(int maxColorN, int maxBuffN)
		{
			bg = new Texture ();
			if(bg=Resources.Load ("ingame/gameBg") as Texture);
			else Debug.Log ("bg Load Faile!");
			blockImg = new Texture[maxColorN];
			buttonImg = new Texture[maxColorN];
			for (int i=0; i<maxColorN; i++) {
				if(blockImg[i] = Resources.Load ("ingame/block/block_whole_000" + i) as Texture);
				else Debug.Log ("blockImg Load Failed! : "+i);
				if(	buttonImg[i]=(Texture) Resources.Load ("ingame/button/button_000"+i) as Texture);
				else Debug.Log ("buttonImg Load Failed! : "+i);
			}
			for (int i=0; i<maxBuffN; i++) {
				if(buffBlockImg[i]=(Texture) Resources.Load ("ingame/block/block_buff_0"+i)as Texture);
				else Debug.Log ("buffBlockImg Load Failed! : "+i);
			}
		}

		public Texture getBg(){
			return bg;
		}
		public Texture getBlocImg(int colorN){
			return blockImg [colorN];
		}
		public Texture getButtonImg(int colorN){
			return buttonImg[colorN];
		}
		public Texture getBuffBlockImg(int buffN){
			return buffBlockImg [buffN];
		}

	}
}