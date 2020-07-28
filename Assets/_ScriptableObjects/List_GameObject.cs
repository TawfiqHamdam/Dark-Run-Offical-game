using System.Collections.Generic;
using UnityEngine;

namespace DarkRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "List_GameObject",
                     menuName = "CLYKI/ScriptableObject/List_GameObject")]
    public class List_GameObject : ScriptableObject
    {
        [SerializeField] List<GameObject> value = new List<GameObject>();

        public bool Contains(GameObject value) { return this.value.Contains(value); }
        public int Count() { return value.Count; }
        public GameObject Value(int index) { return value[index]; }

#if UNITY_EDITOR
        public void Add(GameObject value)
        {
            switch (value == null) { case true: return; }
            switch (this.value.Contains(value)) { case true: return; }
            this.value.Add(value);
            CleanUp();
        }
        public void Remove(GameObject value)
        {
            switch (this.value.Contains(value)) { case false: return; }
            this.value.Remove(value);
            CleanUp();
        }
        private void CleanUp()
        {
            for (int i = 0; i < value.Count; i++)
            {
                switch (value[i] == null) { case false: continue; }
                value.Remove(value[i]);
            }
        }
        public void Scrub() { value = new List<GameObject>(); }
#endif
    }
}