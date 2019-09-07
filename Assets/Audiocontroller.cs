using UnityEngine;
using UnityEngine.SceneManagement;

public class Audiocontroller : MonoBehaviour
{
	public static Audiocontroller singleton;

    void Start()
    {
		if (singleton != null)
		{
			Destroy(gameObject);
			return;
		}
        DontDestroyOnLoad(gameObject);
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(0);
	}
}
