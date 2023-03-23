using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public AudioSource setSound;
	public AudioSource misplaceSound;
	public AudioSource pickUpSound;

	void Start()
	{
		instance = this;
	}

	public void PlaySound(int index)
	{
		if (index == 0) {
			pickUpSound.Play ();
		} else if (index == 1) {
			setSound.Play ();
		} 
		else if (index == 2) 
		{
			misplaceSound.Play ();
		}
	}
}
