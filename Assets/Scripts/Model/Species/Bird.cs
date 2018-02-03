using rak.util;
using UnityEngine;

namespace rak.being.species
{
    public class Bird : Species
    {
        public Bird(string name, char gender, GameObject gameObject, Bird[] parents) : base(name, gender, gameObject)
        {
            this.gameObject = gameObject;

            // Species Specific //
            speciesSocialAspects = new SocialAspects(0, 0, 0, 0, 0);
            speciesName = "Phoenix";
            gestationType = 'e'; // Eggs
            gestationTime = 36000;
            reproductionType = 'h';
            if (gender == 'f')
            {
                canBePregnant = true;
            } else
            {
                canBePregnant = false;
            }
            maxAge = 86400;
            growthToAgeRatio = .0005;
            stopGrowingAt = 360;
            minSize = .1f;
            currentSize = minSize;
            navMeshAgentSpeed = 3.0f;
            name = Util.getRandomString("Phoenix") + " " + name;
            if (parents == null)
            {

            }
            else
            {
                foreach (Bird bird in parents)
                {
                    addParent(bird);
                }
            }
        }
    }
}