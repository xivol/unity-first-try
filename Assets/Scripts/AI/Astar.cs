using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Astar
{
    PathNode[,] nodes;

    public Astar(float[,] map) 
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        nodes = new PathNode[width, height];

        for (int i = 0; i < width; ++i) {
            for (int j = 0; j < height; ++j)
            {
                nodes[i, j] = new PathNode(i, j, 0, map[i, j]);
            }
        }
    }

    public LinkedList<Vector2Int> FindPath(Vector2Int from, Vector2Int to)
    {
        PathNode startPathNode = nodes[from.x, from.y];
        PathNode targetPathNode = nodes[to.x, to.y];

        FindPath(startPathNode, targetPathNode);
        return null;
    }

    protected LinkedList<Vector2Int> FindPath(PathNode startNode, PathNode targetNode) 
    {
        List<PathNode> openSet = new List<PathNode>();
        HashSet<PathNode> closedSet = new HashSet<PathNode>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            PathNode currentNode = openSet[0];
            foreach (var node in openSet)
            {
                if (node.fullCost < currentNode.fullCost || 
                    node.fullCost == currentNode.fullCost && node.endCost < currentNode.endCost)
                {
                    currentNode = node;
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (var neighbour in GetNeighbours(currentNode))
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                    continue;

                float movementCost = currentNode.startCost + currentNode.DistanceTo(neighbour) * neighbour.weight;
                if (movementCost < neighbour.startCost || !openSet.Contains(neighbour))
                {
                    neighbour.startCost = movementCost;
                    neighbour.endCost = neighbour.DistanceTo(targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return null;
    }

    protected LinkedList<Vector2Int> RetracePath(PathNode startNode, PathNode endNode) 
    {
        LinkedList<Vector2Int>path = new LinkedList<Vector2Int>();
        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.AddFirst(currentNode.position);
            currentNode = currentNode.parent;
        }
        return path;
    }

    public List<PathNode> GetNeighbours(PathNode node)
    {
        List<PathNode> neighbours = new List<PathNode>();
        int width = nodes.GetLength(0);
        int height = nodes.GetLength(1);

        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.position.x + x;
                int checkY = node.position.y + y;

                if (checkX >= 0 && checkX < width && 
                    checkY >= 0 && checkY < height)
                {
                    neighbours.Add(nodes[checkX, checkY]);
                }
            }

        return neighbours;
    }

}
