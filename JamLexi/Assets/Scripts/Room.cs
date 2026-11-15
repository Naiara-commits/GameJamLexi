using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum Directions
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT,
        NONE,
    }

    [SerializeField]
    GameObject topWall;
    [SerializeField]
    GameObject rightWall;
    [SerializeField]
    GameObject bottomWall;
    [SerializeField]
    GameObject leftWall;

    Dictionary<Directions, GameObject> walls =
      new Dictionary<Directions, GameObject>();

    public Vector2Int Index
    {
        get;
        set;
    }

    public bool visited { get; set; } = false;

    private void Awake()
    {
        walls[Directions.TOP] = topWall;
        walls[Directions.RIGHT] = rightWall;
        walls[Directions.BOTTOM] = bottomWall;
        walls[Directions.LEFT] = leftWall;
    }

    private void SetActive(Directions dir, bool flag)
    {
        walls[dir].SetActive(flag);
    }

    public void SetDirFlag(Directions dir, bool flag)
    {
        SetActive(dir, flag);
    }

    public bool IsOpen(Directions dir)
    {
        return !walls[dir].activeSelf;
    }
}