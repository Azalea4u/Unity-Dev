using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COR : MonoBehaviour
{
    [SerializeField] float time = 3;
    [SerializeField] bool go = false;

    Coroutine timerCoroutine;

    void Start()
    {
        StartCoroutine("StoryTime");
        timerCoroutine = StartCoroutine(Timer(time));
        StartCoroutine(WaitAction());
    }

    void Update()
    {
        //time -= Time.deltaTime;
        //if (time <= 0)
        //{
        //    time = 3;
        //    print("Time's up!");
        //}
    }

    IEnumerator Timer(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time); 
            // check perception
            print("tick");
        }

        //yield return null;
    }

    IEnumerator StoryTime()
    {
        print("hello");
        yield return new WaitForSeconds(1);
        print("Welcome to the new world");
        yield return new WaitForSeconds(1);
        print("Time to die!");
        yield return new WaitForSeconds(1);
        StopCoroutine(timerCoroutine);

        yield return null;
    }

    IEnumerator WaitAction()
    {
        yield return new WaitWhile(() => go);
        print("go");
        yield return null;
    }
}
