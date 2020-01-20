using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Create GameConfig")]
public class GameConfig : ScriptableObject 
{
    public float PlayerForce;
    public float WallsSpeed;
    public float WallsLeftBorder;
    public float GravityScale;
    public float DistanceBetweenWalls;
    public float WallsStartPosition;
    public float MaxPassHieght;
    public float MinPassHieght;
    public float PassWidth;
    public int PointsForResumeGame;
    public GameObject PlayerPrefab;
    public GameObject WallPrefab;
}
