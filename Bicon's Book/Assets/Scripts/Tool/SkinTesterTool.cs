using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class SkinTesterTool : MonoBehaviour
{
    public SpriteMeshInstance[] SkinRenders;
    public SpriteMesh[] SkinMeshes1;
    public SpriteMesh[] SkinMeshes2;
    public SpriteMesh[] SkinMeshes3;
    public SpriteMesh[] SkinMeshes4;
    public SpriteMesh[] SkinMeshes5;
    [SerializeField]
    private Material material;
    public void GetAllSkinRenders()
    {
        Component[] components = GetComponentsInChildren(typeof(SpriteMeshInstance), true);
        SkinRenders = new SpriteMeshInstance[components.Length];
        SkinMeshes1 = new SpriteMesh[SkinRenders.Length];
        SkinMeshes2 = new SpriteMesh[SkinRenders.Length];
        SkinMeshes3 = new SpriteMesh[SkinRenders.Length];
        SkinMeshes4 = new SpriteMesh[SkinRenders.Length];
        SkinMeshes5 = new SpriteMesh[SkinRenders.Length];

        for (int i = 0; i < components.Length; i++)
        {
            bool Have = false;
            for (int j = 0; j < components.Length; j++)
            {
                CharacterSpriteMeshType type = (CharacterSpriteMeshType)j;
                if (type.ToString() == components[i].gameObject.name)
                {
                    SkinRenders[j] = components[i] as SpriteMeshInstance;
                    SkinMeshes1[j] = SkinRenders[j].spriteMesh;
                    Have = true;
                    SkinRenders[j].sharedMaterial = material;
                }
            }

            if(Have == false)
            {
                print(components[i].gameObject.name);
            }
        }

    }

    public void UseSkinMeshes1()
    {
        for (int i = 0; i < SkinRenders.Length; i++)
        {
            for (int j = 0; j < SkinRenders.Length; j++)
            {
                if (SkinMeshes1[j]!=null && SkinRenders[i]!=null && SkinMeshes1[j].name == SkinRenders[i].name)
                {
                    SkinRenders[i].spriteMesh = SkinMeshes1[j];
                }
            }
        }
    }
    public void UseSkinMeshes2()
    {
        for (int i = 0; i < SkinRenders.Length; i++)
        {
            for (int j = 0; j < SkinRenders.Length; j++)
            {
                if (SkinMeshes2.Length>j && SkinMeshes2[j].name == SkinRenders[i].name)
                {
                    SkinRenders[i].spriteMesh = SkinMeshes2[j];
                }
            }
        }
    }
    public void UseSkinMeshes3()
    {
        for (int i = 0; i < SkinRenders.Length; i++)
        {
            for (int j = 0; j < SkinRenders.Length; j++)
            {
                if (SkinMeshes3[j].name == SkinRenders[i].name)
                {
                    SkinRenders[i].spriteMesh = SkinMeshes3[j];
                }
            }
        }
    }
    public void UseSkinMeshes4()
    {
        for (int i = 0; i < SkinRenders.Length; i++)
        {
            for (int j = 0; j < SkinRenders.Length; j++)
            {
                if (SkinMeshes4[j].name == SkinRenders[i].name)
                {
                    SkinRenders[i].spriteMesh = SkinMeshes4[j];
                }
            }
        }
    }
    public void UseSkinMeshes5()
    {
        for (int i = 0; i < SkinRenders.Length; i++)
        {
            for (int j = 0; j < SkinRenders.Length; j++)
            {
                if (SkinMeshes5[j].name == SkinRenders[i].name)
                {
                    SkinRenders[i].spriteMesh = SkinMeshes5[j];
                }
            }
        }
    }

    public void ClearSkinMesh()
    {
        for (int i = 0; i < SkinRenders.Length; i++)
        {
            SkinRenders[i].spriteMesh = null;
        }
    }
}
