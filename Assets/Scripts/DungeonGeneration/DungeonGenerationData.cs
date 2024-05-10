using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationDat/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    public int nemberOfCrawlers; //число сканов
    public int iterationMin; //min число комнат
    public int iterationMax; //max число комнат
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
