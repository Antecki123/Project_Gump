using System.Collections.Generic;
using UnityEngine;

namespace FrontEnd
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private List<Widget> widgets;

        public void SetActive(bool state)
        {
            if (state)
                Enable();
            else
                Disable();

            foreach (var widget in widgets)
            {
                if (widget == null) continue;
                widget.SetActive(state);
            }
        }

        protected virtual void Enable() { }
        protected virtual void Disable() { }
    }
}