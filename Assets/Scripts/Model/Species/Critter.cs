using rak.util;
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
        }
    }
}