using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDrop : MonoBehaviour
{


    [SerializeField] Animator thisAnimator;
    [SerializeField] SpriteRenderer thisDropWeaponRenderer;

    private void Start()
    {
        InGameManager.instance.ReturnToCampEvent += OnReturnCamp;
    }

    public void OnReturnCamp()
    {
        InGameManager.instance.ReturnToCampEvent -= OnReturnCamp;
        Destroy(gameObject);
    }
    public void StartThis(CharacterManager thisCharacter, EM_Weapon thisWeapon, DropPartType dropPartType, DropAnm dropAnm)
    {
        StartCoroutine(StartDrop(thisCharacter, thisWeapon, dropPartType, dropAnm));
    }
    public IEnumerator StartDrop(CharacterManager thisCharacter,EM_Weapon thisWeapon,DropPartType dropPartType,DropAnm dropAnm)
    {
        yield return new WaitForSeconds(0.01f);
        if (dropAnm == DropAnm.WeaponDeathDrop)
        {
            SpriteRenderer thisWeaponRenderer = thisCharacter.GetWeaponRenderer(dropPartType == DropPartType.RightWeapon, 0)[0];
            thisDropWeaponRenderer.sprite = thisWeaponRenderer.sprite;
            gameObject.transform.position = new Vector3(thisWeaponRenderer.transform.position.x, thisWeaponRenderer.transform.position.y, 0);
            Vector3 thisWeaponRendererScale = thisWeaponRenderer.transform.lossyScale;
            gameObject.transform.localScale = new Vector3 (Mathf.Abs(thisWeaponRendererScale.x), Mathf.Abs(thisWeaponRendererScale.y), Mathf.Abs(thisWeaponRendererScale.z));
            gameObject.transform.eulerAngles = new Vector3(0,thisCharacter.camp * 180,-85f);
            thisDropWeaponRenderer.sortingLayerID = thisWeaponRenderer.sortingLayerID;
            print(thisDropWeaponRenderer.sortingLayerID);

            if(dropPartType == DropPartType.RightWeapon)
            {
             thisDropWeaponRenderer.sortingOrder = 0;
              
            }
            else
            {
                  thisDropWeaponRenderer.sortingOrder = 60;
                thisDropWeaponRenderer.sortingLayerName = "AnimationEffects";
            }

            thisAnimator.Play(dropAnm.ToString());
        }
        yield return null;
    }
}
