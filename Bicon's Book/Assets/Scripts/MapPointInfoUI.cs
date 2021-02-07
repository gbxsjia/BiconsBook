using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapPointInfoUI : MonoBehaviour
{
   static public MapPointInfoUI Instance;
    [SerializeField] TextMeshProUGUI MapNameText;
   [SerializeField] TextMeshProUGUI MapDiscriptionText;
   [SerializeField] List<Image> EqmOutline;
    [SerializeField] List<Image> EqmOnCharacterOutline;
    [SerializeField] List<Image> EqmRare;
    [SerializeField] GameObject RareParentObj;
    [SerializeField] Image MapImage;
    [SerializeField] Button SwichButtons;

    private void Start()
    {
        Instance = this;
    }

    bool IsEnemyImageFirst;
    public void OnMapPointPressed()
    {
        if (UIManager.CurrentMapButton != null)
        {
            if (UIManager.CurrentMapButton.thisPoint == ButtonType.Enemy)
            {
                RareParentObj.SetActive(true);
                
                if (UIManager.MapInfoOpen == true)
                {
                    gameObject.GetComponent<Animator>().Play("Enemy");
                }
                else
                {gameObject.GetComponent<Animator>().Play("ExpendToEnemy");

                }
                SwichButtons.interactable = true;
                IsEnemyImageFirst = true;
            }
            else
            {
                RareParentObj.SetActive(false);
                if (UIManager.MapInfoOpen == true)
                {
                    gameObject.GetComponent<Animator>().Play("Map");
                }
                else
                {gameObject.GetComponent<Animator>().Play("ExpendToMap");

                }
                SwichButtons.interactable = false;
                IsEnemyImageFirst = false;
            }
            UIManager.MapInfoOpen = true;
        }
    }
    public void SwitchImages()
    {
        if (UIManager.CurrentMapButton.thisPoint == ButtonType.Enemy)
        {
            if (IsEnemyImageFirst)
            {
                gameObject.GetComponent<Animator>().Play("SwitchToMapFront");
                IsEnemyImageFirst = false;
            }
            else
            {
                gameObject.GetComponent<Animator>().Play("SwitchToEnemyFront");
                IsEnemyImageFirst = true;
            }
        }
    }
    public void UpdateMapPointInfo()
    {
        MapImage.sprite = UIManager.CurrentMapButton.GetComponent<SpriteRenderer>().sprite;
        MapNameText.text = UIManager.CurrentMapButton.MapName;
        MapDiscriptionText.text = UIManager.CurrentMapButton.MapDiscription;
        EquipmentManager EnemyEqmManager = CharacterManager.EnemyInstance.GetComponent<EquipmentManager>();

        for(int i = 0; i < 16;i++)
        {
            if (EqmOutline[i] != null)
            {
                Equipment thisEquipment = EnemyEqmManager.GetEquipment((EquipmentType)i);
                if (thisEquipment != null)
                {
                    EqmRare[i].color = new Color(1,1,1);
                    if (thisEquipment.Rare == RareLevel.Normal)
                    {
                        EqmOutline[i].enabled = true;
                        EqmOutline[i].color = Color.white;
                        if (i > 1)
                        {
                            EqmOnCharacterOutline[i].enabled = true;
                            EqmOnCharacterOutline[i].color = new Color(1, 1, 1);
                        }
                    }
                    else if (thisEquipment.Rare == RareLevel.Rare)
                    {
                        EqmOutline[i].enabled = true;
                        EqmOutline[i].color = Color.green; if (i > 1)
                        {
                            EqmOnCharacterOutline[i].enabled = true;
                            EqmOnCharacterOutline[i].color = new Color(64f/255f, 1, 18f/255f);
                        }
                    }
                    else if (thisEquipment.Rare == RareLevel.Master)
                    {
                        EqmOutline[i].enabled = true;
                        EqmOutline[i].color = Color.blue; if (i > 1)
                        {
                            EqmOnCharacterOutline[i].enabled = true;
                            EqmOnCharacterOutline[i].color =  new Color(0, 103f/255f, 1);
                        }
                    }
                    else if (thisEquipment.Rare == RareLevel.Epic)
                    {
                        EqmOutline[i].enabled = true;
                        EqmOutline[i].color = new Color(0.5f,0,0.5f); if (i > 1)
                        {
                            EqmOnCharacterOutline[i].enabled = true;
                            EqmOnCharacterOutline[i].color = new Color(166f/255f, 0, 1); 
                        }
                    }
                    else if (thisEquipment.Rare == RareLevel.Legend)
                    {
                        EqmOutline[i].enabled = true;
                        EqmOutline[i].color = Color.yellow; if (i > 1)
                        {
                            EqmOnCharacterOutline[i].enabled = true;
                            EqmOnCharacterOutline[i].color = new Color(1, 196f/255f, 0);
                        }
                    }
                }
                else
                {
                    EqmRare[i].color = new Color(154f / 255f, 154f / 255f, 154f / 255f);
                    EqmOutline[i].enabled = false;
                    if (i > 1)
                    {
                        EqmOnCharacterOutline[i].enabled = false;
                    }
                    
                }
            }
        }
    }

}
