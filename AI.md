# AI Animation Behaviours Overview
This document lists each AI animation behaviour and its animator dependencies.

-----------------
## INavigator Parameters
- (bool) Target_IsAudible: Is an enemy within the hearing radius?
- (bool) Target_InSight: Is an enemy visible within the "Total" FOV area?
- (bool) Target_InFocus: Is an enemy visible within the "Focused" FOV area?
- (bool) CanPatrol: Does the INavigator have more than one waypoint?

-----------------
## AI_Search
### Description
Heads towards the last point of interest of the INavigator.
### Behaviour Locals
- (float) stoppingDistance: NavMeshAgent stopping distance for this state behaviour 

## AI_Patrol
### Description
Heads towards closest waypoint on entry, 
### Behaviour Locals
- (int) targetWaypoint: Index of last waypoint, used to get next waypoint

## AI_FollowTarget
### Description

### Behaviour Locals
- (float) stoppingDistance: NavMeshAgent stopping distance for this state behaviour