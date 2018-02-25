namespace rak.unity.baseobject
{
    using rak.equipment;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    public class BaseScript : MonoBehaviour
    {

        private Inventory inventory;

        // Use this for initialization
        void Start()
        {
            inventory = new Inventory(50, gameObject);
        }

        public Inventory Inventory
        {
            get { return inventory; }
        }
    }
}