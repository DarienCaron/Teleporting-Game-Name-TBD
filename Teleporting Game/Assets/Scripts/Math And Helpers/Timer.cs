using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    



    public void Init(float _Time, TimerMode mode = TimerMode.CountDown )
    {
        m_CountMode = mode;
        m_StartTime = _Time;
        Reset();
        
    }
   

    public void Reset()
    {
        
        m_CurrentTime = 0;
        CurrentTimerState = TimerState.Stopped;
    }

    public void TimerEnd()
    {
        CurrentTimerState = TimerState.Ended;
    }

    public bool HasTimerEnded()
    {
        return CurrentTimerState == TimerState.Ended || CurrentTimerState == TimerState.Stopped;
    }

    public void Update()
    {
       switch(CurrentTimerState)
        {
            case TimerState.Running:
                switch(m_CountMode)
                {
                    case TimerMode.CountDown:
                        m_CurrentTime -= Time.deltaTime;
                        if (m_CurrentTime <= 0)
                        {
                            TimerEnd();
                        }
                        break;
                    case TimerMode.CountUp:
                        m_CurrentTime += Time.deltaTime;
                        if(m_CurrentTime >= m_StartTime)
                        {
                            TimerEnd();
                        }
                        break;
                }
                break;
            case TimerState.Ended:
                return;
            case TimerState.Stopped:
                return;
            case TimerState.Paused:
                return;

        }
    }


    public void Pause()
    {
        CurrentTimerState = TimerState.Paused;
    }

    public void Start()
    {
        Reset();
        CurrentTimerState = TimerState.Running;

        switch (m_CountMode)
        {
            case TimerMode.CountUp:
                m_CurrentTime = 0;
                break;
            case TimerMode.CountDown:
                m_CurrentTime = m_StartTime;
                break;
        }
        
    }

    public float TimeRemaining()
    {
        return Mathf.Abs(m_StartTime - m_CurrentTime);
    }




    public TimerState CurrentTimerState { get; private set; }

    private TimerMode m_CountMode;
    private float m_StartTime;
    private float m_CurrentTime;


    public enum TimerMode
    {
        CountDown,
        CountUp
    }




    public enum TimerState
    {
        Stopped,
        Running,
        Paused,
        Ended
    }

}
