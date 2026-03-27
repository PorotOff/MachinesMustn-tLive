using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour, IAttachable
{
    [SerializeField] private Transform _pivot;

    private Stack<Tile> _tiles = new Stack<Tile>();

    public Tile TopTile => _tiles.Peek();

    public void PickUp()
    {
        
    }

    public void PutDown()
    {
        
    }

    public void Attach()
    {
        throw new System.NotImplementedException();
    }

    public void Detach()
    {
        throw new System.NotImplementedException();
    }
}