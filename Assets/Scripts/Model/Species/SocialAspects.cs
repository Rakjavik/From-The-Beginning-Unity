namespace rak.being.species
{
    public class SocialAspects
    {
        private int religion;
        private int primitive;
        private int warLike;
        private int peaceful;
        private int frisky;

        public SocialAspects(int religion,int primitive,int warLike,int peaceful,int frisky)
        {
            this.religion = religion;
            this.primitive = primitive;
            this.warLike = warLike;
            this.peaceful = peaceful;
            this.frisky = frisky;
        }

        public int getFrisky()
        {
            return frisky;
        }
    }
}