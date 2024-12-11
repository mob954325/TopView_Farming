using System.Collections.Generic;
using UnityEngine;

public class Factory<T> : MonoBehaviour
{
    private List<Product> products;
    private Queue<Product> readyQueue;

    public Product product;
    public int capacity;

    private void Awake()
    {
        products = new List<Product>(capacity);
        readyQueue = new Queue<Product>(capacity);

        for(int i = 0;  i < capacity; i++)
        {
            Product spawnedProduct = Instantiate(product.gameObject, this.transform).GetComponent<Product>();
            products.Add(spawnedProduct);
            readyQueue.Enqueue(spawnedProduct);
            spawnedProduct.gameObject.SetActive(false);
        }
    }

    protected T SpawnProduct(Vector3 pos, Quaternion rot)
    {
        if(readyQueue.Count <= 0)
        {
            IncreaseCapacity();
        }

        Product spawnedProduct = readyQueue.Dequeue();
        spawnedProduct.transform.position = pos;
        spawnedProduct.transform.rotation = rot;
        spawnedProduct.gameObject.SetActive(true);

        spawnedProduct.OnDisableProduct += () => { readyQueue.Enqueue(spawnedProduct); };

        return spawnedProduct.GetComponent<T>();
    }

    private void IncreaseCapacity()
    {
        int preCapacity = capacity;
        List<Product> temp = new List<Product>(capacity);

        for(int i = 0; i < temp.Capacity; i++)
        {
            temp.Add(products[i]);
        }

        capacity *= 2;

        products = new List<Product>(capacity);
        for(int i = 0; i < preCapacity; i++)
        {
            products.Add(temp[i]);
        }

        for(int i = preCapacity; i < capacity; i++)
        {
            Product spawnedProduct = Instantiate(product.gameObject, this.transform).GetComponent<Product>();
            products.Add(spawnedProduct);
            readyQueue.Enqueue(spawnedProduct);
            spawnedProduct.gameObject.SetActive(false);
        }
    }
}