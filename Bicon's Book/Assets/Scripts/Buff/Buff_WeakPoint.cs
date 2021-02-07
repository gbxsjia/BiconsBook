using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_WeakPoint : Buff_base
{
    public Buff_WeakPoint(BuffType type, int remains, int amount) : base(type, remains, amount)
    {
        Type = type;
        Remains = remains;
        Amount = amount;
        BuffName = "破绽";
        BuffDescription = "受到攻击时，伤害增加35%";
    }

    GameObject effectInstance;
    // public static event System.Action WeakPointsEvent;
    public override void AddToManager(BuffManager buffManager, BodyPart bodyPart)
    {
        base.AddToManager(buffManager, bodyPart);
        bodyPart.OnDamageFloatEvent += BodyPart_OnDamageEvent;
        effectInstance = ArtResourceManager.instance.GenerateVFXObject("WeaknessBuff", bodyPart.transform.position, Quaternion.identity);
        if (OwnerBuffManager.characterManager.camp == 1)
        {
            OwnerBuffManager.a_WeakPointsEvent();
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
        AttachToBodyPart.OnDamageFloatEvent -= BodyPart_OnDamageEvent;
        GameObject.Destroy(effectInstance);
        ArtResourceManager.instance.GenerateVFXObject("WeaknessRemove", AttachToBodyPart.transform.position, Quaternion.identity);
        CharacterManager.PlayerInstance.GetComponent<AbilityManager>().WeakPointRemoved = true;
    }

   // public static event System.Action DestroyWeakPointsEvent;
    private float BodyPart_OnDamageEvent(float damage)
    {
        if (OwnerBuffManager.characterManager.camp == 1)
        {
            OwnerBuffManager.a_DestroyWeakPointsEvent();
        }

        if (!isRemoving)
        {
            OwnerBuffManager.RemoveBuff(this);
        }    
        return damage + 0.35f;
    }
}
