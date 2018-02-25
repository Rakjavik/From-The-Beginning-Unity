using rak.work.tasks;
using UnityEngine;

namespace rak.being.psyche
{
    public enum PsycheNeedType { HAPPINESS,HUNGER,THIRST}

    public class Psyche
    {
        private Being being;
        private PsycheNeed[] needs;

        public Psyche(Being being)
        {
            this.being = being;
            needs = new PsycheNeed[3];
            needs[0] = new PsycheNeed(PsycheNeedType.HAPPINESS);
            needs[1] = new PsycheNeed(PsycheNeedType.HUNGER,0,100,50,.5f,Tasks.TaskType.EAT,1);
            needs[2] = new PsycheNeed(PsycheNeedType.THIRST);
        }

        public void update()
        {
            for(int count = 0; count < needs.Length; count++)
            {
                needs[count].degrade(Time.deltaTime);
            }
        }

        public float getCurrentValue(PsycheNeedType needType)
        {
            foreach(PsycheNeed need in needs)
            {
                if(need.getPsycheNeedType() == needType)
                {
                    return need.getCurrentValue();
                }
            }
            return -1f;
        }
    }

    public class PsycheNeed
    {
        private float currentValue;
        private int minValue;
        private int maxValue;
        private float degradesOvertime;
        private PsycheNeedType needType;
        private Tasks.TaskType taskRefills;
        private float taskRefillAmount;

        public PsycheNeed(PsycheNeedType needType)
        {
            initialize(needType, 0, 100, 100, 0, 0, 0);
        }
        public PsycheNeed(PsycheNeedType needType, int minValue, int maxValue, float currentValue, float degradesOvertime, Tasks.TaskType taskRefills, float taskRefillAmount)
        {
            initialize(needType, minValue, maxValue, currentValue, degradesOvertime, taskRefills, taskRefillAmount);
        }
        private void initialize(PsycheNeedType needType,int minValue,int maxValue,float currentValue,float degradesOvertime, Tasks.TaskType taskRefills,float taskRefillAmount)
        {
            this.needType = needType;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.currentValue = currentValue;
            this.degradesOvertime = degradesOvertime;
            this.taskRefills = taskRefills;
            this.taskRefillAmount = taskRefillAmount;
        }
        public void degrade(float timeDelta)
        {
            currentValue -= timeDelta * degradesOvertime;
        }
        public PsycheNeedType getPsycheNeedType()
        {
            return needType;
        }
        public float getCurrentValue()
        {
            return currentValue;
        }
    }
}