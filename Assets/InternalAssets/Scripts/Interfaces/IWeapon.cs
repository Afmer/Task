namespace Task.Interfaces
{
    public interface IWeapon : IWeaponDirection, IWeaponClip
    {
        public void Shoot();
    }
}
