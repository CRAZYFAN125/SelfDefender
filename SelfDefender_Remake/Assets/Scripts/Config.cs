using System.Collections.Generic;
using UnityEngine;

namespace Crazy.Menu
{
    [System.Serializable]
    public class Config
    {
        [System.Serializable]
        public class Button
        {
            public string Name;
            public KeyCode key;
        }
        public bool ControllerInput = false;
        public List<Button> buttons;
    }
}