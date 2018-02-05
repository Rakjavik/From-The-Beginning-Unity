namespace rak.being.psyche
{
    public class Psyche
    {
        private PsycheNeed happiness;
        private PsycheNeed hunger;
        private PsycheNeed thirst;

        public Psyche()
        {
            happiness = new PsycheNeed();
            hunger = new PsycheNeed();
            thirst = new PsycheNeed();
        }
    }

    public class PsycheNeed
    {
        private float currentValue;
        private int baseValue;
        private float degradesOvertime;
    }
}