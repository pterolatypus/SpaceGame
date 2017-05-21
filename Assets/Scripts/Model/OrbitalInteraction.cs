namespace Model {
    public abstract class OrbitalInteraction {

        protected Orbital obj;

        public OrbitalInteraction(Orbital obj) {
            this.obj = obj;
        }

        public abstract InteractionTab GetTab();
    

    }
}
