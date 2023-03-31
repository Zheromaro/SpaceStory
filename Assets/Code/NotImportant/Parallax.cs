using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.NotImportant
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private float choke;
        [SerializeField] private GameObject prefabEmptyBackground;
        [SerializeField] private parallaxBackgrounds[] backgroundEditor;
        private List<GameObject> backGrounds = new List<GameObject>();
        private Camera mainCamera;
        private Vector2 screenBounds;
        private Vector3 lastScreenPosition;

        private void Start()
        {
            generateBackgrounds();
            mainCamera = Camera.main;
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
            foreach (GameObject obj in backGrounds)
            {
                loadChildObjects(obj);
            }
            lastScreenPosition = mainCamera.transform.position;
        }

        private void LateUpdate()
        {
            foreach (GameObject obj in backGrounds)
            {
                repositionChildObjects(obj);
                float parallaxSpeed = 1 - Mathf.Clamp01(Mathf.Abs(mainCamera.transform.position.z / obj.transform.position.z));
                float difference = mainCamera.transform.position.x - lastScreenPosition.x;
                obj.transform.Translate(Vector3.right * difference * parallaxSpeed);

            }
            lastScreenPosition = mainCamera.transform.position;
        }

        private void loadChildObjects(GameObject obj)
        {
            float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
            int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
            GameObject clone = Instantiate(obj) as GameObject;
            for (int i = 0; i <= childsNeeded; i++)
            {
                GameObject c = Instantiate(clone) as GameObject;
                c.transform.SetParent(obj.transform);
                c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
                c.name = obj.name + i;
            }
            Destroy(clone);
            Destroy(obj.GetComponent<SpriteRenderer>());
        }

        private void repositionChildObjects(GameObject obj)
        {
            Transform[] children = obj.GetComponentsInChildren<Transform>();
            if (children.Length > 1)
            {
                GameObject firstChild = children[1].gameObject;
                GameObject lastChild = children[children.Length - 1].gameObject;
                float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
                if (mainCamera.transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth - choke)
                {
                    firstChild.transform.SetAsLastSibling();
                    firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
                }
                else if (mainCamera.transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
                {
                    lastChild.transform.SetAsFirstSibling();
                    lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
                }

            }
        }

        public void generateBackgrounds()
        {
            if (backgroundEditor == null)
            {
                Debug.LogWarning("There are no backgounds");
                return;
            }

            // Remove old element
            for (int i = 0; i < backGrounds.Count; i++)
            {
                Destroy(backGrounds[i]);
            }

            Transform parent = gameObject.transform;
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }

            backGrounds.Clear();

            // Add new element
            foreach (var background in backgroundEditor)
            {
                GameObject newObject = Instantiate(prefabEmptyBackground, gameObject.transform);
                newObject.transform.position = new Vector3(newObject.transform.position.x, newObject.transform.position.y, background.z);
                SpriteRenderer spriteRenderer = newObject.GetComponent<SpriteRenderer>();
                if (background.sprite != null)
                {
                    spriteRenderer.sprite = background.sprite;
                }
                else
                {
                    Debug.LogWarning("There are no sprit");
                }

                backGrounds.Add(newObject);
            }
        }

        public void generateBackgroundsForInspector()
        {
            if(backgroundEditor == null)
            {
                Debug.LogWarning("There are no backgounds");
                return;
            }

            // Remove old element
            for (int i = 0; i < backGrounds.Count; i++)
            {
                DestroyImmediate(backGrounds[i]);
            }

            Transform parent = gameObject.transform;
            foreach (Transform child in parent)
            {
                DestroyImmediate(child.gameObject);
            }

            backGrounds.Clear();

            // Add new element
            foreach (var background in backgroundEditor)
            {
                GameObject newObject = Instantiate(prefabEmptyBackground, gameObject.transform);
                newObject.transform.position = new Vector3(newObject.transform.position.x, newObject.transform.position.y, background.z);
                SpriteRenderer spriteRenderer = newObject.GetComponent<SpriteRenderer>();
                if(background.sprite != null)
                {
                    spriteRenderer.sprite = background.sprite;
                }
                else
                {
                    Debug.LogWarning("There are no sprit");
                }

                backGrounds.Add(newObject);
            }
        }

        public void saveBackgroundsNewStats()
        {
            for (int i = 0; i < backGrounds.Count; i++)
            {
                backgroundEditor[i].z = backGrounds[i].transform.position.z;
            }
        }

    }

    [System.Serializable]
    public class parallaxBackgrounds
    {
        public Sprite sprite;
        public float z;
    }

}