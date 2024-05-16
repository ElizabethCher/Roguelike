using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationDat/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    public int nemberOfCrawlers; //����� ������
    public int iterationMin; //min ����� ������
    public int iterationMax; //max ����� ������
    
}
