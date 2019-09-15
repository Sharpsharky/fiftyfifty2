using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColdCry.Utility
{
    public class GeneralObjectPool : IEnumerable<GameObject>
    {
        private Transform parent;
        private List<GameObject> pooledObjects;

        public GeneralObjectPool(int size, string parentName = null)
        {
            if (size <= 0) {
                throw new System.ArgumentException( "Size cannot be 0 or less" );
            }
            pooledObjects = new List<GameObject>( size );
            if (parentName != null) {
                parent = new GameObject( parentName ).transform;
            }
        }

        public GeneralObjectPool(int size, Transform parent)
        {
            if (size <= 0) {
                throw new System.ArgumentException( "Size cannot be 0 or less" );
            }
            pooledObjects = new List<GameObject>( size );
            this.parent = parent;
        }

        public void Add(GameObject gameObject)
        {
            gameObject.SetActive( false );
            if (parent != null) {
                gameObject.transform.parent = parent;
            }
            pooledObjects.Add( gameObject );
        }

        public GameObject Get()
        {
            foreach (GameObject pooledObject in pooledObjects) {
                if (!pooledObject.gameObject.activeInHierarchy) {
                    pooledObject.gameObject.SetActive( true );
                    return pooledObject;
                }
            }
            return null;
        }

        public GameObject GetRandom()
        {
            if (pooledObjects.Count == 0)
                return null;

            int index;
            GameObject gameObject = null;
            do {
                index = UnityEngine.Random.Range( 0, pooledObjects.Count );
                gameObject = pooledObjects[index];
            } while (gameObject.activeInHierarchy);

            gameObject.SetActive( true );
            return gameObject;
        }

        public GameObject Get(Vector2 position)
        {
            return Get( position, Quaternion.identity );
        }

        public GameObject Get(Vector2 position, Quaternion rotation)
        {
            foreach (GameObject pooledObject in pooledObjects) {
                if (!pooledObject.gameObject.activeInHierarchy) {
                    pooledObject.gameObject.SetActive( true );
                    pooledObject.transform.position = position;
                    pooledObject.transform.rotation = rotation;
                    return pooledObject;
                }
            }
            return null;
        }

        public void Return(GameObject obj)
        {
            if (!pooledObjects.Contains( obj ))
                throw new System.ArgumentException( "Object doesn't belong to pool" );
            obj.gameObject.SetActive( false );
        }

        public void ReturnToParent(GameObject obj)
        {
            if (parent)
                obj.transform.parent = parent;
            Return( obj );
        }

        public void Clear()
        {
            foreach (GameObject t in pooledObjects) {
                GameObject.Destroy( t.gameObject );
            }
            if (parent) {
                GameObject.Destroy( parent );
            }
            pooledObjects.Clear();
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            return pooledObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (GameObject t in pooledObjects) {
                yield return t;
            }
        }

        public int Size { get => pooledObjects.Count; }
        public Transform Parent { get => parent; }
    }

}