using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Reflection;

public static class RefrenceRepository
{
    //prefabs
    public static string Axe_Prefab_Refrence = "Prefabs/Items/Weapones/Axe";
    public static string Pickaxe_Prefab_Refrence = "Prefabs/Items/Weapones/Pickaxe";
    public static string Spear_Prefab_Refrence = "Prefabs/Items/Weapones/Spear";
    public static string Meat_Prefab_Refrence = "Prefabs/Items/Energy/Meat";
    public static string Coconut_Prefab_Refrence = "Prefabs/Items/Energy/Coconut";
    public static string Banana_Prefab_Refrence = "Prefabs/Items/Energy/Banana";
    public static string Pineapple_Prefab_Refrence = "Prefabs/Items/Energy/Pineapple";

    public static string Player_1_Prefab_Refrence = "Prefabs/Players/Player_1";
    public static string Player_2_Prefab_Refrence = "Prefabs/Players/Player_2";
    public static string Player_3_Prefab_Refrence = "Prefabs/Players/Player_3";
    public static string Player_4_Prefab_Refrence = "Prefabs/Players/Player_4";
    public static string Player_5_Prefab_Refrence = "Prefabs/Players/Player_5";
    public static string Player_6_Prefab_Refrence = "Prefabs/Players/Player_6";
    public static string Player_7_Prefab_Refrence = "Prefabs/Players/Player_7";
    public static string Player_8_Prefab_Refrence = "Prefabs/Players/Player_8";
    public static string Player_9_Prefab_Refrence = "Prefabs/Players/Player_9";
    public static string Player_10_Prefab_Refrence = "Prefabs/Players/Player_10";

    //prefab names
    public static string Player_1_Prefab_Name = "Player_1";
    public static string Player_2_Prefab_Name = "Player_2";
    public static string Player_3_Prefab_Name = "Player_3";
    public static string Player_4_Prefab_Name = "Player_4";
    public static string Player_5_Prefab_Name = "Player_5";
    public static string Player_6_Prefab_Name = "Player_6";
    public static string Player_7_Prefab_Name = "Player_7";
    public static string Player_8_Prefab_Name = "Player_8";
    public static string Player_9_Prefab_Name = "Player_9";
    public static string Player_10_Prefab_Name = "Player_10";

    //animators
    public static string Map1_Animator_Refrence = "Animators/Map1AnimatorController";
    public static string Map2_Animator_Refrence = "Animators/Map2AnimatorController";
    public static string Map3_Animator_Refrence = "Animators/Map3AnimatorController";
    public static string Map4_Animator_Refrence = "Animators/Map4AnimatorController";
    public static string Map5_Animator_Refrence = "Animators/Map5AnimatorController";
    public static string Map6_Animator_Refrence = "Animators/Map6AnimatorController";
    public static string Map7_Animator_Refrence = "Animators/Map7AnimatorController";
    public static string Map8_Animator_Refrence = "Animators/Map8AnimatorController";
    public static string Map9_Animator_Refrence = "Animators/Map9AnimatorController";
    public static string Map10_Animator_Refrence = "Animators/Map10AnimatorController";


    //sprites
    public static string Damage_Blood_1_Sprite_Refrence = "Sprites/5_ Gui/Bloods/Red_Blood/Blood_1";
    public static string Damage_Blood_2_Sprite_Refrence = "Sprites/5_ Gui/Bloods/Red_Blood/Blood_2";
    public static string Damage_Blood_3_Sprite_Refrence = "Sprites/5_ Gui/Bloods/Red_Blood/Blood_3";
    public static string Damage_Blood_4_Sprite_Refrence = "Sprites/5_ Gui/Bloods/Red_Blood/Blood_4";
    public static string Damage_Blood_5_Sprite_Refrence = "Sprites/5_ Gui/Bloods/Red_Blood/Blood_5";
    public static string Damage_Blood_6_Sprite_Refrence = "Sprites/5_ Gui/Bloods/Red_Blood/Blood_6";
    public static string Damage_Blood_7_Sprite_Refrence = "Sprites/5_ Gui/Bloods/Red_Blood/Blood_7";
    public static string Damage_Blood_8_Sprite_Refrence = "Sprites/5_ Gui/Bloods/Red_Blood/Blood_8";

