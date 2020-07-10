using DragonBones;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Transform = UnityEngine.Transform;

enum Probability {
    Moveleft=2,         //移动
    Moveright=2,
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
    public string getcurstate()
    {
        return curstate;
    }
    void Start()
    {
        // Load Data  不用绑定到角色上
        //UnityFactory.factory.LoadDragonBonesData("Epoque/Fmout_Epoque/Fmout_Epoque_Build_ske");
        //UnityFactory.factory.LoadTextureAtlasData("Epoque/Fmout_Epoque/Fmout_Epoque_Build_tex");
         UnityArmatureComponent  = GetComponent<UnityArmatureComponent>();//获得UnityArmatureComponent对象 绑定到角色上
        //UnityArmatureComponent = UnityFactory.factory.BuildArmatureComponent("armatureName");
        UnityArmatureComponent.animation.Play("Relax",0);//播放动画

        curstate = "Relax";
        //获得动作列表
        array = UnityArmatureComponent.animation.animationNames;
        //删除列表默认动作
        array.Remove("Default");
        len = array.Count;
        print(array.ToArray().ToString());
    }
    /*
    Boolean IsChange(String str)
    {
        return curstate.Equals(str);
    }*/
    void Update()
    {
        i++;
        if (i >= 600)
        {
            string str = array[new System.Random().Next(0, len)];
            flagaction = randomaction(str);
            if (flagaction&&!curstate.Equals(str))
            {
                UnityArmatureComponent.animation.Stop();
                if (str.Equals("Move"))
                {
                    switch (movedirection)
                    {
                        case "moveleft": transform.localScale = new Vector3(-1, 1, 1); break;
                        case "moveright": transform.localScale = new Vector3(1, 1, 1); break;
                    }
                }
                UnityArmatureComponent.animation.Play(str,0);
            }
            i = 0;
        }
        keyinput();
        if (curstate.Equals("moveleft") )
        {
            WindowsTool.bx = true;
            WindowsTool.direction = "left";
            
        }
        else if (curstate.Equals("moveright"))
        {
            WindowsTool.bx = true;
            WindowsTool.direction = "right";
            //Rect pos1 = WindowsTool.getrect();
            //WindowsTool.SetWindowPos(WindowsTool.GetForegroundWindow(), 0, (int)pos1.x + 5, (int)pos1.y, (int)pos1.width, (int)pos1.height, WindowsTool.SWP_SHOWWINDOW);
        }
    }

    Boolean randomaction(string name)
    {
        Boolean flag1 = false;
        int tmp=0;
        switch (name)
        {
            case "Move": 
                tmp = (int)(Probability.Moveleft);
                if (new System.Random().Next(0, 2) == 0)
                    movedirection = "moveleft";
                else
                    movedirection = "moveright";
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            curstate = "Move";
            mousebuttondown = true;
            movedirection = "moveleft";
            //WindowsTool.SetWindowPos(WindowsTool.GetForegroundWindow(),-1,0,0,0,0,WindowsTool.SWP_SHOWWINDOW);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            mousebuttondown = true;
            curstate = "Move";
            movedirection = "moveright";
            //WindowsTool.SetWindowPos(WindowsTool.GetForegroundWindow(), -1, 500, 400, 0, 0, WindowsTool.SWP_SHOWWINDOW);
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
                        transform.localScale = new Vector3(-1, 1, 1);
                   else if(movedirection=="moveright")
                        transform.localScale = new Vector3(1, 1, 1);
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
