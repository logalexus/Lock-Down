using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenSpawner : MonoBehaviour
{
    [SerializeField] private Citizen _citizen;
    [SerializeField] private List<GameObject> _spawnDoors;
    [SerializeField] private List<RuntimeAnimatorController> _citizensAnimators;
    private void Start()
    {
        GameController.Instance.StartGame += ()=>
        {
            StartCoroutine(Spawn());
        };
        Player.Instance.BeginInteract += () => StopAllCoroutines();
        Player.Instance.CompleteInteract += () => StartCoroutine(Spawn());

    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            var instance = Instantiate(_citizen);
            SetCitizenAnimation(instance);

            var door = _spawnDoors[Random.Range(0, _spawnDoors.Count)];
            instance.transform.position = new Vector2(door.transform.position.x, door.transform.position.y - 2);

            Animator doorAnimator = door.GetComponent<Animator>();
            doorAnimator.SetBool("isOpen", true);
            yield return new WaitForSeconds(1);
            doorAnimator.SetBool("isOpen", false);
            yield return new WaitForSeconds(5);
        }
    }

    private void SetCitizenAnimation(Citizen citizen)
    {
        Animator citizenAnimator = citizen.GetComponent<Animator>();
        citizenAnimator.runtimeAnimatorController = _citizensAnimators[Random.Range(0, _citizensAnimators.Count)];
        citizenAnimator.SetBool("isMove", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Citizen c))
            Destroy(collision.gameObject);
    }


}
