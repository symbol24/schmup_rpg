using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogoScript : MonoBehaviour
{

    public List<Graphic> imagesToFadeOut1st;
    public float imagesToFadeOut1stTimer;
    public float imagesToFadeOut1stFadeTimer;

    public List<Graphic> imagesToFadein1st;
    public float imagesToFadein1stTimer;
    public float imagesToFadein1stFadeTimer;

    public List<Graphic> imagesToFadeOut2nd;
    public float imagesToFadeOut2ndTimer;
    public float imagesToFadeOut2ndFadeTimer;

    public List<Graphic> imagesToFadein2nd;
    public float imagesToFadein2ndTimer;
    public float imagesToFadein2ndFadeTimer;

    public List<Graphic> imagesToFadeOut3rd;
    public float imagesToFadeOut3rdTimer;
    public float imagesToFadeOut3rdFadeTimer;

    public List<Graphic> imagesToFadein3rd;
    public float imagesToFadein3rdTimer;
    public float imagesToFadein3rdFadeTimer;

    public AudioSource firstThemeSong;
    public float fadeTimerFor1stTheme;
    public float startFadeFor1stTheme;

    public AudioSource secondThemeSong;
    public float fadeTimerFor2ndTheme;
    public float startFadeFor2ndThemeSong;

    public AudioSource shot;
    public float timeForShot;
    public bool isShotEnabled;
    public float timerToCutAudio;
    public float fadeTimeToCutAudio;
    public float timeToJumpToNextScene;
    public string nextSceneName;


	// Use this for initialization
	void Start ()
	{
        /*StartCoroutine(StartFadeout(imagesToFadeOut1st, 0, 0));
        StartCoroutine(StartFadeout(imagesToFadeOut2nd, 0, 0));
        StartCoroutine(StartFadeout(imagesToFadeOut3rd, 0, 0));*/
        StartCoroutine(StartFadein(imagesToFadein1st, 0, 0));
        StartCoroutine(StartFadein(imagesToFadein2nd, 0, 0));
        StartCoroutine(StartFadein(imagesToFadein3rd, 0, 0));
        StartCoroutine(StartFadein(imagesToFadein1st, imagesToFadein1stFadeTimer, imagesToFadein1stTimer));
        StartCoroutine(StartFadein(imagesToFadein2nd, imagesToFadein2ndFadeTimer, imagesToFadein2ndTimer));
        StartCoroutine(StartFadein(imagesToFadein3rd, imagesToFadein3rdFadeTimer, imagesToFadein3rdTimer));
        StartCoroutine(StartFadeout(imagesToFadeOut1st, imagesToFadeOut1stFadeTimer, imagesToFadeOut1stTimer));
        StartCoroutine(StartFadeout(imagesToFadeOut2nd, imagesToFadeOut2ndFadeTimer, imagesToFadeOut2ndTimer));
        StartCoroutine(StartFadeout(imagesToFadeOut3rd, imagesToFadeOut3rdFadeTimer, imagesToFadeOut3rdTimer));
	    if (isShotEnabled)
	    {
	        StartCoroutine(ShotSound(shot, timeForShot));
	    }
	    StartCoroutine(FadeoutMusic(firstThemeSong, startFadeFor1stTheme, startFadeFor1stTheme));
	    StartCoroutine(FadeinMusic(secondThemeSong, startFadeFor2ndThemeSong, fadeTimerFor2ndTheme));
	    StartCoroutine(FadeoutMusic(secondThemeSong, timerToCutAudio, fadeTimeToCutAudio));
	    StartCoroutine(LoadLevel(nextSceneName, timeToJumpToNextScene));
	}

    private IEnumerator StartFadein(IEnumerable<Graphic> collection, float fadeinTime, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var graphic in collection)
        {
            StartCoroutine(graphic.FadeIn(fadeinTime));
        }
    }
    private IEnumerator StartFadeout(IEnumerable<Graphic> collection, float fadeinTime, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var graphic in collection)
        {
            StartCoroutine(graphic.FadeOut(fadeinTime));
        }
    }

    private IEnumerator FadeoutMusic(AudioSource source, float delay, float fadeTime)
    {
        yield return new WaitForSeconds(delay);
        while (source.volume > 0)
        {
            var currentSubstractor = fadeTime <= 0 ? 1 : Time.deltaTime / (fadeTime);
            source.volume = source.volume - currentSubstractor <= 0 ? 0 : source.volume - currentSubstractor;
            yield return null;
        }
        source.Stop();
    }
    private IEnumerator FadeinMusic(AudioSource source, float delay, float fadeTime)
    {
        yield return new WaitForSeconds(delay);
        source.volume = 0;
        source.Play();
        while (source.volume < 1)
        {
            var currentAdder = fadeTime <= 0 ? 0 : Time.deltaTime / (fadeTime);
            source.volume = source.volume + currentAdder >= 1 ? 1 : source.volume + currentAdder;
            yield return null;
        }
    }

    private IEnumerator ShotSound(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.Play();
    }

    private IEnumerator LoadLevel(string levelName, float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.LoadLevel(levelName);
    }


}
