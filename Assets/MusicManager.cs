using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour 
{
    public static MusicManager Instance { get; private set; }

    private AudioSource source;

	private void Awake()
	{
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        source = GetComponent<AudioSource>();
	}

    public void ChangeSong(AudioClip clip)
    {
        if (source == null)
            return;

        StartCoroutine(ChangeSongRoutine(clip));
    }

    IEnumerator ChangeSongRoutine(AudioClip clip)
    {
        yield return new WaitForSeconds(0.25f);
        while (source.volume > 0.1f)
        {
            source.volume -= Time.deltaTime;
            yield return null;
        }

        source.Stop();
        source.clip = clip;

        yield return new WaitForSeconds(0.3f);
        source.Play();

        while (source.volume < 0.95f)
        {
            source.volume += Time.deltaTime;
            yield return null;
        }
    }
}
