﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class Navigation : MonoBehaviour {

    public struct Coordinate2 {
        public int x;
        public int y;

        public Coordinate2(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Coordinate2 up { get { return new Coordinate2(x, y + 1); } }
        public Coordinate2 down { get { return new Coordinate2(x, y - 1); } }
        public Coordinate2 left { get { return new Coordinate2(x - 1, y); } }
        public Coordinate2 right { get { return new Coordinate2(x + 1, y); } }

        public Coordinate2 upLeft { get { return new Coordinate2(x - 1, y + 1); } }
        public Coordinate2 upRight { get { return new Coordinate2(x + 1, y + 1); } }
        public Coordinate2 downLeft { get { return new Coordinate2(x - 1, y - 1); } }
        public Coordinate2 downRight { get { return new Coordinate2(x + 1, y - 1); } }

        public override string ToString() {
            return string.Format("({0},{1})", x, y);
        }
    }

    public class PathfindingNode : Priority_Queue.FastPriorityQueueNode {
        private readonly Coordinate2 coordinate;
        public Coordinate2 Coordinate { get { return coordinate; } }
        private readonly Coordinate2 parent;
        public Coordinate2 Parent { get { return parent; } }
        private readonly float cost;
        public float Cost { get { return cost; } }

        public PathfindingNode(Coordinate2 coordinate, float cost, Coordinate2 parent) {
            this.coordinate = coordinate;
            this.parent = parent;
            this.cost = cost;
        }
    }

    Priority_Queue.SimplePriorityQueue<PathfindingNode> frontierQueue = new Priority_Queue.SimplePriorityQueue<PathfindingNode>();

    /// <summary>
    /// KeySpace of the dictionary is a set. Associated key is the AStar node with additional data.
    /// </summary>
    Dictionary<Coordinate2, PathfindingNode> traversedNodes = new Dictionary<Coordinate2, PathfindingNode>();

    void Start() {
        List<Coordinate2> path = pathToPlayer(new Coordinate2(0, 0), new Coordinate2(5, 5));
        foreach(Coordinate2 coordinate in path) {
            Debug.Log(coordinate);
        }
    }

    public List<Coordinate2> pathToPlayer(Coordinate2 source, Coordinate2 playerPosition) {
        return calculatePath(playerPosition, source);
    }

    private List<Coordinate2> calculatePath(Coordinate2 source, Coordinate2 destination) {
        //check if they're the same

        traversedNodes.Add(source, new PathfindingNode(source, 0, source));
        Expand(source, 0, destination);

        //check destination isn't already in traversedNodes

        while (frontierQueue.Count > 0) {
            Debug.Log(frontierQueue.Count);
            PathfindingNode nextNode = frontierQueue.Dequeue();

            //while the node dequeued was already expanded
            while(traversedNodes.ContainsKey(nextNode.Coordinate)) {
                if(frontierQueue.Count == 0) {
                    //no nodes left
                    return null;
                }

                nextNode = frontierQueue.Dequeue();
            }

            //add node to traversedNodes
            traversedNodes.Add(nextNode.Coordinate, nextNode);

            if(nextNode.Coordinate.Equals(destination)) {

                //path found. Create result
                List<Coordinate2> result = new List<Coordinate2>();
                while (!nextNode.Coordinate.Equals(source)) {
                    result.Add(nextNode.Coordinate);
                    nextNode = traversedNodes[nextNode.Parent];
                }
                result.Add(source);
                return result;
            } else {
                Expand(nextNode.Coordinate, nextNode.Cost, destination);
            }
        }
        //no nodes left
        return null;

        //DON'T clear frontierQueue and frontierSet, so they can be maintained while dest is still the player.
    }

    void Expand(Coordinate2 point, float pointCost, Coordinate2 destination) {
        TryAddNode(new PathfindingNode(point.up, pointCost + 1, point), destination);
        TryAddNode(new PathfindingNode(point.down, pointCost + 1, point), destination);
        TryAddNode(new PathfindingNode(point.left, pointCost + 1, point), destination);
        TryAddNode(new PathfindingNode(point.right, pointCost + 1, point), destination);

        TryAddNode(new PathfindingNode(point.upRight, pointCost + 1.41f, point), destination);
        TryAddNode(new PathfindingNode(point.upLeft, pointCost + 1.41f, point), destination);
        TryAddNode(new PathfindingNode(point.downRight, pointCost + 1.41f, point), destination);
        TryAddNode(new PathfindingNode(point.downLeft, pointCost + 1.41f, point), destination);
    }

    void TryAddNode(PathfindingNode node, Coordinate2 destination) {
        //check passable point
        if (!traversedNodes.ContainsKey(node.Coordinate)) {
            float priority = node.Cost + Mathf.Abs(destination.x - node.Coordinate.x) + Mathf.Abs(destination.y - node.Coordinate.y);
            frontierQueue.Enqueue(node, priority);
        }
    }

    private void LateUpdate() {
        traversedNodes.Clear();
        frontierQueue.Clear();
    }
}