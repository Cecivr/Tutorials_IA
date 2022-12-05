using System.Collections.Generic;
using UnityEngine;

public class Node1 {

    public Node1 parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    // Constructor
    public Node1(Node1 parent, float cost, Dictionary<string, int> allStates, GAction action) {

        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allStates);
        this.action = action;
    }
}

public class GPlanner {

    public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates states) {

        List<GAction> usableActions = new List<GAction>();

        foreach (GAction a in actions) {

            if (a.IsAchievable()) {

                usableActions.Add(a);
            }
        }

        List<Node1> leaves = new List<Node1>();
        Node1 start = new Node1(null, 0.0f, GWorld.Instance.GetWorld().GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success) {

            Debug.Log("NO PLAN");
            return null;
        }
        
        Node1 cheapest = null;
        foreach (Node1 leaf in leaves) {

            if (cheapest == null) {

                cheapest = leaf;
            } else if (leaf.cost < cheapest.cost) {

                cheapest = leaf;
            }
        }
        List<GAction> result = new List<GAction>();
        Node1 n = cheapest;

        while (n != null) {

            if (n.action != null) {

                result.Insert(0, n.action);
            }

            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();

        foreach (GAction a in result) {

            queue.Enqueue(a);
        }

        Debug.Log("The Plan is: ");
        foreach (GAction a in queue) {

            Debug.Log("Q: " + a.actionName);
        }

        return queue;
    }

    private bool BuildGraph(Node1 parent, List<Node1> leaves, List<GAction> usableActions, Dictionary<string, int> goal) {

        bool foundPath = false;
        foreach (GAction action in usableActions) {

            if (action.IsAchievableGiven(parent.state)) {

                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);

                foreach (KeyValuePair<string, int> eff in action.effects) {

                    if (!currentState.ContainsKey(eff.Key)) {

                        currentState.Add(eff.Key, eff.Value);
                    }
                }

                Node1 node = new Node1(parent, parent.cost + action.cost, currentState, action);

                if (GoalAchieved(goal, currentState)) {

                    leaves.Add(node);
                    foundPath = true;
                } else {

                    List<GAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);

                    if (found) {

                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe) {

        List<GAction> subset = new List<GAction>();

        foreach (GAction a in actions) {

            if (!a.Equals(removeMe)) {

                subset.Add(a);
            }
        }
        return subset;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state) {

        foreach (KeyValuePair<string, int> g in goal) {

            if (!state.ContainsKey(g.Key)) {

                return false;
            }
        }
        return true;
    }
}
