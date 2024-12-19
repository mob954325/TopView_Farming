using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombinationPanelUI : MonoBehaviour
{
    // 오른쪽 아이콘
    // 왼쪽 아이콘
    // 종료 버튼
        // 모든 칸 비우고 창 닫기
    // 확인 버튼
        // 조합하기
            // 결과 창 띄우고 인벤토리에 새로운 아이템 추가

    private LocalManager manager;
    private CombinationRecipes recipes;

    private CanvasGroup canvas;

    private Image icon_Left;
    private Image icon_Right;

    private ItemDataSO data_Left;
    private int leftIndex;
    private ItemDataSO data_Right;
    private int rightIndex;

    private Button cancelButton;
    private Button comfirmButton;

    private TextMeshProUGUI messageText;

    private void Awake()
    {
        manager = FindAnyObjectByType<LocalManager>();
        recipes = FindAnyObjectByType<CombinationRecipes>();
        canvas = GetComponent<CanvasGroup>();    

        Transform contentPanel = transform.GetChild(0);
        icon_Left = contentPanel.GetChild(0).GetComponent<Image>();
        icon_Right = contentPanel.GetChild(1).GetComponent<Image>();

        cancelButton = contentPanel.GetChild(2).GetComponent<Button>();
        comfirmButton = contentPanel.GetChild(3).GetComponent<Button>();

        messageText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        cancelButton.onClick.AddListener(() =>
        {
            SetDeactive();
        });

        comfirmButton.onClick.AddListener(() =>
        {
            ItemCode resultCode = recipes.GetRecipeItem(data_Left.code, data_Right.code);

            if (resultCode != ItemCode.None)
            {
                // 조합에 사용한 아이템 제거
                manager.Player.Inventory.DiscardItems(leftIndex);
                manager.Player.Inventory.DiscardItems(rightIndex);
                manager.Player.Inventory.AddItem(manager.ItemDataManager.Items[(int)resultCode]);
                SetDeactive();
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ShowMessageProcess());
            }
        });

        messageText.text = " ";
        SetDeactive();
    }

    private IEnumerator ShowMessageProcess()
    {
        float timeElpased = 0f;

        while(timeElpased < 2f)
        {
            timeElpased += Time.deltaTime;
            messageText.text = "No Recipe";
            yield return null;
        }
        messageText.text = " ";
    }

    public bool SetData(ItemDataSO data, int index)
    {
        bool result = false;

        if (data_Left == null)
        {
            data_Left = data;
            icon_Left.sprite = data.Icon;
            leftIndex = index;

            result = true;
        }
        else if (data_Left != null && data_Right == null)
        {
            data_Right = data;
            icon_Right.sprite = data.Icon;
            rightIndex = index;

            result = true;
        }

        return result;
    }

    public void SetActive()
    {
        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }

    public void SetDeactive()
    {
        canvas.alpha = 0f;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;

        ClearData();
    }

    private void ClearData()
    {
        data_Left = null;
        data_Right = null;
        leftIndex = -1;

        icon_Left.sprite = null;
        icon_Right.sprite = null;
        rightIndex = -1;
    }
}
