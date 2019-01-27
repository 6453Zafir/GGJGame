﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartConversation : MonoBehaviour
{
    public GameObject prefab;
    private float initial;
    private int randomBegin;
    private float startAnswerTime;//the time of begin answer
    private int answerDuration = 5;
    private int DontAnswer = 20;
    static public bool startAnswer = false;//whether start answer or not
    static public int question_num = 0;
    static public bool IsWin = false;
    static public bool IsOver = false;
    static public int limit_num = 6;
    private GameObject over;
    private GameObject win;
    // Start is called before the first frame update
    private float duration = 1.0f;
    private float count = 0;
    void Start()
    {
        initial = Time.time;
        randomBegin = Random.Range(5, 9);
        over = Resources.Load("Prefabs/GamePanel/EndCanvas") as GameObject;
        win = Resources.Load("Prefabs/GamePanel/Win") as GameObject;
    }
    void FixedUpdate()
    {
        Debug.Log("BoyControl.hairstate     " + BoyControl.hairstate);
        Debug.Log("!StartConversation.IsOver   " + !StartConversation.IsOver);
        if (BoyControl.hairstate == 0 && !IsOver)
        {
            IsOver = true;
            StartCoroutine(delay());
            
        }
        if (IsWin || IsOver) return;

        if (Time.time - initial >= randomBegin && !startAnswer)
        {
            question_num++;
            UIManager.Instance.ShowPanel("Question");
            UIManager.Instance.ShowPanel("Answer");
            startAnswer = true;
            startAnswerTime = Time.time;
        }


        if (startAnswer)
        {
            
            if (Time.time - startAnswerTime >= answerDuration)//the question is over
            {
                GameObject ValueText = GameObject.Find("Conversation/ValueOfAngry/Text");
                Answer.valueOfAngry = Mathf.Min(100, Answer.valueOfAngry + DontAnswer);
                ValueText.GetComponent<Text>().text = "愤怒值：" + Answer.valueOfAngry;
                Close();
                if (question_num == limit_num )
                {
                    IsWin = true;
                   
                    //win.SetActive(true);
                    GameObject win1 = Instantiate(win);
                }
            }
        }


    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(5);
        GameObject over1 = Instantiate(over);
    }
    public void Close()
    {
        UIManager.Instance.ClosePanel("Question");
        UIManager.Instance.ClosePanel("Answer");
        startAnswer = false;
        initial = Time.time;
        randomBegin = Random.Range(5, 9);
    }
}
