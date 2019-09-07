using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageScript : MonoBehaviour
{
	AudioSource source;

	private void OnTriggerEnter(Collider other)
	{
		if (source == null)
			source = GetComponent<AudioSource>();
		source.Play();

		CharacterController controller = other.GetComponent<CharacterController>();
		controller.enabled = false;
		other.transform.position = new Vector3(0f, 1f, 0f);
		controller.enabled = true;
	}
}
