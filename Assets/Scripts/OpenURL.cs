using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour {

	public void OpenWeb (int whichWeb) 
	{ 
		if (whichWeb == 0) {
			Application.OpenURL ("https://twitter.com/pudding_games_");
		}
		else if (whichWeb == 1) {
			Application.OpenURL ("https://www.facebook.com/Pudding-Games-1944780155789174/"); 
		}
		else if (whichWeb == 2) {
			Application.OpenURL ("https://www.instagram.com/pudding_games_/"); 
		}
	}
}
