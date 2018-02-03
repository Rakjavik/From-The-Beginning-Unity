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
        private Agent focus;
        private RAKVRroom room;

        // Use this for initialization
        void Start()
        {
            text = GetComponentInChildren<Text>();
            canvas = GetComponent<Canvas>();
            room = roomObject.GetComponent<RAKVRroom>();
        }

        // Update is called once per frame
        void Update()
        {
            if(focus != null)
            {
                Species being = focus.getBeing();
                StringBuilder builder = new StringBuilder("");
                builder.AppendLine("---Parents---");
                if (being.getParents() != null)
                {
                    foreach (Being parent in being.getParents())
                    {
                        builder.AppendLine(parent.getName());
                    }
                }
                builder.AppendLine("---Children---");

                if (being.getChildren() != null)
                {
                    foreach (Being child in being.getChildren())
                    {
                        if (child != null)
                        {
                            builder.AppendLine(child.getName());
                        } else
                        {
                            builder.Append("");
                        }
                    }

                    builder.AppendLine("Name - " + being.getName());
                    builder.AppendLine("Gender - " + being.getGender());
                    builder.AppendLine("Age - " + Util.numbersAfterDecimal(being.getAge(), 3));
                    builder.AppendLine("Species - " + being.getSpeciesName());
                    builder.AppendLine("Time preggers - " + being.getTimePregnanant());
                    text.text = builder.ToString();
                }
            }
            else
            {
                text.text = "";
            }
            if (room.getSelection() != null)
            {
                focus = room.getSelection();
            }
            else
            {
                focus = null;
            }
        }
    }
}