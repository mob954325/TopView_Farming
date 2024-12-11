using System;
using UnityEngine;

public class Product : MonoBehaviour
{
    public Action OnDisableProduct;

    protected virtual void OnDisable()
    {
        OnDisableProduct?.Invoke();
    }
}