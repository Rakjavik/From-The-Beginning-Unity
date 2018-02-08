namespace rak.being.species.critter
{
    using rak.unity;
    using rak.util;
    using System.Text;
    using UnityEngine;

    public class CritterAgent : Agent, AgentInterface
    {

        public static float changeScaleEvery = .05f; // Scale has to change this much before it is updated on screen
        public static Material[] critterMaterials; // Different materials Critter can use


        

        // Pulls in the last severed body part and removes it from this object, this is called from a body part to get it's assignment
        public BodyPart consumeLastSevered()
        {
            BodyPart bodyPart = lastSevered;
            lastSevered = null;

            return bodyPart;
        }

        // Use this for initialization
        void Start()
        {
            base.initializeAgent(); // Super
            inventory = new Inventory(1, gameObject);
            debug(gameObject.name + " initializing with roomObject - " + roomObject.GetInstanceID());
            floorYPosition = yFloorPositionToScaleRatio;
            Critter myBeing = new Critter(Util.getRandomString("Critter"), 'n', gameObject, null);
            distanceToTargetValidRatio = 2.0f;
            distanceToTargetValid = myBeing.getCurrentSize() * distanceToTargetValidRatio;
            setBeing(myBeing);
        }
        private void OnDestroy()
        {
            Debug.Log("Destorying object - " + transform.name);
        }
        // Initializes the nav mesh agent aspect
        public new void initializeAgent()
        {
            inventory = new Inventory(1, gameObject);
            base.initializeAgent();
            debug(gameObject.name + " initializing with roomObject - " + roomObject.GetInstanceID());
            floorYPosition = yFloorPositionToScaleRatio;
            Critter myBeing = (Critter)getBeing();
            distanceToTargetValidRatio = 2.0f;
            yFloorPositionToScaleRatio = -2.1f;
            distanceToTargetValid = myBeing.getCurrentSize() * distanceToTargetValidRatio;
            setBeing(myBeing);

            critterMaterials = new Material[2];

            critterMaterials[0] = (Material)Resources.Load("Materials/lava_001");
            critterMaterials[1] = (Material)Resources.Load("Materials/lava_002");
            if(DEBUG_DISABLED_VIEWSCREEN)
            {
                viewScreen.text = "";
            }
        }

        // Update is called once per frame
        new void Update()
        {
            
            base.Update();
            if (!DEBUG_DISABLED_VIEWSCREEN)
            {
                // Viewscreen update //
                updateViewScreen();
            }
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
                AgentInterface ai = this;
                ai.birth();
            }
            if (being.isPregnant())
            {
                being.progressPregnany(Time.deltaTime);
            }
        }

        void AgentInterface.initializeBeing(Agent child, GameObject gameObject,char gender,string name,IntelligentSpecies[] parents)
        {
            Critter childCritter = new Critter(name, gender, gameObject,(Critter[])parents);
            child.setBeing(childCritter);
        }

        void AgentInterface.birth()
        {
            being.birth();
            CritterAgent child = Object.Instantiate(this, this.transform.parent,true);
            child.transform.position = transform.position;
            AgentInterface ai = this;
            ai.initializeBeing(child, child.gameObject, 'n', Util.getLastName(this.being), new Critter[] { (Critter)being });
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
            Debug.Log(child.getBeing().getName());
        }
    }
}