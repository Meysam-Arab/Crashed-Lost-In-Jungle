using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveModel LoadSavedPlayerData()
    {
        if (!PlayerPrefs.HasKey("PlayerSaveData"))
        {
            SavePlayerData();
        }

        string saveJson = PlayerPrefs.GetString("PlayerSaveData");
        SaveModel save = JsonUtility.FromJson<SaveModel>(saveJson);

        return save;
    }

    public static void SavePlayerData()
    {
        SaveModel save = new SaveModel();

        PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        save.energy = pc.playerData.energy;
        save.level = GameController.instance.level;
        save.collectedMaps = pc.playerData.collectedMapCount;
        save.playerPrefabName = pc.playerData.prefabName;
        save.gameVersion = GameController.version;
        if(!string.IsNullOrEmpty(pc.playerData.equipedWeaponeTag))
        {
            save.equipedWeaponeTag = pc.playerData.equipedWeaponeTag;
        }
            

        ////save enemies tags and positions
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject enemy in enemies)
        //{
        //    save.enemyTags.Add(enemy.tag);
        //    save.enemyPositions.Add(enemy.transform.position);
        //}

        ////save blockades tags and positions
        //GameObject[] blockades = GameObject.FindGameObjectsWithTag("Blockade");
        //foreach (GameObject blockade in blockades)
        //{
        //    save.blockadeTags.Add(blockade.tag);
        //    save.blockadePositions.Add(blockade.transform.position);
        //}

        ////save energies tags and positions
        //GameObject[] energies = GameObject.FindGameObjectsWithTag("Food");
        //foreach (GameObject energy in energies)
        //{
        //    save.energyTags.Add(energy.tag);
        //    save.energyPositions.Add(energy.transform.position);
        //}


        //save.exitPosition = GameObject.FindGameObjectWithTag("Exit").transform.position;


        string saveJson = JsonUtility.ToJson(save);
        PlayerPrefs.SetString("PlayerSaveData", saveJson);
        PlayerPrefs.Save();

    }
}
