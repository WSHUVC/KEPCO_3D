using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WSH.UI;
using Debug = WSH.Util.Debug;

namespace WSH.Core.Manager
{
    public class UIManager : MonoBehaviour
    {
        Managers managers;
        public CanvasBase[] canvasis;
        PanelBase[] panels;
        private void Awake()
        {
            var trashs = FindObjectsOfType<UIManager>();
            if (trashs.Length > 1)
            {
                Debug.Log($"12345!");
                for(int i = 1; i < trashs.Length; ++i)
                {
                    var target = trashs[i];
                    DestroyImmediate(target.gameObject);
                }
                return;
            }

            managers = FindObjectOfType<Managers>();
            canvasis = FindObjectsOfType<CanvasBase>();
            panels = FindObjectsOfType<PanelBase>();
        }

        UI_Canvas_Idle canvas_Idle;
        UI_Canvas_TopBar canvas_TopBar;
        UI_Canvas_Bottom canvas_Bottom;
        UI_Canvas_Tabs canvas_Tabs;
        public List<GameObject> allUI = new List<GameObject>();
        public void IdleOff()
        {
            Debug.Log($"{name}:IdleOff");
            //canvas_Tabs.Active();
            canvas_TopBar.Active();
            canvas_Bottom.Active();
            managers.ActivePlaceFlag();
        }
        public void IdleOn()
        {
            Debug.Log($"{name}:IdleOn");
            canvas_TopBar.Deactive();
            canvas_Bottom.Deactive();
            canvas_Tabs.Deactive();
            managers.DeactivePlaceFlag();
            managers.DeactiveSensorFlag();
        }
        WIBehaviour[] allUIs;
        void Start()
        {
            allUIs = FindObjectsOfType<WIBehaviour>();
            //Debug.Log($"UIManagerStart{i++}");
            foreach (var c in allUIs)
            {
                c.Initialize();
                if (c is UI_Canvas_Idle)
                {
                    canvas_Idle = c as UI_Canvas_Idle;
                    canvas_Idle.button_IdleOff.onClick.AddListener(IdleOff);
                }
                else if (c is UI_Canvas_TopBar)
                {
                    canvas_TopBar = c as UI_Canvas_TopBar;
                }
                else if (c is UI_Canvas_Bottom)
                {
                    canvas_Bottom = c as UI_Canvas_Bottom;
                }
                else if (c is UI_Canvas_Tabs)
                {
                    canvas_Tabs = c as UI_Canvas_Tabs;
                    canvas_Tabs.Deactive();
                }
            }
        }
    }
}