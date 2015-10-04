using UnityEngine;
using System.Collections;

    public class LoadOnClick : MonoBehaviour
    {
        public void LoadScene(int level)
        {
            Debug.LogFormat("Loading {0}", level);
            Application.LoadLevel(level);
        }

    }