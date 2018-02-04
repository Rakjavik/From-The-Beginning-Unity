using rak.being.species;
using rak.util;
using System.Text;
using UnityEngine;

namespace rak.unity
{
    public class BirdAgent : Agent
    {
        public static float changeScaleEvery = .05f;

        // Use this for initialization
        void Start()
        {
            base.initializeAgent(); // Super
            inventory = new Inventory(1, gameObject);
            debug(gameObject.name + " initializing with roomObject - " + roomObject.GetInstanceID());
            floorYPosition = yFloorPositionToScaleRatio;
            Bird myBeing = new Bird(Util.getRandomString("Phoenix"), 'r', gameObject, null);
            distanceToTargetValidRatio = 10.0f;
            distanceToTargetValid = myBeing.getCurrentSize() * distanceToTargetValidRatio;
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
    }
}