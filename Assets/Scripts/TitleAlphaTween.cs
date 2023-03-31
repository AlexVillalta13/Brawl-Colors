using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAlphaTween : MonoBehaviour {

		public string nextScene = "";

		// Update is called once per frame
		void TweenAlphaComplete (AlphaTweenWhite ta) {
			//dfadf
			ta.completionMessage = "GotoNextSlide";
			ta.targetAlpha = 1;

			ta.enabled = true;

		}

		void GotoNextSlide() {
			SceneManager.LoadScene(nextScene);
		}
	}
