namespace rak.unity.baseobject
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    public class BaseScript : MonoBehaviour
    {

        private Inventory inventory;
        private Text viewScreen;

        // Use this for initialization
        void Start()
        {
            inventory = new Inventory(50, gameObject);
            viewScreen = gameObject.GetComponentInChildren<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            StringBuilder stringBuilder = new StringBuilder(gameObject.name);
            if (inventory.hasEmptySpace())
            {
                stringBuilder.Append("\nInventory : ").Append("\n" + inventory.listItems(false)).Append("/").Append(inventory.getMaxInventorySize());
            }
            else
            {
                stringBuilder.Append("INVENTORY FULL");
            }
            viewScreen.text = stringBuilder.ToString();
        }

        public bool addItem(GameObject item)
        {
            bool response = inventory.addItem(item);
            return response;
        }
    }
}