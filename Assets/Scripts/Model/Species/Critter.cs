using rak.util;
using UnityEngine;

namespace rak.being.species.critter
{
    public class Critter : Species
    {

        public Critter(string name,char gender,GameObject gameObject,Critter[] parents,bool canBePregnant) : base(name,gender,gameObject,canBePregnant)
        {
            this.gameObject = gameObject;

            // Species Specific //
            speciesSocialAspects = new SocialAspects(0, 0, 0, 0,0);
            speciesName = "Critter";
            gestationType = 'l'; // Live birth
            gestationTime = 5;
            reproductionType = 'a';
            maxAge = 1000;
            growthToAgeRatio = .005;
            stopGrowingAt = 200;
            minSize = .2f;
            currentSize = minSize;
            navMeshAgentSpeed = 1.5f;
            name = Util.getRandomString("Critter") + " " + name;
            if (parents == null)
            {
                
            } else
            {
                foreach(Critter critter in parents)
                {
                    addParent(critter);
                }
            }
        }
    }
}