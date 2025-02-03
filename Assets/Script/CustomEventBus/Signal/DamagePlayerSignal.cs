namespace Script.CustomEventBus.Signal
{
    public class DamagePlayerSignal
    {
        public int Damage;

        public DamagePlayerSignal(int damage)
        {
            Damage = damage;
        }
    }
}