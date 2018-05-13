using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeedBoatManager : SingletonBehaviour<SpeedBoatManager>
{
    public CameraPathAnimator cpa0;
    public CameraPathAnimator cpa1;

    public GameObject boat0;
    public GameObject boat1;
    // Use this for initialization
    void Start()
    {
        cpa0.AnimationFinishedEvent += OnFinishedEvent0;
        cpa1.AnimationFinishedEvent += OnFinishedEvent1;

        cpa0.gameObject.SetActive(false);
        cpa1.gameObject.SetActive(false);
        boat0.SetActive(false);
        boat1.SetActive(false);

       EventDispatcher.AddEventListener(EventDefine.Event_Speed_Boat_Active, OnActiveBoat);
    }

    void OnDestroy()
    {
        EventDispatcher.RemoveEventListener(EventDefine.Event_Speed_Boat_Active, OnActiveBoat);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnFinishedEvent0()
    {
        StartCoroutine(ToHide(cpa0, boat0));
    }

    private void OnFinishedEvent1()
    {
        StartCoroutine(ToHide(cpa1, boat1));
    }

    private bool hasBeenActive;
    private void OnActiveBoat()
    {
        if (hasBeenActive)
            return;

        hasBeenActive = true;
        cpa0.gameObject.SetActive(true);
        cpa1.gameObject.SetActive(true);
        boat0.SetActive(true);
        boat1.SetActive(true);
        cpa0.Play();
        cpa1.Play();
    }

    IEnumerator ToHide(CameraPathAnimator cpa, GameObject boat)
    {
        yield return new WaitForSeconds(20);
        cpa.Stop();
        GameObject.Destroy(boat);
        GameObject.Destroy(cpa.gameObject);
    }
}
