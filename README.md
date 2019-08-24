# Flocking Algorithm in Unity

## Requirements

Unity 2019.x

## Overview

### Flocking Algorithm

The flocking behavior is achieved through a series of distinct behaviors which are computed at each step for each agent (a member of the flock).

- Alignment: Compels the agent to have a similar orientation to that of its neighbors.
- Avoidance: Compels the agent to move away from nearby agents if they are within a certain threshold.
- Cohesion: Compels the agent to move towards the midpoint of all nearby agents within a certain threshold.

The results of these behaviors is then adjusted via a weight and then applied to get the actual output for where the agent moves. Changing the weights of on the behavior outputs is what generates different looking flocks.

### Implementation

To achieve this in Unity I had one game object per flock and each agent was generated as a child of that game object. The flock game object handled the iteration through each of its agents as well as their instantation (agents are prefabs).

Each flock has a set of behaviors which can be changed beyond the traditional three outlined above. For instance, I created a simple "predator" behavior for a predator agent to hunt down and attack agents not in its flock. 

## Sources

[Board to Bits](https://youtube.com/channel/UCifiUB82IZ6kCkjNXN8dwsQ) for his original series on the topic.
[Craig Reynolds](https://www.red3d.com/cwr/boids/) for his original research into simulating flocking creatures.

## Media

![Two Agents Types Avoiding Obstacles](https://github.com/LBess/flocking-algorithm/master/Screenshots/2flocks_obstacles.gif?raw=true)
