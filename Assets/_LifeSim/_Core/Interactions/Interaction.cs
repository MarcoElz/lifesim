using UnityEngine;
using System.Collections;
using LifeSim.Core.Items;

namespace LifeSim.Core.Interaction
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] int equippedIndex;
        [SerializeField] float minDistance;
        Player player;
        Inventory inventory;

        private void Awake()
        {
            player = GetComponent<Player>();
            inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            if (!player.IsControllable)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                ItemAction();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Interact();
            }
        }

        public void SetEquipItem(int index)
        {
            equippedIndex = index;
        }

        void ItemAction()
        {
            Item equipped = inventory.Get(equippedIndex).item;
            if (equipped == null)
                return;

            Debug.Log("Attemping to do action with equip item using: " + equipped.Name);

            RaycastHit hit = RayCastFromCamera();
            if (!IsNearToInteract(hit.point, transform.position))
                return;

            //TimedActions
            StartCoroutine(ActionWait(equipped, hit));
        }

        IEnumerator ActionWait(Item equipped, RaycastHit hit)
        {
            Actionable[] actions = hit.collider.GetComponents<Actionable>();
            if (actions != null)
            {
                Vector3 look = hit.point;
                look.y = transform.position.y;
                transform.LookAt(look);

                GetComponentInChildren<Animator>().SetTrigger("Action");
                GetComponent<Player>().IsControllable = false;

                yield return new WaitForSeconds(0.45f);

                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i].Execute(equipped, hit.point);
                }
            }
            
            yield return new WaitForSeconds(1.2f - 0.45f);
            GetComponent<Player>().IsControllable = true;
        }
        

        void Interact()
        {
            Debug.Log("Attemping to do an interaction");

            RaycastHit hit = RayCastFromCamera();
            if (!IsNearToInteract(hit.point, transform.position))
                return;
            Interactable[] interactions = hit.collider.GetComponentsInChildren<Interactable>();
            if (interactions != null && interactions.Length > 0)
            {
                for (int i = 0; i < interactions.Length; i++)
                {
                    interactions[i].Interact(hit.point);
                }
            }
        }

        private bool IsNearToInteract(Vector3 point, Vector3 player)
        {
            point.y = 0f;
            player.y = 0f;
            float d = Vector3.Distance(point, player);

            return d <= minDistance;
        }

        private RaycastHit RayCastFromCamera()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                return hit;
            }

            return hit;
        }
    }
}