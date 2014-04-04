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
		public Texture[] debuffBlockImg;
		public Texture grayBlock;
		public Texture statBar;

		public ResourceManager ()
		{
		}
		public void setResourceManager(int maxColorN, int maxBuffN, int maxDebuffN)
		{
			bg = new Texture ();
			bg=Resources.Load ("ingame/gameBg") as Texture;
			blockImg = new Texture[maxColorN];
			grayBlock = new Texture ();
			grayBlock=Resources.Load ("ingame/block/block_whole_0010") as Texture;
			buttonImg = new Texture[maxColorN];
			debuffBlockImg = new Texture[maxDebuffN];
			for (int i=0; i<maxColorN; i++) {
				blockImg[i] = (Texture) Resources.Load ("ingame/block/block_whole_000" + i) as Texture;
				buttonImg[i]=(Texture) Resources.Load ("ingame/button/button_000"+i) as Texture;
			}
			for (int i=0; i<maxBuffN; i++) {
				buffBlockImg[i]=(Texture) Resources.Load ("ingame/block/block_buff_0"+i)as Texture;
			}
			for (int i=0; i<maxDebuffN; i++) {
				debuffBlockImg[i]=(Texture) Resources.Load ("ingame/block/block_debuff_0"+i)as Texture;
			}
			statBar = new Texture ();
			statBar=Resources.Load ("ingame/bar_status") as Texture;
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