using rak.being.species;
using rak.util;
using System.Text;
using UnityEngine;

namespace rak.unity
{
    public class BirdAgent : Agent, AgentInterface
    {
        public static float changeScaleEvery = .05f;

        // Use this for initialization
        void Start()
        {
            base.initializeBaseAgent(); // Super
            debug(gameObject.name + " initializing with roomObject - " + roomObject.GetInstanceID());
            inventory = new Inventory(50, gameObject);
            floorYPosition = yFloorPositionToScaleRatio;
            Bird myBeing = new Bird(Util.getRandomString("Phoenix"), 'r', gameObject, null);
            distanceToTargetValidRatio = 3.0f;
            distanceToTargetValid = myBeing.getCurrentSize() * distanceToTargetValidRatio;
            navMeshSpeedRatio = 1.2f;
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
            being.ageBeing(Time.deltaTime, changeScaleEvery);
            if (gameObject.transform.position.y <= floorYPosition && being.isWaitingToGiveBirth())
            {
                //birth();
            }
            if (being.isPregnant())
            {
                being.progressPregnany(Time.deltaTime);
            }
        }
        void AgentInterface.initializeBeing(Agent child, GameObject gameObject, char gender, string name, IntelligentSpecies[] parents)
        {
            Bird childCritter = new Bird(name, gender, gameObject,(Bird[])parents);
            child.setBeing((Bird)childCritter);
        }

        void AgentInterface.birth()
        {
            being.birth();
            BirdAgent child = Object.Instantiate(this, this.transform.parent, true);
            child.transform.position = transform.position;
            AgentInterface ai = this;
            ai.initializeBeing(child, child.gameObject, 'n', Util.getLastName(this.being), new Bird[] { (Bird)being });
            child.initializeBaseAgent();
            child.setInitialized(true);
            being.addChild(child.getBeing());
            Debug.Log(child.getBeing().getName());
        }
    }
}