namespace rak.unity.GUI
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;
    using System.Text;
    using rak.being;
    using rak.being.species;
    using rak.util;

    public class MainGUI : MonoBehaviour
    {
        public GameObject roomObject;

        private Text text;
        private Canvas canvas;
        private BeingAgent focus;

        // Use this for initialization
        void Start()
        {
            text = GetComponentInChildren<Text>();
            canvas = GetComponent<Canvas>();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}