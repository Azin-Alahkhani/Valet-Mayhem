using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : RandomEvent
{
    public float activeTime;
    float timer;
    public bool active = true;
    public UnityEvent ended;


    public virtual void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= activeTime)
        {
            active = false;
            Destroy(gameObject);
            timer = 0;
            ended.Invoke();
        }
    }
}
