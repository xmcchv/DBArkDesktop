using Assets.Epoque.Animation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Assets.Epoque.Voice
{
    class ReadXml : MonoBehaviour
    {
        XmlDocument xmlDoc;
        //private Text m_Text;
        public LinkedList<string[]> list = new LinkedList<string[]>();
        public string curstate="";
        void Start() {
           // m_Text = GetComponent<Text>();
            //xmlDoc.Load(@"\Xml\远山.xml");
            if (File.Exists(Application.streamingAssetsPath + "/VoicePackets/Xml/远山.xml"))
            {
                xmlDoc = new XmlDocument();
                //XmlReaderSettings settings = new XmlReaderSettings();
                //settings.IgnoreComments = true;//忽略文档里面的注释
                //XmlReader reader = XmlReader.Create(@"\Xml\远山.xml", settings);
                //XmlNode xn = xmlDoc.SelectSingleNode("voicestore");
                //获取到XML的根元素进行操作
               // string str = "";
                xmlDoc.Load(Application.streamingAssetsPath + "/VoicePackets/Xml/远山.xml");
                XmlNodeList node = xmlDoc.SelectSingleNode("voicestore").ChildNodes;
                int i =0,j = 0;
                foreach (XmlElement element in node)
                {
                    
                    string[] tmp = new string[2]; 
                    foreach(XmlElement data in element)
                    {
                        // Debug.LogFormat("Name:{0},InnerText:{1} \n", element.Name, element.InnerText);
                        tmp[j] = data.InnerText;
                       // str+= data.InnerText;
                        j++;
                        if (j == 2)
                        {
                            j = 0;
                        }
                    }
                    i++;
                    list.AddFirst(tmp);
                    //Debug.LogFormat("Name:{0},InnerText:{1} \n", element.Name, element.InnerText);
                    //m_Text.text+=element.Name + element.InnerText;
                }
                //m_Text.text = list.ToString();
                curstate = PlayControl.curstate;
            }
            /*
            IEnumerable<XElement> enumerable = root.Elements();
            foreach (XElement item in enumerable)
            {
                int i = 0;
                foreach (XElement item1 in item.Elements())
                {
                    Console.WriteLine(item1.Name);   //输出 title   txt         
                    if(i==0)
                        WavPlayer.PlayWavSound(Application.streamingAssetsPath+@"/VoicePackets/远山/远山_"+item1.Name+".wav");
                    print(item1.Name);
                }
            }*/
            //Console.ReadKey();
        }
        void MouseDown()
        {
            //print(Application.streamingAssetsPath + "/VoicePackets/远山/远山_" + tmp.ElementAt(i) + ".wav");
            //Debug.LogWarning(Application.streamingAssetsPath + "/VoicePackets/远山/远山_" + tmp.ElementAt(i) + ".wav");
            //if (File.Exists(Application.streamingAssetsPath + "/VoicePackets/远山/远山_" + tmp.ElementAt(i) + ".wav"))
            //WavPlayer.PlayWavSound(Application.streamingAssetsPath + "/VoicePackets/远山/远山_" + tmp.ElementAt(i) + ".wav");
            //else
            // print("无此音频文件！");
            int len=list.Count();
            int pos = new System.Random().Next(0, len);
            string str = (list.ElementAt(pos))[0];
            string text= (list.ElementAt(pos))[1];
            string filepath = Application.streamingAssetsPath + "/VoicePackets/远山/远山_" + str + ".wav";
            if (File.Exists(filepath))
            {
                WavPlayer.PlayWavSound(filepath);
                TextTest.text = text;
                countnum = 0;
            }
            
            //WavPlayer.PlayWavSound(Application.streamingAssetsPath + "/VoicePackets/远山/远山_问候.wav");
            
        }
        private int countnum = 0;
       void Update()
        {
            countnum++;
            if (countnum >= 480)
            {
                countnum = 0;
                TextTest.text = "";
            }
            if (Input.GetMouseButtonDown(2))
            {
                MouseDown();
            }
            /*
            if (Input.GetKeyDown(KeyCode.Alpha1))
           {
                MouseDown();
            }*/
        }
        
        /*
         <library id="30">
          <BOOK id="20">
            <name>高等数学</name>
            <name1>大学英语</name1>
          </BOOK>
        </library>
         */
    }
}
