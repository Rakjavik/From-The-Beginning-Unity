using UnityEngine;

namespace rak.being.species
{

    public class Species : Being
    {
        protected SocialAspects speciesSocialAspects;

        protected GameObject gameObject;
        protected string speciesName;
        protected char reproductionType;
        protected double timePregnant;
        protected double gestationTime;
        protected char gestationType;

        protected bool waitingToGiveBirth = false;

        protected Species(string name,char gender,GameObject gameObject) : base(name,gender)
        {
            timePregnant = 0;
        }
        
        public char getReproductionType()
        {
            return reproductionType;
        }
        public double getTimePregnanant()
        {
            return timePregnant;
        }
        public double getGestationTime()
        {
            return gestationTime;
        }
        public string getSpeciesName()
        {
            return speciesName;
        }
        public bool isWaitingToGiveBirth()
        {
            return waitingToGiveBirth;
        }
        public void inpregnate()
        {
            if (timePregnant == 0)
            {
                timePregnant = 1;
            }
        }
        public bool isPregnant()
        {
            if(timePregnant > 0)
            {
                return true;
            }
            return false;
        }
        public void progressPregnany(float delta)
        {
            timePregnant += delta;
            if(timePregnant >= gestationTime)
            {
                timePregnant = 0;
                waitingToGiveBirth = true;
            }
        }
        public void birth()
        {
            waitingToGiveBirth = false;
        }
        public SocialAspects getSocialAspects()
        {
            return speciesSocialAspects;
        }
    }
}