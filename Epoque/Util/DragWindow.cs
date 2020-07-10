using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.epoque.util
{
    class DragWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        //记录鼠标位置.
        Vector3 newPosition;
        public void OnBeginDrag(PointerEventData eventData)
        {
            SetDraggedPosition(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            SetDraggedPosition(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SetDraggedPosition(eventData);
        }
        /// <summary>
        /// set position of the dragged game object
        /// </summary>
        /// <param name="eventData"></param>
        private void SetDraggedPosition(PointerEventData eventData)
        {
            var rt = gameObject.GetComponent<RectTransform>();
            // transform the screen point to world point int rectangle
            Vector3 globalMousePos;
            //把屏幕坐标转换为UGUI对应的坐标
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos;
            }
        }
    }
}
