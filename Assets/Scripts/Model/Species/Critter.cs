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
            stopGrowingAt = 100;
            minSize = .1f;
            currentSize = minSize;
            navMeshAgentSpeed = 1.5f;
            name = Util.getRandomString("Critter") + " " + name;
            if (parents != null)
            {
                this.parents = parents;
            }
            BodyPart body = new BodyPart("Body",null);
            BodyPart head = new BodyPart("Head", body);

            BodyPart leftFrontLeg = new BodyPart("Left Front Leg", body);
            BodyPart leftFrontFoot = new BodyPart("Left Front Foot", leftFrontLeg);

            BodyPart rightFrontLeg = new BodyPart("Right Front Leg", body);
            BodyPart rightFrontFoot = new BodyPart("Right Front Foot", rightFrontLeg);

            BodyPart leftBackLeg = new BodyPart("Left Back Leg", body);
            BodyPart leftBackFoot = new BodyPart("Left Back Foot", leftBackLeg);

            BodyPart rightBackLeg = new BodyPart("Right Back Leg", body);
            BodyPart rightBackFoot = new BodyPart("Right Back Foot", rightBackLeg);

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