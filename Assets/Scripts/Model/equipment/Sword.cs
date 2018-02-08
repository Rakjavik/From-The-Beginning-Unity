using rak.being;

namespace rak.equipment
{
    public class Sword : Weapon,Weapon.WeaponInterface
    {
        public Sword(Being[] canBeUsedBy, BodyPart.BodyPartLocation[] canBeEquippedTo, EquipmentType equipmentType, Material material, string name,
            WeaponType weaponType, DamageType damageType) :
            base(canBeUsedBy, canBeEquippedTo, equipmentType, material, name,weaponType,damageType)
        {

        }

        public float getBluntDamage()
        {
            throw new System.NotImplementedException();
        }

        public float getSharpDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}