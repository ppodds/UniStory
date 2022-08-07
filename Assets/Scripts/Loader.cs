using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MapleLib.WzLib;
using UnityEngine;

public class Loader
{
    private static Loader _instance;
    
    public WzFile Base,
        Character,
        Effect,
        Etc,
        Item,
        Map,
        // Map001,
        // Map002,
        // Map2,
        Mob,
        // Mob001,
        // Mob002,
        // Mob2,
        Morph,
        Npc,
        Quest,
        Reactor,
        Skill,
        // Skill001,
        // Skill002,
        // Skill003,
        Sound,
        // Sound001,
        // Sound002,
        // Sound2,
        String,
        TamingMob,
        UI;

    private Loader()
    {
        LoadAll();
    }

    public static Loader getInstance()
    {
        return _instance ??= new Loader();
    }
    
    private WzFile Load(string name)
    {
        var path = Application.dataPath + "/wz/" + name + ".wz";
        if (!File.Exists(path)) throw new FileNotFoundException();
        var wz = new WzFile(path, WzMapleVersion.EMS);
        wz.ParseWzFile();
        return wz;
    }
    
    private void LoadAll()
    {
        Base = Load("Base");
        Character = Load("Character");
        Effect = Load("Effect");
        Etc = Load("Etc");
        Item = Load("Item");
        Map = Load("Map");
        // Map001 = Load("Map001");
        // Map002 = Load("Map002");
        // Map2 = Load("Map2");
        Mob = Load("Mob");
        // Mob001 = Load("Mob001");
        // Mob002 = Load("Mob002");
        // Mob2 = Load("Mob2");
        Morph = Load("Morph");
        Npc = Load("Npc");
        Quest = Load("Quest");
        Reactor = Load("Reactor");
        Skill = Load("Skill");
        // Skill001 = Load("Skill001");
        // Skill002 = Load("Skill002");
        // Skill003 = Load("Skill003");
        Sound = Load("Sound");
        // Sound001 = Load("Sound001");
        // Sound002 = Load("Sound002");
        // Sound2 = Load("Sound2");
        String = Load("String");
        TamingMob = Load("TamingMob");
        UI = Load("UI");
    }

    // private void Awake()
    // {
    //     var t = new WzFile(Application.dataPath + "/wz/Map.wz", WzMapleVersion.EMS);
    //     t.ParseWzFile();
    //     t.WzDirectory.ParseDirectory();
    //     print(t.WzDirectory);
    //     print(t.header.Copyright);
    // }
}