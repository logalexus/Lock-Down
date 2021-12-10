using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenSpawner : MonoBehaviour
{
    [SerializeField] private Citizen _citizen;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Sprite _doorClose;
    [SerializeField] private Sprite _doorOpen;




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
            var door = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            instance.transform.position = door.position;
            door.GetComponent<SpriteRenderer>().sprite = _doorOpen;
            yield return new WaitForSeconds(1);
            door.GetComponent<SpriteRenderer>().sprite = _doorClose;
            yield return new WaitForSeconds(5);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Citizen c))
            Destroy(collision.gameObject);
    }


}
