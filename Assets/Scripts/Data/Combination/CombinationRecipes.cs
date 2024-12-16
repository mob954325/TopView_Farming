using System;
using System.Collections.Generic;
using UnityEngine;

public struct Combination
{
    public Combination(ItemCode first, ItemCode second)
    {
        firstCode = first;
        SecondCode = second;
    }

    public ItemCode firstCode;
    public ItemCode SecondCode;
}

public class CombinationRecipes : MonoBehaviour
{
    public Dictionary<Combination, ItemCode> recipe;

    private void Awake()
    {
        SetRecipeData();
    }

    private void SetRecipeData()
    {
        recipe = new Dictionary<Combination, ItemCode>();

        // key(두개의 아이템 코드), value(조합 완료된 아이템 코드)
        recipe.Add(new Combination(ItemCode.Damanged_Equipment, ItemCode.Scrap), ItemCode.Button);  // 파손된 장비 + 고철 = 버튼
        recipe.Add(new Combination(ItemCode.Scrap, ItemCode.RustyNail), ItemCode.Spike);            // 고철 + 녹슨 못    = 스파이크
    }

    /// <summary>
    /// 새로운 아이템으로 조합하는 함수 ( 레시피가 없으면 None코드로 반환)
    /// </summary>
    /// <param name="item1">아이템 코드 1</param>
    /// <param name="item2">아이템 코드 1이 아닌 아이템 코드</param>
    /// <returns>있으면 해당 레시피의 value값 없으면 None</returns>
    public ItemCode GetRecipeItem(ItemCode item1, ItemCode item2)
    {
        if (item1 == item2) return ItemCode.None;

        // 아이템 조합 찾기
        ItemCode result = ItemCode.None;
        Combination checkCombination = new Combination(item1, item2);

        foreach(KeyValuePair<Combination,ItemCode> combination in recipe)
        {
            if(CheckRecipe(checkCombination, combination.Key))
            {
                result = combination.Value;
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// 조합법이 존재하는지 확인하는 함수
    /// </summary>
    /// <param name="checkValue">확인할 아이템 조합</param>
    /// <param name="recipe">비교할 아이템 조합</param>
    /// <returns>있으면 true 아니면 false</returns>
    private bool CheckRecipe(Combination checkValue, Combination recipe)
    {
        bool result = false;

        // a b or b a 가 동일하면 true
        if((checkValue.firstCode == recipe.firstCode && checkValue.SecondCode == recipe.SecondCode)     // a b
        || (checkValue.firstCode == recipe.SecondCode && checkValue.SecondCode == recipe.firstCode))    // b a
        {
            result = true;
        }

        return result;
    }
}