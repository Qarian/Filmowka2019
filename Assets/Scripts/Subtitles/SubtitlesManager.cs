using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(StreamVideo))]
public class SubtitlesManager : MonoBehaviour
{
	[SerializeField]
	GameObject subtitlesPrefab = default;

	SubtitleScript subtitleScript;
	StreamVideo streamVideoScript;

	public static SubtitlesManager singleton;

	private void Awake()
	{
		singleton = this;
	}

	private void Start()
	{
		streamVideoScript = GetComponent<StreamVideo>();
		subtitleScript = Instantiate(subtitlesPrefab).GetComponent<SubtitleScript>();

		streamVideoScript.Begin(subtitleScript.background);
		subtitleScript.Begin(streamVideoScript);
	}

	public void ShowSubtitles(List<SubtitlesFile> subtitles)
	{
		subtitleScript.ShowSubtitles(subtitles);
	}
	/*
	public void ShowNextSubtitles()
	{
		if (subtitles.Count == nextId)
		{
			Debug.Log("Koniec napisów");
			subtitleScript.ShowSubtitles("The end", null, 5f);
			return;
		}

		//disableMovement

		if (subtitlesClip.Count > nextId)
		{
			if (extraOffset.Count > nextId)
				subtitleScript.ShowSubtitles(subtitles[nextId], subtitlesClip[nextId], extraOffset[nextId]);
			else
				subtitleScript.ShowSubtitles(subtitles[nextId], subtitlesClip[nextId]);
		}
		else
		{
			if (extraOffset.Count > nextId)
				subtitleScript.ShowSubtitles(subtitles[nextId], null, extraOffset[nextId]);
			else
				Debug.LogError("Subtititles with no length", gameObject);
		}

		nextId++;
	}*/
}
