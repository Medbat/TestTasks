using UnityEngine;

namespace Assets.Scripts
{
	[RequireComponent(typeof(Animator))]
	public class MuzzleFlashScript : MonoBehaviour
	{
		private void Start ()
		{
			Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
		}
	}
}
