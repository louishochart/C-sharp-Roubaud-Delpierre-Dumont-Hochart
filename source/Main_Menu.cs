using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Script that display the main menu at the beginning of the game
public class Main_Menu : MonoBehaviour
{
    //Buttons 
    public GameObject playNormal;
    public GameObject playHard;
    public GameObject quit;
    //Sounds
    public AudioClip mousePassingBy;
    public AudioClip onClickSample;
    public AudioSource musicSource;

    private AudioSource _ausrc;

    // Start is called before the first frame update
    void Start()
    {
        _ausrc = GetComponent<AudioSource>();

        //PUT MUSIC SOURCE ON AWAKE


        //Add the listenre on buttons (to have OnClick methods)
        playNormal.GetComponent<Button>().onClick.AddListener(playNormalOnClick);
        playHard.GetComponent<Button>().onClick.AddListener(playHardOnClick);
        quit.GetComponent<Button>().onClick.AddListener(quitOnClick);


        EventTrigger.Entry eventtype = new EventTrigger.Entry();
        eventtype.eventID = EventTriggerType.PointerEnter;
        eventtype.callback.AddListener((eventData) => { _ausrc.PlayOneShot(mousePassingBy); });

        //
        playNormal.AddComponent<EventTrigger>();
        playNormal.GetComponent<EventTrigger>().triggers.Add(eventtype);

        playHard.AddComponent<EventTrigger>();
        playHard.GetComponent<EventTrigger>().triggers.Add(eventtype);

        quit.AddComponent<EventTrigger>();
        quit.GetComponent<EventTrigger>().triggers.Add(eventtype);
    }

    void playNormalOnClick()
    {
        _ausrc.PlayOneShot(onClickSample);
        StaticDifficultyManager.setDifficultyToNormal();
        loadSceneOne();
    }
    void playHardOnClick()
    {
        _ausrc.PlayOneShot(onClickSample);
        StaticDifficultyManager.setDifficultyToHard();
        loadSceneOne();
    }
    void quitOnClick()
    {
        _ausrc.PlayOneShot(onClickSample);
        Debug.Log("3");
        Application.Quit();
    }

    void loadSceneOne()
    {
        //TODO
        SceneManager.LoadScene("MAINSCENE");
    }
}
