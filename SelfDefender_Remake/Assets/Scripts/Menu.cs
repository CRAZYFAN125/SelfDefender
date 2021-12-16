using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Crazy.Menu
{
    public class Menu : MonoBehaviour
    {
        public static Menu menu;
        public Text returnText;
        [Header("Buttons:")]
        public Button setCloudSave;
        public Button getCloudSave;
        public Text LogIn;

        //bool Loged = false;

        // Start is called before the first frame update
        void Start()
        {
            menu = this;
            if (returnText!= null)
            {
                returnText.text = "";
            }

            ReloadButtonsData();
            
        }
        //void Update()
        //{
        //    if (Loged&&GameJolt.API.GameJoltAPI.Instance.CurrentUser!=null)
        //    {
        //        Loged = true;
        //        ReloadButtonsData();
        //    }
        //}

        public static void ReloadButtonsData()
        {
            if (GameJolt.API.GameJoltAPI.Instance.CurrentUser != null)
            {
                menu.LogIn.text = "Change account- GameJolt";
                if (File.Exists(Application.persistentDataPath + "/Save_SelfDefender.crazy"))
                {
                    menu.setCloudSave.interactable = true;
                }
                string key = "save";
                bool isGlobal = false;
                GameJolt.API.DataStore.Get(key, isGlobal, (string value) => {
                    if (value != null)
                    {
                        menu.getCloudSave.interactable = true;
                    }
                });
            }
            else
            {
                menu.LogIn.text = "Log In- GameJolt";
            }
        }

        #region cache


        public void ShowGameJoltLogIn()
        {
            if (GameJolt.API.GameJoltAPI.Instance.CurrentUser!=null)
            {
                GameJolt.API.GameJoltAPI.Instance.CurrentUser.SignOut();
            }
            GameJolt.UI.GameJoltUI.Instance.ShowSignIn();
        }



        public void SendNotification(string text)
        {
            if (returnText == null) return;
            returnText.text = text;
        }
        public void SendGameJoltNotification(string text)
        {
            if (GameJolt.UI.GameJoltUI.Instance == null)
            {
                return;
            }
            GameJolt.UI.GameJoltUI.Instance.QueueNotification(text);
        }
        #region Cloud
        public void SetSaveInCloud()
        {
            string key = "save";
            string value = File.ReadAllText(Application.persistentDataPath + "/Save_SelfDefender.crazy");
            bool isGlobal = false;
            GameJolt.API.DataStore.Set(key, value, isGlobal, (bool success) => { Debug.Log("Sended: " + success); });
        }
        public void GetSaveInCloud()
        {
            string key = "save";
            bool isGlobal = false;
            GameJolt.API.DataStore.Get(key, isGlobal, (string value) => {
                if (value != null)
                {
                    File.WriteAllText(Application.persistentDataPath + "/Save_SelfDefender.crazy", value);
                    Debug.Log("Downloaded File!");
                }
            });
        }
        #endregion
        public void ExitGame() { Application.Quit(); }

        #region Loaders
        #region Int
        public void LoadScene(int index)
        {
            StartCoroutine(Load(index));
        }

        IEnumerator Load(int index)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            if (returnText != null)
            {
                while (!operation.isDone)
                {
                    float value = Mathf.Floor( operation.progress / .9f);
                    returnText.text = value + "%";
                    yield return null;
                }
            }
            yield return null;
        }
        #endregion
        #region String
        public void LoadScene(string index)
        {
            StartCoroutine(Load(index));
        }

        IEnumerator Load(string index)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            if (returnText != null)
            {
                while (!operation.isDone)
                {
                    float value = Mathf.Floor( operation.progress / .9f);
                    returnText.text = value + "%";
                    yield return null;
                }
            }
            yield return null;
        }
        #endregion
        #endregion
        #endregion
    }
}