    public static string Banana_Sprite_Refrence = "Sprites/2_ Items/1_ Foods/Banana_1_1";
    public static string Coconut_Sprite_Refrence = "Sprites/2_ Items/1_ Foods/Coconut_1_1";
    public static string Meat_Sprite_Refrence = "Sprites/2_ Items/1_ Foods/Meat_1_1";
    public static string Pineapple_Sprite_Refrence = "Sprites/2_ Items/1_ Foods/Pineapple_1_1";

    public static string Axe_Sprite_Refrence = "Sprites/2_ Items/2_ ToolsAndWeapons/Axe_On_Ground_1";
    public static string Pickaxe_Sprite_Refrence = "Sprites/2_ Items/2_ ToolsAndWeapons/Pickaxe_On_Ground_1";
    public static string Spear_Sprite_Refrence = "Sprites/2_ Items/2_ ToolsAndWeapons/Spear_On_Ground_1";

    public static string Bear_Sprite_Refrence = "Sprites/4_ Enemies/1_ Animals/Bear_1_1";
    public static string Gorilla_Sprite_Refrence = "Sprites/4_ Enemies/1_ Animals/Gorilla_1_1";
    public static string Jaguar_Sprite_Refrence = "Sprites/4_ Enemies/1_ Animals/Jaguar_1_1";
    public static string Lion_Sprite_Refrence = "Sprites/4_ Enemies/1_ Animals/Lion_1_1";
    public static string Lioness_Sprite_Refrence = "Sprites/4_ Enemies/1_ Animals/Lion_2_1";
    public static string Tiger_Sprite_Refrence = "Sprites/4_ Enemies/1_ Animals/Tiger_1_1";
    public static string Monkey_Sprite_Refrence = "Sprites/4_ Enemies/1_ Animals/Monkey_1_1";

    public static string Cannibal_Sprite_Refrence = "Sprites/4_ Enemies/2_ Cannibals/Cannibal_1_8";


    //sounds
    public static string Game_Over_Lose_Sound_Name = "GameOver";
    public static string Game_Over_Lose_Sound_Address = "Sounds/Game/Miscellaneous/";

    public static string Eat_Food_Sound_Name = "ConsumeFood";
    public static string Eat_Food_Sound_Address = "Sounds/Game/Miscellaneous/";

    public static string Hype_Sound_Name = "Hype";
    public static string Hype_Sound_Address = "Sounds/Game/Miscellaneous/";

    public static string Winner_Sound_Name = "Winner";
    public static string Winner_Sound_Address = "Sounds/Game/Miscellaneous/";

    public static UnityEngine.Object GetPrefabByTag(string prefabTag)
    {
        //UnityEngine.Object go =
        if (prefabTag == WeaponModel.TAG_WEAPON_AXE)
            return Resources.Load(Axe_Prefab_Refrence);
        else if (prefabTag == WeaponModel.TAG_WEAPON_PICKAXE)
            return Resources.Load(Pickaxe_Prefab_Refrence);
        else if (prefabTag == WeaponModel.TAG_WEAPON_SPEAR)
            return Resources.Load(Spear_Prefab_Refrence);
        else if (prefabTag == EnergyModel.TAG_ENERGY_MEAT)
            return Resources.Load(Meat_Prefab_Refrence);
        else if (prefabTag == EnergyModel.TAG_ENERGY_BANANA)
            return Resources.Load(Banana_Prefab_Refrence);
        else if (prefabTag == EnergyModel.TAG_ENERGY_COCONUT)
            return Resources.Load(Coconut_Prefab_Refrence);
        else if (prefabTag == EnergyModel.TAG_ENERGY_PINEAPPLE)
            return Resources.Load(Pineapple_Prefab_Refrence);
        else
            return null;
    }

