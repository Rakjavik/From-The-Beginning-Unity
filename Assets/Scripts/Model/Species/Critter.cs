using rak.util;
using System.Collections.Generic;
using UnityEngine;

namespace rak.being.species.critter
{
    public class Critter : IntelligentSpecies
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
            growthToAgeRatio = .0005;
            maxSize = .5f;
            minSize = .2f;
            currentSize = minSize;
            navMeshAgentSpeed = .15f;
            name = Util.getRandomString("Critter") + " " + name;
            if (parents != null)
            {
                this.parents = parents;
            }
            BodyPart body = new BodyPart("Body",BodyPart.BodyPartLocation.BODY, null);
            BodyPart head = new BodyPart("Head",BodyPart.BodyPartLocation.HEAD, body);

            BodyPart leftFrontLeg = new BodyPart("LeftArm", BodyPart.BodyPartLocation.LEFTFRONTLEG, body);
            BodyPart leftFrontFoot = new BodyPart("LeftHand", BodyPart.BodyPartLocation.LEFTFRONTFOOT,leftFrontLeg);

            BodyPart rightFrontLeg = new BodyPart("RightArm", BodyPart.BodyPartLocation.RIGHTFRONTLEG, body);
            BodyPart rightFrontFoot = new BodyPart("RightHand", BodyPart.BodyPartLocation.RIGHTFRONTFOOT, rightFrontLeg);

            BodyPart leftBackLeg = new BodyPart("LeftLeg",BodyPart.BodyPartLocation.LEFTBACKLEG, body);
            BodyPart leftBackFoot = new BodyPart("LeftFoot", BodyPart.BodyPartLocation.LEFTBACKFOOT, leftBackLeg);

            BodyPart rightBackLeg = new BodyPart("RightLeg", BodyPart.BodyPartLocation.RIGHTBACKLEG, body);
            BodyPart rightBackFoot = new BodyPart("RightFoot", BodyPart.BodyPartLocation.RIGHTBACKFOOT, rightBackLeg);

            //

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