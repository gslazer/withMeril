using UnityEngine;
using System.Collections;

public class CMainTitle : MonoBehaviour {
	Texture tGamePlay;  
	Texture tItems;
	Texture tSetting;
	Texture tGiftbox;
	Texture tFriends;
	GameObject battleroom;  
	float ratio=1.0f;
	// Use this for initialization
	void Start () {
		//resources Load;
		this.tGamePlay = Resources.Load("main/main_gameplay") as Texture;  
		this.tItems = Resources.Load ("main/main_items") as Texture;
		this.tSetting = Resources.Load ("main/main_setting")as Texture;
		this.tGiftbox = Resources.Load ("main/main_giftbox")as Texture;
		this.tFriends = Resources.Load ("main/main_friends")as Texture;
		this.battleroom = GameObject.Find("BattleRoom");  
		this.battleroom.SetActive(false);  
		ratio = Screen.width / 1280.0f;  
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))  
		{
			// 마우스 버튼이 눌리면 battleroom 오브젝트를 활성화 시키고,
			// 현재 오브젝트를 비활성화 시켜서 room화면으로 전환시켜 줍니다.
			
			// 게임오브젝트가 활성화 되면 거기에 붙은 스크립트의 Start()함수가 호출되면서 작동이 시작되고,
			// 비활성화 되면 스크립트 역시 작동을 멈춥니다.
			
			// 따라서 각각의 화면들을 오브젝트로 만들고 그에따른 스크립트를 붙여주면
			// 각 화면에 따른 코드들을 여러 파일에 분리하여 관리할 수 있게 됩니다.
			this.battleroom.SetActive(true);
			gameObject.SetActive(false);  
		}  
	}
	void OnGUI()  
	{
		// 그냥 그립니다. 화면 크기대로~  
		GUI.DrawTexture(new Rect(0,0,450*ratio,450*ratio), this.tGamePlay);  
		GUI.DrawTexture(new Rect((Screen.width/2-225*ratio),(Screen.height/2-226*ratio),450*ratio,451*ratio), this.tItems);  
		GUI.DrawTexture(new Rect((Screen.width - 450*ratio) ,0,450*ratio,452*ratio), this.tSetting);  
		GUI.DrawTexture(new Rect(0,(Screen.height-453*ratio),450*ratio,453*ratio), this.tGiftbox);  
		GUI.DrawTexture(new Rect((Screen.width - 450*ratio),(Screen.height-454*ratio),450*ratio,454*ratio), this.tFriends);  
	}  
}
