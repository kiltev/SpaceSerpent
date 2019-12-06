using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayButtonController : MonoBehaviour

{
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float maxVolume;

    [SerializeField] private GameObject playButton;
//    private EventTrigger.Entry entry;

 

    // Start is called before the first frame update

    void Start()
    {

        StartCoroutine(StartFade(GetComponent<AudioSource>(), fadeInDuration, maxVolume));
        //        // Create a new event trigger for the button:
//        EventTrigger trigger = GameObject.FindGameObjectWithTag("Button").GetComponent<EventTrigger>();
//
//        //create a new event trigger entry for mouseEnter and set the OnMouseEnter function to activate the trigger entry:
//        EventTrigger.Entry onMouseEnter = new EventTrigger.Entry();
//        onMouseEnter.eventID = EventTriggerType.PointerEnter;
//        onMouseEnter.callback = new EventTrigger.TriggerEvent();
//        onMouseEnter.callback.AddListener(new UnityAction<BaseEventData>(OnMouseEnter));
//        trigger.triggers.Add(onMouseEnter);
//
//        // create a new event trigger entry for mouseEnter and set the OnMouseExit function to activate the trigger entry:
//        EventTrigger.Entry onMouseExit = new EventTrigger.Entry();
//        onMouseExit.eventID = EventTriggerType.PointerExit;
//        onMouseExit.callback = new EventTrigger.TriggerEvent();
//        onMouseExit.callback.AddListener(new UnityAction<BaseEventData>(OnMouseExit));
//        trigger.triggers.Add(onMouseExit);
//
//        // create a new event trigger entry for mouseEnter and set the onMouseClick function to activate the trigger entry:
//        EventTrigger.Entry onMouseClick = new EventTrigger.Entry();
//        onMouseClick.eventID = EventTriggerType.PointerClick;
//        onMouseClick.callback = new EventTrigger.TriggerEvent();
//        onMouseClick.callback.AddListener(new UnityAction<BaseEventData>(OnMouseClick));
//        trigger.triggers.Add(onMouseClick);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            //play start game and go to game
            playButton.GetComponent<Animator>().SetTrigger("PressedPlay");
        }
    }


    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    //    public void manageEventTrigger()
    //    {
    //        
    //    }

    public void OnMouseEnter(BaseEventData eventData)
    {
        playButton.GetComponent<Animator>().SetBool("onHover", true);
        Debug.Log("entered");
    }

    public void OnMouseExit(BaseEventData eventData)
    {
        playButton.GetComponent<Animator>().SetBool("onHover", false);
        Debug.Log("exited");
    }

    public void OnMouseClick(BaseEventData eventData)
    {
        playButton.GetComponent<Animator>().SetTrigger("PressedPlay");
        Debug.Log("clicked");
    }
}
