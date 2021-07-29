using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    List<Monster> _monsters;
    [SerializeField]
    string _nextLevelName;
    // Update is called once per frame

    public bool MonstersAllDead { get
        {
            return _monsters.TrueForAll((monster) => monster.HasDied);
        }
    }
    void Update()
    {
        if (MonstersAllDead && !string.IsNullOrEmpty(_nextLevelName)) {
            SceneManager.LoadScene(_nextLevelName);
        }
    }
}
