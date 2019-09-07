using UnityEngine;

[CreateAssetMenu(fileName = "Subtitles #", menuName = "Subtitles/file")]
public class SubtitlesFile : ScriptableObject
{
	public string text;
	public AudioClip audio;
	public float extraTime = 0f;
}
