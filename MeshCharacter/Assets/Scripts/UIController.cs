using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class UIController : MonoBehaviour
	{
		public Animator animatorr;

		public void Exit()
		{
			Application.Quit();
		}

		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update () {
	
		}

		void OnTriggerEnter2D(Collider2D coll)
		{
			if (coll.gameObject.tag == "Player")
			{
				animatorr.SetTrigger("CameInto");
			}
		}
		void OnTriggerExit2D(Collider2D coll)
		{
			if (coll.gameObject.tag == "Player")
			{
				animatorr.SetTrigger("CameOut");
			}
		}
	}
}
