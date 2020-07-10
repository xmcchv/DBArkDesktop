using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    //定义一个声音字段
    private AudioSource m_AudioSource;

    // Use this for initialization
    void Start()
    {
        //接收组件获取的值
        m_AudioSource = gameObject.GetComponent<AudioSource>();

    }
    //WavPlayer.PlayWavSound(Application.streamingAssetsPath + "/VoicePackets/远山/远山_交谈1.wav");
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(2))
        {
            
            m_AudioSource.Play();
        }

        /*
        //通过按键A控制播放

        if (Input.GetKeyDown(KeyCode.A))
        {
            m_AudioSource.Play();
        }


        //通过按键S控制暂停
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_AudioSource.Pause();
        }

        //通过按键D控制播放停止

        if (Input.GetKeyDown(KeyCode.D))
        {
            m_AudioSource.Stop();
        }
        */
    }
}

