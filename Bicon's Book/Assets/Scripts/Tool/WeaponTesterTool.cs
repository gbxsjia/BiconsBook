#if UNITY_EDITOR

using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class WeaponTesterTool : MonoBehaviour
{
    SpriteRenderer[][] LeftRendererTypeList;
    SpriteRenderer[][] RightRendererTypeList;
   
   public GameObject LeftHandParent;
   public GameObject RightHandParent;

    public WeaponList m_LeftWeapon;
    public WeaponList m_RightWeapon;

    public void ExchangeHands()
    {
        LeftRendererTypeList = new SpriteRenderer[LeftHandParent.transform.childCount][];
        RightRendererTypeList = new SpriteRenderer[LeftHandParent.transform.childCount][];

        List<GameObject> AllObjList = new List<GameObject>();
        List<bool> AllObjActiveList = new List<bool>();


        for (int i = 0; i < LeftHandParent.transform.childCount; i++)
        {

            AllObjList.Add(LeftHandParent.transform.GetChild(i).gameObject);
            AllObjList.Add(RightHandParent.transform.GetChild(i).gameObject);

            LeftRendererTypeList[i] = new SpriteRenderer[LeftHandParent.transform.GetChild(i).childCount];
            RightRendererTypeList[i] = new SpriteRenderer[RightHandParent.transform.GetChild(i).childCount];

            for (int y = 0; y < LeftHandParent.transform.GetChild(i).childCount; y++)
            {
                LeftRendererTypeList[i][y] = LeftHandParent.transform.GetChild(i).GetChild(y).GetComponent<SpriteRenderer>();
                RightRendererTypeList[i][y] = RightHandParent.transform.GetChild(i).GetChild(y).GetComponent<SpriteRenderer>();
            }


            for(int x = 0; x < LeftRendererTypeList.Length; x++ )
            {
                AllObjList.Add(LeftRendererTypeList[i][x].gameObject);
                AllObjList.Add(RightRendererTypeList[i][x].gameObject);
            }

        }

        

        foreach(GameObject a in AllObjList)
        {
            if(a.active == true)
            { AllObjActiveList.Add(true); }
            else
            { AllObjActiveList.Add(false); }
            
            a.SetActive(true);
        }

        List<Sprite>[] LeftSpritesTpye = new List<Sprite>[LeftHandParent.transform.childCount];
        List<Sprite>[] RightSpritesTpye = new List<Sprite>[RightHandParent.transform.childCount];

        for( int i = 0; i <  LeftSpritesTpye.Length;i++)
        {
            LeftSpritesTpye[i] = new List<Sprite>();
            RightSpritesTpye[i] = new List<Sprite>();


            for (int x = 0; x < LeftRendererTypeList[i].Length;x++)
            {
               LeftSpritesTpye[i].Add(LeftRendererTypeList[i][x].sprite);
               RightSpritesTpye[i].Add(RightRendererTypeList[i][x].sprite);
            }         
        }


        for(int x = 0; x < LeftRendererTypeList.Length;x++)
        {

           for(int y = 0; y < LeftRendererTypeList[x].Length;y++)
            {
                LeftRendererTypeList[x][y].sprite = RightSpritesTpye[x][y];
                RightRendererTypeList[x][y].sprite = LeftSpritesTpye[x][y];
            }
        }

        for(int i = 0; i < AllObjList.Count;i++)
        {
            if(AllObjActiveList[i] == false)
            {
                AllObjList[i].SetActive(false);
            }
        }
    }

    public void ChangeWeapons()
    {
        foreach(Texture2D a in allTex2d)
        {
            if(a.name == m_LeftWeapon.ToString())
            {
                Sprite s = Sprite.Create(a, new Rect(0, 0, a.width, a.height), Vector2.one/2);
                LeftHandParent.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = s;
            }
            if (a.name == m_RightWeapon.ToString())
            {
                Sprite s = Sprite.Create(a, new Rect(0, 0, a.width, a.height), Vector2.one/2);
                RightHandParent.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = s;
            }
        }
    }
    public void GenWeaponPngEnum()
    {
        load();

        List<string> ImageNameList = new List<string>();
        for(int i = 0; i < allTex2d.Count;i++)
        {
            ImageNameList.Add(allTex2d[i].name);
        }

        var arg = "";
        foreach (var PngName in ImageNameList)
        {
            arg += "\t" + PngName + ",\n";
        }
        var res = "public enum WeaponList\n{\n" + arg + "}\n";
        var path = Application.dataPath + "/Scripts/Tool/WeaponList.cs";
        File.WriteAllText(path, res, Encoding.UTF8);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


    List<Texture2D> allTex2d = new List<Texture2D>();

    void load()
    {
        allTex2d = new List<Texture2D>();
        List<string> filePaths = new List<string>();
        string imgtype = "*.PNG";

        string[] dirs = Directory.GetFiles(Application.dataPath + "/ArtResource/weapon", imgtype);
        for (int j = 0; j < dirs.Length; j++)
        {
            filePaths.Add(dirs[j]);
        }


        for (int i = 0; i < filePaths.Count; i++)
        {
            Texture2D tx = new Texture2D(100, 100);
            tx.name = Path.GetFileNameWithoutExtension(filePaths[i]);
            tx.LoadImage(getImageByte(filePaths[i]));
            allTex2d.Add(tx);
        }

    }

    private static byte[] getImageByte(string imagePath)
    {
        FileStream files = new FileStream(imagePath, FileMode.Open);
        byte[] imgByte = new byte[files.Length];
        files.Read(imgByte, 0, imgByte.Length);
        files.Close();
        return imgByte;
    }

}
#endif