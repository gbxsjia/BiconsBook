using UnityEngine;
using System.Collections;

public class Ab_Popeye : Ability_Base
{
    private bool used = false;
    public override void OnAbilityEquip(CharacterManager thisCharacter)
    {
        base.OnAbilityEquip(thisCharacter);
        AbilityName = "麒麟臂";
        AbilityDescription = "双手增加36血量上限";
        if (!used)
        {
            UpGradeManager.instance.UpgradePart(2, 36);
            UpGradeManager.instance.UpgradePart(3, 36);
            used = true;
        }
        
    }

    public override void OnAbilityUnEquip()
    {
        base.OnAbilityUnEquip();
        if (used)
        {
            UpGradeManager.instance.UpgradePart(2, -36);
            UpGradeManager.instance.UpgradePart(3, -36);
            used = false;
        }

    }
}
