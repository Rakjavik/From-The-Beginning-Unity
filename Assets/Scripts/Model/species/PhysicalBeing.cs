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

        public Body(BodyPart[] partsList)
        {
            this.bodyParts = partsList;
        }
        public BodyPart[] getBodyParts()
        {
            return bodyParts;
        }
        public BodyPart[] getDetachedParts()
        {
            List<BodyPart> bodyPartList = new List<BodyPart>();
            foreach (BodyPart part in bodyParts)
            {
                if(!part.isAttached())
                {
                    bodyPartList.Add(part);
                }
            }
            return bodyPartList.ToArray();
        }
    }

    public class BodyPart
    {
        private string name;
        private bool attached;
        private List<RAKBodyPart> bodyPartGameObject;
        private BodyPart parent;
        private List<BodyPart> children;

        public BodyPart(string name,BodyPart parent)
        {
            this.name = name;
            this.parent = parent;
            attached = true;
            bodyPartGameObject = new List<RAKBodyPart>();
        }

        public void addChild(BodyPart child)
        {
            children.Add(child);
        }

        public void removeBodyPart(bool freezeLimbs)
        {
            for(int count = 0; count < bodyPartGameObject.Count; count++)
            {
                bodyPartGameObject[count].detach(freezeLimbs);
            }
            attached = false;
        }

        public string getName()
        {
            return name;
        }
        public bool isAttached()
        {
            return attached;
        }
        public void addBodyPartGameObject(RAKBodyPart bodyPart)
        {
            bodyPartGameObject.Add(bodyPart);
        }
    }
}