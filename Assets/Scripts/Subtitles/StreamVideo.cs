using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
	public VideoPlayer videoPlayer;

	AudioSource audioSource;
	RawImage rawImage;

	public void Begin(RawImage rawImage)
	{
		videoPlayer.Prepare();
		this.rawImage = rawImage;
	}

	public void PlayVideo(AudioSource audioSource)
	{
		this.audioSource = audioSource;
		StartCoroutine(PlayVideoEnumerator());
	}

	IEnumerator PlayVideoEnumerator()
	{
		videoPlayer.Prepare();
		WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
		while (!videoPlayer.isPrepared)
		{
			yield return waitForSeconds;
			break;
		}
		rawImage.texture = videoPlayer.texture;
		videoPlayer.Play();
		audioSource.Play();
	}
}