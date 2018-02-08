﻿namespace rak.unity
{

    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.AI;
    using rak.work.tasks;
    using rak.unity.baseobject;
    using rak.being.species;
    using rak.work.job;
    using rak.being;
    using NobleMuffins.LimbHacker.Guts;
    using NobleMuffins.LimbHacker;

    // Base agent class //
    public class Agent : MonoBehaviour
    {
        protected IntelligentSpecies being;

        protected float floorYPosition; //Y position when agent is close to ground, Set in child object
        protected float distanceToTargetValid; //Distance from target object before it's considered close enough to touch, Set in child object

        public bool DEBUG_RESOURCES_PERMANENT; // Agent does not deplete resources when picking up
        public bool DEBUG_CRITTER; // Display general debug for this agent
        public bool DEBUG_DISABLE_RESOURCE_COLLECTION; // Don't collect resources
        public bool DEBUG_DISABLED_VIEWSCREEN; // Keep the personal view screen blank

        private Task currentTask; // Current active task for agent
        protected bool initialized = false; // Whether the agent has been initialized

        public GameObject room; // Current room of the agent
        public GameObject viewToMoveTo; // Main Camera for when agent is selected

        protected string agentName; // Name of agent
        protected Inventory inventory; // Inventory of agent
        protected bool selected = false; // Whether the unit is currently selected
        protected RAKVRroom roomObject; // Room specific script
        protected Text viewScreen; // personal viewscreen of agent
        protected NavMeshAgent agent; // Navmesh agent for movement
        protected Camera camera; // Main camera
        protected Collider collider; // Collider
        //protected GameObject target; // Agent's movement destination
        protected JobQueue jobQueue; // Job queue
        protected float distanceToTargetValidRatio; // Ratio between size of agent and distance before it is considered at it's destination
        protected float yFloorPositionToScaleRatio; // Ration between size of agent and what is considered on the ground

        public BodyPart lastSevered = null; // The last severed body part, used for temporary storage so the severed part's thread can pick it up when created
        private ForwardPassAgent childOfHackable; // A script that gets put on body parts when they are severed
        private float timeDetached = 0; // Elapsed time the part has been detached from the body
        private GameObject figure; // Conv object

        private float lastClickTime = 0; // Last mouse click event


        // Use this for initialization
        void Start()
        {
            figure = transform.Find("Figure").gameObject; // Conv object
            childOfHackable = GetComponent<ForwardPassAgent>(); // If present, this is a body part
            if (!initialized && childOfHackable == null) // If it's NOT a body part and hasn't been initialized yet
            {
                initializeAgent();
                initialized = true;
                AgentInterface ai = (AgentInterface)this;
                //ai.birth();
                //ai.birth();
                //ai.birth();
            }
            // This is a body part //
            else if (childOfHackable != null)
            {
                debug(name);
                // Toggle game object to disable children objects //
                gameObject.SetActive(false);
                gameObject.SetActive(true);
                GameObject figure = transform.Find("Figure").gameObject;
                GameObject hackedOffMesh = figure.transform.GetChild(1).gameObject;
                hackedOffMesh.name = "HackedOffLimb";
                // remove unneeded components //
                hackedOffMesh.GetComponentInParent<Collider>().enabled = false;
                hackedOffMesh.GetComponentInParent<Agent>().enabled = false;
                hackedOffMesh.GetComponentInParent<NavMeshAgent>().enabled = false;
                hackedOffMesh.GetComponentInParent<Hackable>().enabled = false;
                // Add a new collider that will size to new mesh //
                BoxCollider collider = hackedOffMesh.AddComponent<BoxCollider>();
                hackedOffMesh.SetActive(true);
                hackedOffMesh.transform.parent.gameObject.SetActive(true);
                figure.SetActive(true);
                gameObject.SetActive(true);
            }
        }
        // Initialize componenets //
        public void initializeAgent()
        {
            selected = false;
            camera = viewToMoveTo.GetComponent<Camera>();
            viewScreen = GetComponentInChildren<Text>();
            roomObject = room.GetComponent<RAKVRroom>();
            if (!roomObject.doDisplayPersonalViewScreens())
            {
                viewScreen.enabled = false;
            }
            agent = GetComponentInChildren<NavMeshAgent>();
            collider = GetComponentInChildren<Collider>();
            jobQueue = new JobQueue(this);
            currentTask = jobQueue.getNextTask();
        }
        // Update is called once per frame
        protected void Update()
        {
            lastClickTime += Time.deltaTime;

            // This is a limb, not a full critter //
            if (childOfHackable != null)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                timeDetached += Time.deltaTime;
                debugVerbose("Time detached - " + timeDetached);
                if (timeDetached > 10)
                {
                    Destroy(gameObject);
                }
                return;
            }

            // Make sure agent is not moving if it selected //
            if (selected)
            {
                collider.attachedRigidbody.velocity = Vector3.zero;
            }

            if(currentTask.isCompleted())
            {
                currentTask = jobQueue.getNextTask();
            }
            // Attempt to get the current job //
            Job currentJob = jobQueue.getCurrentJob();

            // The job's target //
            GameObject target = null;
            if (currentJob.hasTarget())
            {
                target = currentJob.getTarget();
            }
            // There is a valid target for this job //
            if (target != null)
            {
                // Can't set destination if agent is selected //
                if (agent.isActiveAndEnabled) { 
                    agent.SetDestination(target.transform.position);
                }
                
                // Get distance from agent to target //
                float magnitude = (transform.position - target.transform.position).magnitude;
                debug("MAGNITUDE - " + magnitude + " DISTANCE TO TARGET VALID - " + distanceToTargetValid);
                // If agent is close enough to be considered at arrived //
                if (magnitude < distanceToTargetValid)
                {
                    if (currentJob.isThisType(Job.JobType.DropOff))
                    {
                        debug("At dropoff site - " + target.name);
                        Item item = inventory.get(0);

                        GameObject itemGameObject = item.getGameObject();
                        if (target.GetComponent<BaseScript>().addItem(item))
                        {
                            MeshRenderer renderer = itemGameObject.GetComponent<MeshRenderer>();
                            renderer.enabled = false;
                            itemGameObject.transform.SetParent(target.transform);
                            inventory.removeItem(item);
                            jobQueue.completeCurrentJob(currentTask);
                        }
                        else
                        {
                            debug("Problem dropping off item for - " + getBeing().getName());
                        }
                    }
                    else if (currentJob.isThisType(Job.JobType.PickUp))
                    {
                        debug("Picking up - " + target.name);
                        // Try to add item to inventory //
                        if (inventory.addItem(target.GetComponent<RAKResource>().getAsItem()))
                        {
                            target.transform.SetParent(this.transform);
                            target.transform.position = this.transform.position + Vector3.forward * being.getCurrentSize() + Vector3.right * .1f;
                            target.transform.rotation = this.transform.rotation;
                            target.GetComponent<Collider>().enabled = false;
                            target.GetComponent<Rigidbody>().isKinematic = true;
                            
                            // Item added, clear target //
                            if (!DEBUG_RESOURCES_PERMANENT)
                            {
                                // If the agent only has one inventory slot, let's see it carrying the object //
                                if (inventory.getMaxInventorySize() > 1)
                                {
                                    MeshRenderer targetMesh = target.GetComponent<MeshRenderer>();
                                    targetMesh.enabled = false;
                                }
                            }
                            jobQueue.completeCurrentJob(currentTask);
                        }
                    }
                }
                // Too far away, cancel target //
                else if (magnitude > 20)
                {
                    currentTask.markComplete();
                }
            }
            if(currentTask.isThisTask(Tasks.TaskType.IDLE))
            {
                jobQueue.completeCurrentJob(currentTask);
            }
            // Object has just been released and is falling toward the ground //
            if (!agent.isActiveAndEnabled)
            {
                debug("Floor Y position - " + floorYPosition + " Critter-" + transform.position.y);
                // Is the agent close enough to the floor to reengage the mesh agent? //
                if (transform.position.y < floorYPosition)
                {
                    agent.enabled = true;
                    if (roomObject.isWaitingOnObjectMovement())
                    {
                        roomObject.toggleWaitingOnObjectMovement();
                    }
                    debug(gameObject.name + "- Agent enabled at y - " + transform.position.y);
                }
            }
            beingUpdates();
        }

        public void setBeing(IntelligentSpecies being)
        {
            this.being = being;
        }

        public IntelligentSpecies getBeing()
        {
            return being;
        }

        protected Task getCurrentTask()
        {
            return currentTask;
        }

        // Controls selection //
        public void setSelected(bool selected)
        {
            debug("Set selected called on " + gameObject.name + " with value - " + selected);
            // Clicked on this, and not waiting other objects to finish moving //
            // Check if the room already has a selection //
            if (selected && !roomObject.isWaitingOnObjectMovement())
            {
                // Room already has a selection that needs dropping //
                if (roomObject.isSomethingSelected())
                {
                    // Tells room to drop the current selection, can't do anything else until it's ready for a new one //
                    roomObject.dropSelection();
                }
                // Green light, set new selection and move character //
                else
                {
                    // Disable nav mesh, disable gravity, move agent to camera view //
                    this.selected = true;
                    roomObject.setSelection(this);
                    agent.enabled = false;
                    collider.attachedRigidbody.transform.position = viewToMoveTo.transform.position + viewToMoveTo.transform.forward * .5f;
                    collider.attachedRigidbody.useGravity = false;
                    collider.attachedRigidbody.Sleep();
                    collider.attachedRigidbody.isKinematic = true;
                    collider.attachedRigidbody.velocity = Vector3.zero;
                    agent.velocity = Vector3.zero;
                    //collider.attachedRigidbody.angularVelocity = new Vector3(90, 90, 90) * .5f;
                    // Disable personal view screen //
                    viewScreen.enabled = false;
                }
            }
            // Clicked on this, but room is waiting on an object to finish //
            else if (selected && roomObject.isWaitingOnObjectMovement())
            {
                debug(gameObject.name + " - Can't select, waiting on object");
            }
            // If called to unselect //
            if (!selected)
            {
                // Critter was selected and is not switching to not selected //
                if (this.selected)
                {
                    // Drop agent, agent will report when it hits ground and navmesh is enabled //
                    roomObject.dropSelection();
                    this.selected = false;
                    collider.attachedRigidbody.useGravity = true;
                    collider.attachedRigidbody.WakeUp();
                    //roomObject.toggleWaitingOnObjectMovement();
                    collider.attachedRigidbody.angularVelocity = Vector3.zero;
                    collider.attachedRigidbody.isKinematic = false;
                    if (roomObject.doDisplayPersonalViewScreens())
                    {
                        viewScreen.enabled = true;
                    }
                }
            }
        }

        // MOUSE CLICK //
        void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                setSelected(!selected);
            }
            else if(Input.GetMouseButton(1))
            {

                if (lastClickTime > 1)
                {
                    lastClickTime = 0;
                    //being.inpregnate();
                    being.getPhysicalBeing().getBody().getParts(true)[7].removeBodyPart(gameObject);
                }
            }
        }

        protected void debug(string message)
        {
            if (DEBUG_CRITTER)
            {
                Debug.Log(message);
            }
        }

        protected void debugVerbose(string message)
        {
            Debug.Log(message);
        }

        public bool isSelected()
        {
            return selected;
        }

        protected void beingUpdates()
        {
            if(currentTask.getTaskType() == Tasks.TaskType.NSFW) {
                if(new System.Random().Next(0,100) > being.getSocialAspects().getFrisky())
                {
                    if (being.canGetPregnant() && !being.isPregnant())
                    {
                        being.inpregnate();
                        Debug.Log("BABY TIME");
                    }
                }
            }
            float currentSize = being.getCurrentSize();
            // Did size change //
            if (transform.Find("Figure").localScale.x != currentSize)
            {
                transform.Find("Figure").localScale = new Vector3(currentSize, currentSize, currentSize);
                distanceToTargetValid = currentSize * distanceToTargetValidRatio;
                floorYPosition = currentSize * yFloorPositionToScaleRatio;
            }

        }

        protected void setDEBUG(bool debug)
        {
            this.DEBUG_CRITTER = debug;
        }
        public Inventory getInventory()
        {
            return inventory;
        }
        public void setInitialized(bool initialized)
        {
            this.initialized = initialized;
        }
        
    }

    public interface AgentInterface
    {
        // Initialize a new Being
        void initializeBeing(Agent child, GameObject gameObject, char gender, string name, IntelligentSpecies[] parents);
        // Agent birthing //
        void birth();
    }
}