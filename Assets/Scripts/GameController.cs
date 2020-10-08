using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Text textComponent;
    [SerializeField] Text day;
    [SerializeField] State startingState;
    [SerializeField] Material nightSky;
    [SerializeField] Material daySky;
    [SerializeField] Text button1Text;
    [SerializeField] Text button2Text;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;

    //Audio
    [SerializeField] AudioClip wolfHowling;
    [SerializeField] AudioClip backGroundMusic;
    [SerializeField] AudioClip nightAmbience;
    [SerializeField] AudioClip dayAmbience;

    AudioSource backgroundMusicPlayer;
    AudioSource ambiencePlayer;
    AudioSource backgroundNoisePlayer;

    GameObject _particleSystem;

    State state;
    State[] nextStates;
    Light _gameLight;
    bool isHowling = true;

    string eatenFruit;

    private void Start()
    {
        _gameLight = GameObject.Find("Directional Light").GetComponent<Light>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        _particleSystem = GameObject.Find("Particle System");
        backgroundMusicPlayer = audioSources[0];
        ambiencePlayer = audioSources[1];
        backgroundNoisePlayer = audioSources[2];
        backgroundMusicPlayer.clip = backGroundMusic;
        backgroundMusicPlayer.loop = true;
        backgroundMusicPlayer.volume = 0.2f;
        backgroundMusicPlayer.Play();
        state = startingState;
        NightTime();
        nextDay("One");
    }

    private void UpdateScene()
    {
        String story = state.GetStateStory();
        story = story.Replace("$", eatenFruit);
        textComponent.text = story;
        button1Text.text = state.GetButton1Text();
        if(button2.activeSelf)
        {
        button2Text.text = state.GetButton2Text();
        }
    }

    private void NightTime()
    {
        _particleSystem.SetActive(true);
        RenderSettings.skybox = nightSky;
        _gameLight.intensity = 10f;
        _gameLight.range = 68f;
        _gameLight.color = new Color(0.5566038f, 0.5566038f, 0.5566038f);
        ambiencePlayer.clip = nightAmbience;
        ambiencePlayer.loop = true;
        ambiencePlayer.volume = 0.4f;
        backgroundNoisePlayer.clip = wolfHowling;
        ambiencePlayer.Play();
        StartCoroutine(WolfHowling());
    }

    IEnumerator WolfHowling()
    {
        while(isHowling == true)
        {
            backgroundNoisePlayer.volume = 0.3f;
            backgroundNoisePlayer.Play();
            yield return new WaitForSeconds(UnityEngine.Random.Range(10f,20f));
        }
    }
    private void DayTime()
    {
        _particleSystem.SetActive(false);
        backgroundNoisePlayer.Stop();
        isHowling = false;
        StopCoroutine(WolfHowling());
        RenderSettings.skybox = daySky;
        _gameLight.range = 68f;
        _gameLight.intensity = 12f;
        _gameLight.color = new Color(0.9339623f,0.922413f,0.1806248f);
        ambiencePlayer.clip = dayAmbience;
        ambiencePlayer.loop = true;
        ambiencePlayer.Play();
    }

    private void Update() {
        nextStates = state.GetNextState();
        if(nextStates.Length <=1)
        {
            button2.SetActive(false);
        }
        else
        {
            button2.SetActive(true);
        }
        UpdateScene();
    }
    private void nextDay(string dayNumber)
    {
        day.text = "Day: " + dayNumber;
    }

    public void Option1()
    {
        state = nextStates[0];
        if(button1Text.text == "Next Day")
        {
            nextDay(state.GetDayNumber());
            DayTime();
        }
        if(button1Text.text == "That Night")
        {
            NightTime();
        }
        if(button1Text.text == "Eat the bananas")
        {
            eatenFruit = "bananas";
        }
    }
    public void Option2()
    {
        state = nextStates[1];
        if (button2Text.text == "Eat the berries")
        {
            eatenFruit = "berries";
        }
    }
}
