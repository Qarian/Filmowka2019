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

	[Space]
	List<SubtitlesFile> subtitles = default;

	AudioSource audioSource;
	StreamVideo streamVideoScript;

	int subtitlesId = 0;

	public void Begin(StreamVideo streamVideoScript)
	{
		audioSource = GetComponent<AudioSource>();
		this.streamVideoScript = streamVideoScript;

		gameObject.SetActive(false);

		if (subtitles.Count == 0)
			Debug.LogError("Nie ma pliku subtitles!!!");
	}

	public void ShowSubtitles(List<SubtitlesFile> subtitles)
	{
		this.subtitles = subtitles;
	}

	void ShowSubtitles()
	{
		gameObject.SetActive(true);
		text.text = subtitles[subtitlesId].text;
		audioSource.clip = subtitles[subtitlesId].audio;

		if (subtitles[subtitlesId].audio != null)
			StartCoroutine(HideSubtitles(subtitles[subtitlesId].audio.length + subtitles[subtitlesId].extraTime));
		else
			StartCoroutine(HideSubtitles(subtitles[subtitlesId].extraTime));

		streamVideoScript.PlayVideo(audioSource);
	}

	IEnumerator HideSubtitles(float time)
	{
		yield return new WaitForSeconds(time);
		if (subtitles.Count > subtitlesId)
		{
			subtitlesId++;
			ShowSubtitles();
		}
		else
			gameObject.SetActive(false);
	}
}
