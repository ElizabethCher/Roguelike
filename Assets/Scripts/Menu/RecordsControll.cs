using TMPro;
using UnityEngine;

public class RecordsControll : MonoBehaviour
{
    static int record;
    public  TMP_Text recordText;
    private void Awake()
    { 
        record= PlayerPrefs.GetInt("RecordPlayerLevel");
       
    }
    private void Start()
    {
        record= PlayerPrefs.GetInt("RecordPlayerLevel");
        recordText.text =$"{record}";
    }
    private void Update()
    {
        recordText.text = $"{record}";
    }
    public static void RecordWrite()
    {
        record = PlayerPrefs.GetInt("RecordPlayerLevel");
            if (LevelUp.Level > record)
            {
                record = LevelUp.Level;

            }
  
        SaveRecord();

    }
    public static void SaveRecord()
    {
        PlayerPrefs.SetInt("RecordPlayerLevel", record);
        PlayerPrefs.Save();
    }

}
