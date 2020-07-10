using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Epoque.Animation
{
    
    public class TextTest : MonoBehaviour
    {

        private Text m_Text;
        public static string text="";
        private void Start()

        {

            m_Text = GetComponent<Text>();

        }


        private void Update()

        {

            // 获取当前时间

            //DateTime dateTime = DateTime.Now;

            // 将当前时间显示在 Text 控件上

            //m_Text.text = dateTime.ToString();
            m_Text.text = text;

        }

    }
}
