using NobleMuffins.LimbHacker.Guts;
using rak.being.species.critter;
using System.Collections.Generic;
using UnityEngine;

namespace rak.being
{
    public class PhysicalBeing
    {
        private Body body;

        public PhysicalBeing(BodyPart[] partsList)
        {
            body = new Body(partsList);
        }

        public Body getBody()
        {
            return body;
        }
    }

    public class Body
    {
        private BodyPart[] bodyParts;
        private GameObject gameObject;

        public Body(BodyPart[] partsList)
        {
            this.bodyParts = partsList;
        }
        public BodyPart[] getBodyParts()
        {
            return bodyParts;
        }
        public BodyPart[] getParts(bool attached)
        {
            List<BodyPart> bodyPartList = new List<BodyPart>();
            foreach (BodyPart part in bodyParts)
            {
                if(part.isAttached() == attached)
                {
                    bodyPartList.Add(part);
                }
            }
            return bodyPartList.ToArray();
        }
    }

    public class BodyPart
    {
        public enum BodyPartLocation {BODY,HEAD,LEFTFRONTLEG,LEFTFRONTFOOT,RIGHTFRONTLEG,RIGHTFRONTFOOT,LEFTBACKLEG,LEFTBACKFOOT,RIGHTBACKLEG,RIGHTBACKFOOT}

        private string name;
        private bool attached;
        private BodyPart parent;
        private List<BodyPart> children;
        private BodyPartLocation bodyPartLocation;

        public BodyPart(string name,BodyPartLocation bodyPartLocation,BodyPart parent)
        {
            this.name = name;
            this.bodyPartLocation = bodyPartLocation;
            this.parent = parent;
            attached = true;
        }

        public void addChild(BodyPart child)
        {
            children.Add(child);
        }

        public void removeBodyPart(GameObject gameObject)
        {
            attached = false;
            LimbHackerAgent.instance.SeverByJoint(gameObject, name, 0.0f, null);
            gameObject.GetComponent<CritterAgent>().lastSevered = this;
        }

        public string getName()
        {
            return name;
        }
        public bool isAttached()
        {
            return attached;
        }
    }
}