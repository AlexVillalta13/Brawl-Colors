using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace DemiumGames.Notifications{

	//Ejemplo de manager de notificaciones muy simple. 
	public class NotificationHandler : MonoBehaviour {

	    [System.Serializable]
	    private struct NotificationData
	    {
	        public int id;
	        public string title;
	        public string text;
	        public int seconds; 
	    }

	    string[] texts;
		int[] notificationsSeconds;
		private string smallIcon = Resources.noticono;
		private string largeIcon = Resources.noticonoBig; 

		public int hours;
		public int minutes;
		public int seconds;

	    [SerializeField]
	    private List<NotificationData> notifications;


	    public void CancellAllNotifications()
	    {
			NotificationManager.Instance.CancelAllNotifications (); 
	    }

		// Use this for initialization
		void Start () {
	        this.CancellAllNotifications(); 
			texts = new string[] {
				"Brawl Colors is waiting for you.", 
				"Play Brawl Colors now!", 
				"Someone somewhere has beaten your score!", 
				"Play to increase your intelligence.",

				"Play to beat your ",
				"Try to beat your score! ",
				"It's been a while :(",
				"We miss you :("
			};
			notificationsSeconds = new int[] {0,0,0,0,0,0,0,0,0,0,0};
		}

	    void OnApplicationPause(bool pauseStatus)
	    {
			DateTime dateTime = DateTime.Now;
			DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day + 1, hours, minutes, seconds);

			double secondsTillFirstNotification = dt.Subtract (dateTime).TotalSeconds;

			for (int i = 0; i < notificationsSeconds.Length; i++) {
				if (i > 6) {
					notificationsSeconds [i] = (int)secondsTillFirstNotification + ((i - 5) * 604800);
				}
				else{
					notificationsSeconds [i] = (int)secondsTillFirstNotification + (i * 86400);
				}
			}

			texts [4] += GameManager.Instance.highscore + " score now!";
			texts [5] += GameManager.Instance.highscore;
	        if (pauseStatus)
	        {
				int randomy;
				Debug.Log ("Pausado"); 
				for (int i = 0; i < notifications.Count; i++) {
					if (i > 5) {
						randomy = UnityEngine.Random.Range (6, texts.Length - 1);
					} 
					else
					{
						randomy = UnityEngine.Random.Range (0, texts.Length - 3);
					}
					NotificationManager.Instance.SendNotification(notifications[i].id, notifications[i].title, texts[randomy], smallIcon, largeIcon, notificationsSeconds[i]);
	            }
	        }
	        else
	        {
				Debug.Log ("No pausado"); 

	            this.CancellAllNotifications(); 
	        }
	    }
	  }
}
