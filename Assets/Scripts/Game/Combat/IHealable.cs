namespace Combat
{
    public interface IHealable 
    {
        void Heal(float amount);
        void MaxHeal();
    }
}