using rak.being;
using UnityEngine;

namespace rak.equipment {

    public enum EquipmentType { ARMOR, WEAPON }
    public enum ArmorType {HEAD,SHOULDER,CHEST,ARMS,LEGS,FEET}
    public enum WeaponType { SINGLE,TWOHANDED,RANGED }
    public enum DamageType { SHARP,BLUNT}
    public enum Material { PICARDIUM,QUARKIUM,DOCTONE}

    public interface EquipmentInterface
    {
        
    }

    public class Equipment
    {
        protected Being[] canBeUsedBy;
        protected BodyPart.BodyPartLocation[] canBeEquippedTo;
        private EquipmentType equipmentType;
        private Material material;
        private new string name;
        private int weight;
        private int wear;

        public Equipment(Being[] canBeUsedBy,BodyPart.BodyPartLocation[] canBeEquippedTo,EquipmentType equipmentType,Material material,string name)
        {
            this.canBeUsedBy = canBeUsedBy;
            this.canBeEquippedTo = canBeEquippedTo;
            this.equipmentType = equipmentType;
            this.material = material;
            this.name = name;

            weight = 1; // Some calculation
            wear = 0;
        }

        protected bool canEquip(Equipment item, Being being)
        {
            bool compatibleBeing = false;
            foreach (Being oneBeing in canBeUsedBy)
            {
                if (oneBeing.getName() == being.getName())
                {
                    compatibleBeing = true;
                    break;
                }
            }
            if (!compatibleBeing)
            {
                return false;
            }
            return true;
        }
    }

    public class Weapon : Equipment,EquipmentInterface
    {
        public interface WeaponInterface
        {
            float getBluntDamage();
            float getSharpDamage();
        }

        private WeaponType weaponType;
        private DamageType damageType;

        public Weapon(Being[] canBeUsedBy,BodyPart.BodyPartLocation[] canBeEquippedTo,EquipmentType equipmentType,Material material,string name,
            WeaponType weaponType,DamageType damageType) :
            base(canBeUsedBy,canBeEquippedTo,equipmentType,material,name)
        {
            this.weaponType = weaponType;
            this.damageType = damageType;
        }
    }

    public class Armor : Equipment,EquipmentInterface
    {
        private ArmorType armorType;

        public Armor(Being[] canBeUsedBy, BodyPart.BodyPartLocation[] canBeEquippedTo, EquipmentType equipmentType, Material material, string name,
            ArmorType armorType) :
            base(canBeUsedBy, canBeEquippedTo, equipmentType, material, name)
        {
            this.armorType = armorType;
        }
    }
}