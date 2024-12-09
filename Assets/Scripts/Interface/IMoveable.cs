using Unity.VisualScripting;
using UnityEngine;

public interface IMoveable
{
    public float Speed { get; set; }

    /// <summary>
    /// 움직일 때 실행되는 함수
    /// </summary>
    /// <param name="moveDir">움직이는 방향</param>
    /// <returns>움직이려는 위치</returns>
    public void OnMove(Vector3 moveDir);
}
