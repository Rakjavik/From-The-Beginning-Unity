namespace rak.being.species.critter
{
    using rak.unity;
    using rak.unity.baseobject;
    using rak.util;
    using rak.work.tasks;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.AI;
    using UnityEngine.UI;

    public class CritterAgent : Agent
    {

        public static float changeScaleEvery = .05f;
        public static Material[] critterMaterials;

        // Use this for initialization
        void Start()
        {
            if (!initialized)
            {
                initializeBeing(this, gameObject, 'n', Util.getRandomString("Critter"), null);
                initializeAgent();
                initialized = true;
            }
        }

        public new void initializeAgent()
        {
            inventory = new Inventory(1, gameObject);
            base.initializeAgent();
            debug(gameObject.name + " initializing with roomObject - " + roomObject.GetInstanceID());
            floorYPosition = yFloorPositionToScaleRatio;
            Critter myBeing = (Critter)getBeing();
            distanceToTargetValidRatio = 5.0f;
            yFloorPositionToScaleRatio = -2.1f;
            distanceToTargetValid = myBeing.getCurrentSize() * distanceToTargetValidRatio;
            setBeing(myBeing);

            critterMaterials = new Material[2];

            critterMaterials[0] = (Material)Resources.Load("Materials/lava_001");
            critterMaterials[1] = (Material)Resources.Load("Materials/lava_002");
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
            // Viewscreen update //
            updateViewScreen();
            beingUpdates();
        }

        private void updateViewScreen()
        {
            StringBuilder stringBuilder = new StringBuilder("--Name--\n").AppendLine(getBeing().getName()).AppendLine("--Current Job--");
            stringBuilder.AppendLine(getCurrentTask().getTaskName());
            stringBuilder.AppendLine("--Target--");
            if (jobQueue.getCurrentJobTarget() != null)
            {
                stringBuilder.AppendLine(jobQueue.getCurrentJobTarget().name);
            }
            else
            {
                stringBuilder.Append("None");
            }
            viewScreen.text = stringBuilder.ToString();
        }

        private new void beingUpdates()
        {
            base.beingUpdates();
            being.ageBeing(Time.deltaTime,changeScaleEvery);
            if (gameObject.transform.position.y <= floorYPosition && being.isWaitingToGiveBirth())
            {
                birth();
            }
            if (being.isPregnant())
            {
                being.progressPregnany(Time.deltaTime);
            }
        }

        private void initializeBeing(CritterAgent child, GameObject gameObject,char gender,string name,Critter[] parents)
        {
            Critter childCritter = new Critter(name, gender, gameObject,parents);
            child.setBeing(childCritter);
        }

        protected void birth()
        {
            being.birth();
            CritterAgent child = Object.Instantiate(this, this.transform.parent,true);
            child.transform.position = transform.position;
            initializeBeing(child, child.gameObject, 'n', Util.getLastName(this.being), new Critter[] { (Critter)being });
            child.initializeAgent();
            child.setInitialized(true);
            Material childMaterial;
            if (Time.time % 2 <= 1)
            {
                childMaterial = critterMaterials[0];
            } else
            {
                childMaterial = critterMaterials[1];
            }
            child.GetComponentInChildren<Renderer>().material = childMaterial;
            
            being.addChild(child.getBeing());
            NavMeshAgent agent = child.GetComponent<NavMeshAgent>();
            Debug.Log(child.getBeing().getName());
        }
    }
}