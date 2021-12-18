using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Crazy.Menu;


    public class Settings : MonoBehaviour
    {
        /*#region Classes

        #region Enums
        public enum PathVersion
        {
            Persistent,
            DataFolder,
            RootFolder
        }
        [System.Serializable]
        public enum Keys
        {
            Up,
            Down,
            Left,
            Right,
            Build,
            Shop,
            Destroyer
        }
        #endregion

        #region Classes

        [System.Serializable]
        public class ButtonSettings
        {
            public string ButtonName;
            public Text buttonText;
        }

        #endregion

        #endregion
        #region Config settings
        [Header("Config settings:")]
        public static Settings Instance;
        public PathVersion pathVersion;
        public string configFile = "config.json";
        private string configFilePath;
        public Config config = new Config();
        [Header("ConfigObj:")]
        public Event keyEvent;
        KeyCode newKey;
        public ButtonSettings[] settingsB;
        public bool WaitingForKeys = false;
        #endregion

        #region Buttons
        public KeyCode Up { get; private set; }
        public KeyCode Down { get; private set; }
        public KeyCode Left { get; private set; }
        public KeyCode Right { get; private set; }
        public KeyCode Build { get; private set; }
        public KeyCode Shop { get; private set; }
        public KeyCode Destroyer { get; private set; }

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            if (Instance != null)
            {
                Debug.LogError("There's another settings script in scene!");
                return;
            }
            Instance = this;
            WaitingForKeys = false;
            #region Config File
            #region Setting Config Path
            configFile = "/" + configFile;
            switch (pathVersion)
            {
                case PathVersion.Persistent:
                    configFilePath = Application.persistentDataPath + configFile;
                    break;
                case PathVersion.DataFolder:
                    configFilePath = Application.dataPath + configFile;
                    break;
                case PathVersion.RootFolder:
                    configFilePath = Application.dataPath + "/.." + configFile;
                    break;
            }
            Debug.Log("Config File Path is now: " + configFilePath);
            #endregion

            #region Save/Load Config File
            SaveLoadConfig();
            //ret:
            //if (!File.Exists(configFilePath))
            //{
            //    string data = JsonUtility.ToJson(config);
            //    File.WriteAllText(configFilePath, data);
            //}
            //else
            //{
            //    try
            //    {
            //        string data = File.ReadAllText(configFilePath);
            //        config = JsonUtility.FromJson<Config>(data);
            //    }
            //    catch (System.Exception error)
            //    {
            //        Debug.LogError(error.ToString());
            //        File.Delete(configFilePath);
            //        goto ret;
            //    }
            //}
            #endregion

            #region Apply Config File Data

            Config.Button[] buttons = config.buttons.ToArray();
            string x = "";
            foreach (Config.Button item in buttons)
            {
                if (item.Name == "Up")
                {
                    Up = item.key;
                }
                else if (item.Name == "Right")
                {
                    Right = item.key;
                }
                else if (item.Name == "Down")
                {
                    Down = item.key;
                }
                else if (item.Name == "Left")
                {
                    Left = item.key;
                }
                else if (item.Name == "Build")
                {
                    Build = item.key;
                }else if (item.Name == "Shop")
                {
                    Shop = item.key;
                }else if (item.Name == "Destroyer")
                {
                    Destroyer = item.key;
                }
                x += string.Format("{0} ma ustawienie: {1}\n", item.Name, item.key);
            }
            Debug.Log(x);
            #endregion

            foreach (Config.Button item in config.buttons)
            {
                foreach (ButtonSettings setting in settingsB)
                {
                    if (item.Name == setting.ButtonName)
                    {
                        setting.buttonText.text = item.key.ToString();
                    }
                }
            }
            #endregion
        }
        void SaveLoadConfig()
        {
            if (!File.Exists(configFilePath))
            {
                string data = JsonUtility.ToJson(config,true);
                File.WriteAllText(configFilePath, data);
            }
            else
            {
                try
                {
                    string data = File.ReadAllText(configFilePath);
                    config = JsonUtility.FromJson<Config>(data);
                }
                catch (System.Exception error)
                {
                    Debug.LogError(error.ToString());
                    File.Delete(configFilePath);
                    SaveLoadConfig();
                }
            }
      }

        void OnGUI()
        {
            keyEvent = Event.current;
            if (keyEvent.isKey&&WaitingForKeys)
            {
                newKey = keyEvent.keyCode;
                WaitingForKeys = false;
            }
        }

        public void ApplyButton(Keys Key)
        {
            if (!WaitingForKeys) StartCoroutine(AsignKey(Key));
        }
    public void ApplyButton(string Gey)
    {
        Keys Key = new Keys();
        if (Gey == "Left")
        {
            Key = Settings.Keys.Left;
        }
        if (!WaitingForKeys) StartCoroutine(AsignKey(Key));
    }
    IEnumerator WaitingForKey()
        {
            while (!keyEvent.isKey)
            {
                yield return null;
            }
        }
        public IEnumerator AsignKey(Keys keys)
        {
            WaitingForKeys = true;

            yield return WaitingForKey();

            
                if(keys== Keys.Up){
                    Up = newKey;
                    SetButtonText("Up", Up);
                    }
                if(keys== Keys.Down){
                    SetButtonText("Down", Down);
                    }
                if(keys== Keys.Left){
                    SetButtonText("Left", Left);
                    }
                if(keys== Keys.Right){
                    SetButtonText("Right", Right);
                    }
                if(keys== Keys.Build){
                    SetButtonText("Build", Build);
                    }
                if(keys== Keys.Shop){
                    SetButtonText("Shop", Shop);
                    }
                if(keys== Keys.Destroyer){
                    SetButtonText("Destroyer", Destroyer);
                    }
            
            File.Delete(configFilePath);
            SaveLoadConfig();
        }
        void SetButtonText(string _name, KeyCode key)
        {
            foreach (ButtonSettings item in settingsB)
            {
                if (item.ButtonName == _name)
                {
                    item.buttonText.text = key.ToString();
                }
            }
        }*/
    }

