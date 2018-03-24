# AI Overview

-----------------
## Animation Parameters
- (bool)    Target_IsAudible: Is an enemy within the hearing radius?
- (bool)    Target_InSight: Is an enemy visible within the "Total" FOV area?
- (bool)    Target_InFocus: Is an enemy visible within the "Focused" FOV area?
- (float)   AttackSpeed: Speed multiplier parameter for attack animation.

-----------------
## AI Behaviour Types

### Aggressive
- Patrols multiple waypoints
- Pursues player while sensed
- Attacks player while focused

### Cautious
- Patrols multiple waypoints
- Looks at player while sensed
- Attacks player while focused

### Stationary
- Does not move position
- Looks at player while sensed
- Attacks player while focused

-----------------
## Custom StateMachineBehaviours

### AI_Follow_Hearing
#### Description
Navigates animator object towards ISensor's LastPOI.
#### Interface Dependencies
- ISensor
- INavigator
#### Behaviour Locals
- (float) stoppingDistance: Stopping distance of the NavMeshAgent for this behaviour.

### AI_Follow_Target
#### Description
Navigates animator object towards ISensor's CurrentTarget.
#### Interface Dependencies
- ISensor
- INavigator
#### Behaviour Locals
- (float) stoppingDistance: Stopping distance of the NavMeshAgent for this behaviour.

### AI_LookAt_Target
#### Description
Rotates animator object towards ISensor's CurrentTarget.
#### Interface Dependencies
- ISensor
- INavigator

### AI_Patrol
#### Description
Handles waypoint navigation for the animator object's INavigator component.
#### Interface Dependencies
- INavigator
#### Behaviour Locals
- (int) targetWaypoint: Tracks the index of the most recently navigated waypoint.

### Attack
#### Description
Handles attack events.