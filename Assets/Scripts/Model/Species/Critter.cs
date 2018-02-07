using rak.util;
using System.Collections.Generic;
using UnityEngine;

namespace rak.being.species.critter
{
    public class Critter : Species
    {

        public Critter(string name,char gender,GameObject gameObject,Critter[] parents) : base(name,gender,gameObject)
        {
            this.gameObject = gameObject;

            // Species Specific //
            speciesSocialAspects = new SocialAspects(0, 0, 0, 0,1);
            speciesName = "Critter";
            gestationType = 'l'; // Live birth
            gestationTime = 5;
            reproductionType = 'a';
            canBePregnant = true;
            maxAge = 1000;
            growthToAgeRatio = .001;
            stopGrowingAt = 60;
            minSize = .2f;
            currentSize = minSize;
            navMeshAgentSpeed = 1.5f;
            name = Util.getRandomString("Critter") + " " + name;
            if (parents != null)
            {
                this.parents = parents;
            }
            BodyPart body = new BodyPart("Body",null);
            BodyPart head = new BodyPart("Head", body);

            BodyPart leftFrontLeg = new BodyPart("LeftArm", body);
            BodyPart leftFrontFoot = new BodyPart("LeftHand", leftFrontLeg);

            BodyPart rightFrontLeg = new BodyPart("RightArm", body);
            BodyPart rightFrontFoot = new BodyPart("RightHand", rightFrontLeg);

            BodyPart leftBackLeg = new BodyPart("LeftLeg", body);
            BodyPart leftBackFoot = new BodyPart("LeftFoot", leftBackLeg);

            BodyPart rightBackLeg = new BodyPart("RightLeg", body);
            BodyPart rightBackFoot = new BodyPart("RightFoot", rightBackLeg);

            List<BodyPart> newList = new List<BodyPart>();
            newList.Add(body);
            newList.Add(head);
            newList.Add(leftFrontLeg);
            newList.Add(leftFrontFoot);
            newList.Add(rightFrontLeg);
            newList.Add(rightFrontFoot);
            newList.Add(leftBackLeg);
            newList.Add(leftBackFoot);
            newList.Add(rightBackLeg);
            newList.Add(rightBackFoot);

            partsList = newList.ToArray();

            physicalBeing = new PhysicalBeing(partsList);
        }
    }
}