using DragonBones;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Probability {
    Move=2,         //移动
    Relax=3,        //放松   
    Sit=2,          //坐
    Sleep=2,        //睡觉
    Special=3,      //特殊
    Attack=2,       //攻击
    Speak=3         //说话
}
public class PlayControl : MonoBehaviour
{
    UnityArmatureComponent UnityArmatureComponent;
    //private String[] action = { "Relax","Interact","Move","Sit",""};
    List<string> array;
    Boolean flagaction = false;
    int len = 0;
    int i = 0;
    public static Boolean mousebuttondown = false; 
    //当前行动
    public static string curstate="";
    //move left right
    public static string movedirection = "";

    //人物活动范围
    int horizontal = 0;
    int vertical = 0;

    //屏幕像素尺寸
    private int px,py;

    //private GameObject game=GameObject.Find("Canvas");
    UnityEngine.Transform tran;

    public string getcurstate()
    {
        return curstate;
    }
    void Start()
    {
        
        UnityArmatureComponent  = GetComponent<UnityArmatureComponent>();//获得UnityArmatureComponent对象 绑定到角色上
        //UnityArmatureComponent = UnityFactory.factory.BuildArmatureComponent("armatureName");
        UnityArmatureComponent.animation.Play("Relax",0);//播放动画

        curstate = "Relax";
        //获得动作列表
        array = UnityArmatureComponent.animation.animationNames;
        //删除列表默认动作
        array.Remove("Default");
        len = array.Count;

        //获取设置当前屏幕分辩率 
        Resolution[] resolutions = Screen.resolutions;
        //Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
        px = resolutions[resolutions.Length - 1].width;
        py = resolutions[resolutions.Length - 1].height;
    }
    void move()
    {
        //UnityArmatureComponent.animation.Stop();
        UnityArmatureComponent.animation.Play("Move", 0);
    }
    void Update()
    {
        i++;

        Debug.Log(curstate.Equals("Move"));
        Debug.Log(horizontal);
        if (curstate.Equals("Move"))
        {
            switch (movedirection)
            {
                case "moveleft":
                    transform.position += Vector3.left * 0.1f;
                    horizontal--;
                    if (horizontal < -300)
                    {
                        movedirection = "moveright";
                        transform.localScale = new Vector3(10, 10, 1);
                        move();
                    }
                    break;
                case "moveright":
                    transform.position += Vector3.right * 0.1f;
                    horizontal++;
                    if (horizontal > 1000)
                    {
                        movedirection = "moveleft";
                        UnityArmatureComponent.transform.localScale = new Vector3(-10, 10, 1);
                        move();

                    }
                    break;
                case "moveup":
                    transform.position += Vector3.up * 0.1f;
                    vertical++;
                    if (vertical > 700)
                    {
                        movedirection = "movedown";
                        move();

                    }
                    break;
                case "movedown":
                    transform.position += Vector3.down * 0.1f;
                    vertical --;
                    if (vertical  < -100)
                    {
                        movedirection = "moveup";
                        move();

                    }
                    break;
            }
        }
       

        if (i >= 600)
        {
            string str = array[new System.Random().Next(0, len)];
            //flagaction = true;
            if (new System.Random().Next(0, 3) == 0)
                flagaction = true;
            else
                flagaction = false;
            //flagaction = randomaction(str);
            if (flagaction&&!curstate.Equals(str))
            {
                //Debug.Log(curstate);
                curstate = str;
                UnityArmatureComponent.animation.Stop();
                UnityArmatureComponent.animation.Play(str,0);
            }
            i = 0;
        }

        keyinput();

    }

    Boolean randomaction(string name)
    {
        Boolean flag1 = false;
        int tmp=0;
        switch (name)
        {
            case "Move": 
                tmp = (int)(Probability.Move);
                int a = new System.Random().Next(0, 4);
                if (a == 0)
                    movedirection = "moveleft";
                else if (a == 1)
                    movedirection = "moveright";
                else if (a == 2)
                    movedirection = "moveup";
                else if (a == 3)
                    movedirection = "movedown";

                
                break;
            case "Relax": tmp = (int)(Probability.Relax); break;
            case "Sit": tmp = (int)(Probability.Sit); break;
            case "Sleep": tmp = (int)(Probability.Sleep); break;
            case "Special": tmp = (int)(Probability.Special); break;
            case "Attack": tmp = (int)(Probability.Attack); break;
            case "Speak": tmp = (int)(Probability.Speak); break;
        }
        if (tmp >= new System.Random().Next(0, 10)) {
            flag1 = true;
        }
        return flag1;
    }
    void keyinput()
    {
        //if (flagaction && !curstate.Equals(str))
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            curstate = "Move";
            mousebuttondown = true;
            movedirection = "moveleft";
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mousebuttondown = true;
            curstate = "Move";
            movedirection = "moveright";
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mousebuttondown = true;
            curstate = "Move";
            movedirection = "moveup";
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mousebuttondown = true;
            curstate = "Move";
            movedirection = "movedown";
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            mousebuttondown = true;
            curstate = "Sit";
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            mousebuttondown = true;
            curstate = "Special";
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mousebuttondown = true;
            curstate = "Sleep";
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            mousebuttondown = true;
            curstate = "Relax";
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mousebuttondown = true;
            curstate = "Attack";
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            mousebuttondown = true;
            curstate = "Speak";
        }
        if (mousebuttondown)
        {
            switch (curstate)
            {
                case "Move":
                    UnityArmatureComponent.animation.Play("Move", 0);
                    if(movedirection=="moveleft")
                        //UnityArmatureComponent.armature.flipX = true;
                        transform.localScale = new Vector3(-10, 10, 1);
                    else if(movedirection=="moveright")
                        //UnityArmatureComponent.armature.flipX = false;
                        transform.localScale = new Vector3(10, 10, 1);
                    // UnityArmatureComponent.transform.localScale.Set(-1, 1, 1);
                    break;
                case "Sit": UnityArmatureComponent.animation.Play("Sit", 0); break;
                case "Sleep": UnityArmatureComponent.animation.Play("Sleep", 0); break;
                case "Special": UnityArmatureComponent.animation.Play("Special", 0); break;
                case "Attack": UnityArmatureComponent.animation.Play("Attack", 0); break;
                case "Relax": UnityArmatureComponent.animation.Play("Relax", 0); break;
                case "Speak": UnityArmatureComponent.animation.Play("Speak", 0); break;
            }
        }
        mousebuttondown = false;
    }
}
