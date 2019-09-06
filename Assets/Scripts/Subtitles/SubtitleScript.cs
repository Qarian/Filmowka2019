using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SubtitleScript : MonoBehaviour
{
	public RawImage background = default;
	[SerializeField]
	TextMeshProUGUI text = default;

	AudioSource audioSource;
	StreamVideo streamVideoScript;

	public void Begin(StreamVideo streamVideoScript)
	{
		audioSource = GetComponent<AudioSource>();
		this.streamVideoScript = streamVideoScript;

		gameObject.SetActive(false);
	}

	public void ShowSubtitles(string subtitle, AudioClip clip, float extraOffset = 0)
	{
		gameObject.SetActive(true);
		text.text = subtitle;
		audioSource.clip = clip;

		if (clip != null)
			StartCoroutine(HideSubtitles(clip.length + extraOffset));
		else
			StartCoroutine(HideSubtitles(extraOffset));

		streamVideoScript.PlayVideo(audioSource);
	}

	IEnumerator HideSubtitles(float time)
	{
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
	}
}