    public static UnityEngine.Object GetPrefabByRefrence(string prefabRefrence)
    {
        //UnityEngine.Object go =
        if (prefabRefrence == Axe_Prefab_Refrence)
            return Resources.Load(Axe_Prefab_Refrence);
        else if (prefabRefrence == Pickaxe_Prefab_Refrence)
            return Resources.Load(Pickaxe_Prefab_Refrence);
        else if (prefabRefrence == Spear_Prefab_Refrence)
            return Resources.Load(Spear_Prefab_Refrence);
        else if (prefabRefrence == Meat_Prefab_Refrence)
            return Resources.Load(Meat_Prefab_Refrence);
        else if (prefabRefrence == Pineapple_Prefab_Refrence)
            return Resources.Load(Pineapple_Prefab_Refrence);
        else if (prefabRefrence == Banana_Prefab_Refrence)
            return Resources.Load(Banana_Prefab_Refrence);
        else if (prefabRefrence == Coconut_Prefab_Refrence)
            return Resources.Load(Coconut_Prefab_Refrence);
        else
            return null;
    }

    public static UnityEngine.Object GetMapAnimationByIndex(int index)
    {
        //UnityEngine.Object go =
        if (index == 1)
            return Resources.Load(Map1_Animator_Refrence);
        else if (index == 2)
            return Resources.Load(Map2_Animator_Refrence);
        else if (index == 3)
            return Resources.Load(Map3_Animator_Refrence);
        else if (index == 4)
            return Resources.Load(Map4_Animator_Refrence);
        else if (index == 5)
            return Resources.Load(Map5_Animator_Refrence);
        else if (index == 6)
            return Resources.Load(Map6_Animator_Refrence);
        else if (index == 7)
            return Resources.Load(Map7_Animator_Refrence);
        else if (index == 8)
            return Resources.Load(Map8_Animator_Refrence);
        else if (index == 9)
            return Resources.Load(Map9_Animator_Refrence);
        else if (index == 10)
            return Resources.Load(Map10_Animator_Refrence);
        else
            return null;
    }

    public static Sprite GetSpriteByRefrence(string spriteRefrence)
    {
     
        if (spriteRefrence == Damage_Blood_1_Sprite_Refrence)
            return Resources.Load<Sprite>(Damage_Blood_1_Sprite_Refrence);
        else if (spriteRefrence == Damage_Blood_2_Sprite_Refrence)
            return Resources.Load<Sprite>(Damage_Blood_2_Sprite_Refrence);
        else if (spriteRefrence == Damage_Blood_3_Sprite_Refrence)
            return Resources.Load<Sprite>(Damage_Blood_3_Sprite_Refrence);
        else if (spriteRefrence == Damage_Blood_4_Sprite_Refrence)
            return Resources.Load<Sprite>(Damage_Blood_4_Sprite_Refrence);
        else if (spriteRefrence == Damage_Blood_5_Sprite_Refrence)
            return Resources.Load<Sprite>(Damage_Blood_5_Sprite_Refrence);
        else if (spriteRefrence == Damage_Blood_6_Sprite_Refrence)
            return Resources.Load<Sprite>(Damage_Blood_6_Sprite_Refrence);
        else if (spriteRefrence == Damage_Blood_7_Sprite_Refrence)
            return Resources.Load<Sprite>(Damage_Blood_7_Sprite_Refrence);
        else if (spriteRefrence == Damage_Blood_8_Sprite_Refrence)
            return Resources.Load<Sprite>(Damage_Blood_8_Sprite_Refrence);

        else if (spriteRefrence == Banana_Sprite_Refrence)
            return Resources.Load<Sprite>(Banana_Sprite_Refrence);
        else
            return null;
    }

