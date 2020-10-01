using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : Singleton<GenericObjectPool<T>> where T : Component
{

    [SerializeField] T prefab = null;

    private Queue<T> objectsQueue = new Queue<T>();

    public virtual T GetObject()
    {
        if(objectsQueue.Count <= 0)
        {
            AddObjectToQueue();
        }

        T objectToReturn = objectsQueue.Dequeue();
        //objectToReturn.gameObject.SetActive(true);

        return objectToReturn;
    }
    public virtual void ReturnToPool(T returningObject)
    {
        returningObject.gameObject.SetActive(false);
        objectsQueue.Enqueue(returningObject);
    }

    private void AddObjectToQueue()
    {
        
        T newObject = GameObject.Instantiate(prefab);
        newObject.gameObject.SetActive(false);
        objectsQueue.Enqueue(newObject);

    }



}
