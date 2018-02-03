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
        public static float yFloorPositionToScaleRatio = -2.1f;

        // Use this for initialization
        void Start()
        {
            base.initializeAgent(); // Super
            inventory = new Inventory(1, gameObject);
            debug(gameObject.name + " initializing with roomObject - " + roomObject.GetInstanceID());
            floorYPosition = yFloorPositionToScaleRatio;
            Critter myBeing = new Critter(Util.getRandomString("Critter"), 'n', gameObject,null,true);
            distanceToTargetValid = myBeing.getCurrentSize();
            setBeing(myBeing);
            
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
            /*foreach (GameObject item in inventory.getItems())
            {
                stringBuilder.AppendLine(item.name);
            }*/
            stringBuilder.AppendLine(getCurrentTask().getTaskName());
            stringBuilder.AppendLine("--Target--");
            if (jobQueue.getCurrentJobTarget(transform) != null)
            {
                stringBuilder.AppendLine(jobQueue.getCurrentJobTarget(transform).name);
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

        private void initializeChildBeing(CritterAgent child, GameObject gameObject,char gender,string name,Critter[] parents)
        {
            Critter childCritter = new Critter(name, gender, gameObject,parents,true);
            child.setBeing(childCritter);
        }

        protected void birth()
        {
            being.birth();
            //CritterAgent child = Object.Instantiate(this,transform.forward*1,transform.rotation);
            CritterAgent child = Object.Instantiate(this, this.transform.parent,true);
            child.transform.position = transform.position;
            
            child.initializeAgent();

            //Critter childCritter = (Critter)child.getBeing();
            initializeChildBeing(child, child.gameObject, 'n', Util.getLastName(this.being), new Critter[] { (Critter)being });
            being.addChild(child.getBeing());
            NavMeshAgent agent = child.GetComponent<NavMeshAgent>();
            agent.speed = child.getBeing().getNavMeshAgentSpeed();
            Debug.Log(child.getBeing().getName());
        }
    }
}