    public static Sprite GetSpriteByTag(string spriteTag)
    {

        if (spriteTag == EnergyModel.TAG_ENERGY_BANANA)
            return Resources.Load<Sprite>(Banana_Sprite_Refrence);
        else if (spriteTag == EnergyModel.TAG_ENERGY_COCONUT)
            return Resources.Load<Sprite>(Coconut_Sprite_Refrence);
        else if (spriteTag == EnergyModel.TAG_ENERGY_MEAT)
            return Resources.Load<Sprite>(Meat_Sprite_Refrence);
        else if (spriteTag == EnergyModel.TAG_ENERGY_PINEAPPLE)
            return Resources.Load<Sprite>(Pineapple_Sprite_Refrence);

        else if (spriteTag == WeaponModel.TAG_WEAPON_AXE)
            return Resources.Load<Sprite>(Axe_Sprite_Refrence);
        else if (spriteTag == WeaponModel.TAG_WEAPON_PICKAXE)
            return Resources.Load<Sprite>(Pickaxe_Sprite_Refrence);
        else if (spriteTag == WeaponModel.TAG_WEAPON_SPEAR)
            return Resources.Load<Sprite>(Spear_Sprite_Refrence);

        else if (spriteTag == EnemyModel.TAG_ENEMY_BEAR)
            return Resources.Load<Sprite>(Bear_Sprite_Refrence);
        else if (spriteTag == EnemyModel.TAG_ENEMY_CANNIBAL)
            return Resources.Load<Sprite>(Cannibal_Sprite_Refrence);
        else if (spriteTag == EnemyModel.TAG_ENEMY_GORILLA)
            return Resources.Load<Sprite>(Gorilla_Sprite_Refrence);
        else if (spriteTag == EnemyModel.TAG_ENEMY_JAGUAR)
            return Resources.Load<Sprite>(Jaguar_Sprite_Refrence);
        else if (spriteTag == EnemyModel.TAG_ENEMY_LION)
            return Resources.Load<Sprite>(Lion_Sprite_Refrence);
        else if (spriteTag == EnemyModel.TAG_ENEMY_LIONESS)
            return Resources.Load<Sprite>(Lioness_Sprite_Refrence);
        else if (spriteTag == EnemyModel.TAG_ENEMY_TIGER)
            return Resources.Load<Sprite>(Tiger_Sprite_Refrence);
        else if (spriteTag == EnemyModel.TAG_ENEMY_MONKEY)
            return Resources.Load<Sprite>(Monkey_Sprite_Refrence);

        else
            return null;
    }


    public static UnityEngine.Object GetPrefabByName(string prefabName)
    {
        //UnityEngine.Object go =
        if (prefabName == Player_1_Prefab_Name)
            return Resources.Load(Player_1_Prefab_Refrence);
        else if (prefabName == Player_2_Prefab_Name)
            return Resources.Load(Player_2_Prefab_Refrence);
        else if (prefabName == Player_3_Prefab_Name)
            return Resources.Load(Player_3_Prefab_Refrence);
        else if (prefabName == Player_4_Prefab_Name)
            return Resources.Load(Player_4_Prefab_Refrence);
        else if (prefabName == Player_5_Prefab_Name)
            return Resources.Load(Player_5_Prefab_Refrence);
        else if (prefabName == Player_6_Prefab_Name)
            return Resources.Load(Player_6_Prefab_Refrence);
        else if (prefabName == Player_7_Prefab_Name)
            return Resources.Load(Player_7_Prefab_Refrence);
        else if (prefabName == Player_8_Prefab_Name)
            return Resources.Load(Player_8_Prefab_Refrence);
        else if (prefabName == Player_9_Prefab_Name)
            return Resources.Load(Player_9_Prefab_Refrence);
        else if (prefabName == Player_10_Prefab_Name)
            return Resources.Load(Player_10_Prefab_Refrence);
        else
            return null;
    }

    public static AudioClip GetAudioByName(string soundName)
    {
        string address = null;

        if (soundName == Game_Over_Lose_Sound_Name)
            address = Game_Over_Lose_Sound_Address + Game_Over_Lose_Sound_Name;

        else if (soundName == Eat_Food_Sound_Name)
            address = Eat_Food_Sound_Address + Eat_Food_Sound_Name;

        else if (soundName == Hype_Sound_Name)
            address = Hype_Sound_Address + Hype_Sound_Name;

        else if (soundName == Winner_Sound_Name)
            address = Winner_Sound_Address + Winner_Sound_Name;

        AudioClip go = Resources.Load<AudioClip>(address);
        return go;
    }

    public static Sprite GetRandomBloodSprite()
    {

        int index = UnityEngine.Random.Range(1, 9);
        Sprite damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_1_Sprite_Refrence);

        if (index == 1)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_1_Sprite_Refrence);
        else if (index == 2)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_2_Sprite_Refrence);
        else if (index == 3)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_3_Sprite_Refrence);
        else if (index == 4)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_4_Sprite_Refrence);
        else if (index == 5)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_5_Sprite_Refrence);
        else if (index == 6)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_6_Sprite_Refrence);
        else if (index == 7)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_7_Sprite_Refrence);
        else if (index == 8)
            damageSprite = RefrenceRepository.GetSpriteByRefrence(RefrenceRepository.Damage_Blood_8_Sprite_Refrence);
        return damageSprite;
    }
}


