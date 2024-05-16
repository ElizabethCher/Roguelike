using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public static CameraControll instance;
    public Room currRoom;   //текущая комната
    public float moveSpeedWhenRoomChange;   //скорость с которой камра перемещается
    private void Awake()
    {
        instance= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }
    void UpdatePosition()
    {
        if (currRoom == null)
        {
            return;
        }
        Vector3 targetPos = GetCameraTargetPosition();//обновляем позицию
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime* moveSpeedWhenRoomChange);
    }
    Vector3 GetCameraTargetPosition()
    {
        if(currRoom==null)
        {
            return Vector3.zero;
        }
        Vector3 targetPos = currRoom.GetRoomCenter();
        targetPos.z = transform.position.z;
        return targetPos;
    }
    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false ;
    }
}
