using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ItemDataSO), true)]
[CanEditMultipleObjects]
public class ItemDataEditor : Editor
{
    private ItemDataSO data;

    private void OnEnable()
    {
        data = target as ItemDataSO; // target : 오브젝트 선택할 때 나오는 인스펙터?
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (data != null && data.Icon != null)
        {
            Texture2D texture;
            EditorGUILayout.LabelField("Item Icon Preview");    // 글 생성
            texture = AssetPreview.GetAssetPreview(data.Icon);

            if(texture != null)
            {
                GUILayout.Label("", GUILayout.Height(64), GUILayout.Width(64)); // 텍스쳐 크기
                GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);       // 텍스쳐 표시
            }
        }
    }
}
#endif