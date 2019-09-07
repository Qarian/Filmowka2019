using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SubtitleScript : MonoBehaviour
{
	public RawImage background = default;
	[SerializeField]
	TextMeshProUGUI text = default;

	List<SubtitlesFile> subtitles = default;

	AudioSource audioSource;
	StreamVideo streamVideoScript;
	CharacterController controller;
	UnityEvent onSubtitlesClose;

	int subtitlesId = 0;

	public void Begin(StreamVideo streamVideoScript)
	{
		audioSource = GetComponent<AudioSource>();
		this.streamVideoScript = streamVideoScript;

		gameObject.SetActive(false);
		controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
	}

	public void ShowSubtitles(List<SubtitlesFile> subtitles, UnityEvent onSubtitlesClose)
	{
		this.subtitles = subtitles;
		this.onSubtitlesClose = onSubtitlesClose;
		ShowCurrentSubtitles();
	}

	void ShowCurrentSubtitles()
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
		if (subtitles.Count > subtitlesId + 1)
		{
			subtitlesId++;
			ShowCurrentSubtitles();
		}
		else
		{
			controller.enabled = true;
			if (SceneManager.GetActiveScene().buildIndex == 0)
			{
				SceneManager.LoadScene(1);
			}
			gameObject.SetActive(false);
		}
	}
}
