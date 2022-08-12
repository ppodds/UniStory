using System.IO;
using UnityEngine;
using WzComparerR2.WzLib;

public class Loader
{
    private static Loader _instance;

    public Wz_Node Character,
        Effect,
        Etc,
        Item,
        Map,
        Mob,
        Morph,
        Npc,
        Quest,
        Reactor,
        Skill,
        Sound,
        String,
        TamingMob,
        UI;

    private Loader()
    {
#if UNITY_EDITOR
        LoadAll(false);
#else
        LoadAll(true);
#endif
    }

    public static Loader getInstance()
    {
        return _instance ??= new Loader();
    }

    private Wz_Node Load(string name)
    {
        var path = Application.dataPath + "/wz/" + name + "/" + name + ".wz";
        if (!File.Exists(path)) throw new FileNotFoundException();
        var wzStructure = new Wz_Structure();
        wzStructure.LoadKMST1125DataWz(path);
        return wzStructure.WzNode;
    }

    private void LoadAll(bool useBase)
    {
        
        if (useBase)
        {
            var wzStructure = new Wz_Structure();
            var baseWzPath = Application.dataPath + "/wz/Base/Base.wz";
            wzStructure.LoadKMST1125DataWz(baseWzPath);
            var t = wzStructure.WzNode.Nodes;
            Character = t["Character"];
            Effect = t["Effect"];
            Etc = t["Etc"];
            Item = t["Item"];
            Map = t["Map"];
            Mob = t["Mob"];
            Morph = t["Morph"];
            Npc = t["Npc"];
            Quest = t["Quest"];
            Reactor = t["Reactor"];
            Skill = t["Skill"];
            Sound = t["Sound"];
            String = t["String"];
            TamingMob = t["TamingMob"];
            UI = t["UI"];
        }
        else
        {
            // Character = Load("Character");
            // Effect = Load("Effect");
            // Etc = Load("Etc");
            // Item = Load("Item");
            Map = Load("Map");
            // Mob = Load("Mob");
            // Morph = Load("Morph");
            // Npc = Load("Npc");
            // Quest = Load("Quest");
            // Reactor = Load("Reactor");
            // Skill = Load("Skill");
            Sound = Load("Sound");
            // String = Load("String");
            // TamingMob = Load("TamingMob");
            // UI = Load("UI");
        }
    }
}