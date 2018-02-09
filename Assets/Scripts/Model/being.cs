using rak.being.species;
using rak.being.species.critter;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace rak.being
{
    public class Being
    {
        protected PhysicalBeing physicalBeing;
        protected bool alive;
        protected double age;
        protected string name;
        protected char gender;
        protected bool canBePregnant;
        protected float currentSize;
        protected int maxAge;
        protected double growthToAgeRatio;
        protected float maxSize;
        protected float minSize;
        protected float navMeshAgentSpeed;

        protected Being[] parents;
        protected Being[] children;

        protected Being(string name,char gender)
        {
            this.name = name;
            this.gender = gender;
            if(gender == 'r')
            {
                if(Time.time % 1 == 0)
                {
                    gender = 'm';
                } else
                {
                    gender = 'f';
                }
            }
            parents = new Being[0];
            children = new Being[0];
            alive = true;
            age = 0;
            currentSize = (float)(age * growthToAgeRatio) + minSize;
        }

        public void addParent(Being newParent)
        {
            List<Being> parents = new List<Being>();
            parents.Add(newParent);
            if (this.parents != null)
            {
                for (int count = 0; count < this.parents.Length; count++)
                {
                    parents.Add(this.parents[count]);
                }
            }
            this.parents = parents.ToArray();
        }
        public void addChild(Being newChild)
        {
            List<Being> children = new List<Being>();
            children.Add(newChild);
            if (this.children != null)
            {
                for (int count = 0; count < this.children.Length; count++)
                {
                    children.Add(this.children[count]);
                }
            }
            this.children = children.ToArray();
        }

        public bool isAlive()
        {
            return alive;
        }
        public double getAge()
        {
            return age;
        }
        public string getName()
        {
            return name;
        }
        public char getGender()
        {
            return gender;
        }
        public void ageBeing(float time,float changeScaleEvery)
        {
            age += time;
            if(age > maxAge)
            {
                alive = false;
                Debug.Log(name + " has died of old age at - " + maxAge);
            }
            else if (currentSize < maxSize)
            {
                float newSize = (float)(age * growthToAgeRatio) + minSize;
                if (newSize > minSize && newSize - currentSize > changeScaleEvery)
                {
                    currentSize = newSize;
                }
            }
        }
        public bool canGetPregnant()
        {
            return canBePregnant;
        }
        public Being[] getParents()
        {
            return parents;
        }
        public Being[] getChildren()
        {
            return children;
        }
        public float getCurrentSize()
        {
            return currentSize;
        }
        public float getNavMeshAgent()
        {
            return navMeshAgentSpeed;
        }
        public PhysicalBeing getPhysicalBeing()
        {
            return physicalBeing;
        }
        public float getNavmeshAgentSpeed()
        {
            return navMeshAgentSpeed;
        }
        public void setNavmeshAgentSpeed(float speed)
        {
            this.navMeshAgentSpeed = speed;
        }

    }
}