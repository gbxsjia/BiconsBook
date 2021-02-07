using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Equipment/Weapon")]
public class EM_Weapon : Equipment
{
    public Sprite[] weaponSprite = new Sprite[12];
    public WeaponType weaponType;

    public int WeaponDamage;
    public int WeaponSpecial;
}
public enum WeaponType
{
    Blade,
    Spear,
    Sword,
    Hammer,
    Shield,
    Crossbow,
    Dagger
}