using UnityEngine;

public interface IInteractable
{
    public bool CanInteract { get; set; }

    /// <summary>
    /// 상호작용 시 호출되는 함수
    /// </summary>
    public void OnInteract();
}
