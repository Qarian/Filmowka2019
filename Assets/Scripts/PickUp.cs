using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
	[SerializeField]
	UnityEvent onPickUp = default;

	private void OnTriggerEnter(Collider other)
	{
		onPickUp.Invoke();
	}

	public void ShowNextSubtitles()
	{
		SubtitlesManager.singleton.ShowNextSubtitles();
	}
}
