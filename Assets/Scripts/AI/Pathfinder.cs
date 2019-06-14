using UnityEngine;
using System.Collections.Generic;

public class Pathfinder
{
    PathNode[,] _nodes;
    protected PathNode startNode;

    public Pathfinder(Actor actor, LevelController level) 
    {
        var grid = level.grid;
        _nodes = new PathNode[grid.size.width, grid.size.height];
        startNode = new PathNode(actor.position, grid.Height(actor.position), 0);

        PriorityQueue<PathNode> unexplored = new PriorityQueue<PathNode>();
        HashSet<Vector2Int> explored = new HashSet<Vector2Int>();

        unexplored.Enqueue(startNode);
        while (unexplored.Count > 0)
        {
            var current = unexplored.Dequeue();
            _nodes[current.position.x, current.position.y] = current;

            var neighbors = new Vector2Int[] {
                current.position + Vector2Int.up, 
                current.position + Vector2Int.down,
                current.position + Vector2Int.left, 
                current.position + Vector2Int.right
            };

            foreach(var position in neighbors) 
            {
                var dh = Mathf.Abs(grid.Height(position) - grid.Height(current.position));

                if (!explored.Contains(position) &&  
                    position.x >= 0 && position.x < grid.size.width &&
                    position.y >= 0 && position.y < grid.size.height &&
                    position.DistanceTo(actor.position) <= actor.movementRange &&
                    dh <= actor.jumpHeight ) 
                {
                    float weight = 0;
                    if (level.IsOccupied(position))
                        weight += actor.PathTroughCost(level.obstacles[position.x, position.y]);
                    else
                        weight += level.PathTroughCost(position);
                    
                    var node = new PathNode(position, grid.Height(position), weight);
                    node.startCost = current.startCost + weight + dh;
                    node.parent = current;
                    unexplored.Enqueue(node);
                }
            }

            explored.Add(current.position);
        }
    }

    public LinkedList<Vector2Int> FindPath(Vector2Int target) 
    {
        PathNode endNode = _nodes[target.x, target.y];
        
        if (endNode == null) return null;

        return RetracePath(startNode, endNode);
    }

    protected LinkedList<Vector2Int> RetracePath(PathNode startNode, PathNode endNode)
    {
        LinkedList<Vector2Int> path = new LinkedList<Vector2Int>();
        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            // Remove internal segment points for smooth animation
            //if (path.Count > 1 && currentNode.position.InLine(path.First.Next.Value, path.First.Value))
                // add height check here!
                //path.RemoveFirst();
            
            path.AddFirst(currentNode.position);
            currentNode = currentNode.parent;
        }
        return path;
    }

    public float[] ReachabilityList() {
        float[] map = new float[_nodes.GetLength(0) * _nodes.GetLength(1)];
        int w = _nodes.GetLength(0);
        foreach (var node in _nodes)
            if (node != null)
                map[node.position.x * w + node.position.y] = node.weight;
        return map;
    }
}
