using UnityEngine;

[CreateAssetMenu(menuName ="Cards/Plant Card", fileName ="New Plant Card")]

public class PlantCardScriptableObject : ScriptableObject
{
	public Texture plantIcon;
	public int cost;
	public float cooldown;
}
