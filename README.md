# Path Finding

#### Assignment 3 @ COMP-521

![image-20201122131929693](/home/gao/Desktop/pathfinding/screenshot.png)

The results below are obtained from simulating for 60 seconds.

###### 2 Agents

------

| Successful | Path Planned | Path Re-planned | Total Planning Time (ms) |
| ---------- | ------------ | --------------- | ------------------------ |
| 44         | 44           | 0               | 11.479                   |
| 36         | 48           | 11              | 22.1644                  |
| 42         | 58           | 14              | 20.0816                  |

###### 4 Agents

------

| Successful | Path Planned | Path Re-planned | Total Planning Time (ms) |
| ---------- | ------------ | --------------- | ------------------------ |
| 71         | 116          | 43              | 20.4988                  |
| 82         | 111          | 25              | 22.4063                  |
| 84         | 113          | 27              | 23.8855                  |

###### 8 Agents

------

| Successful | Path Planned | Path Re-planned | Total Planning Time (ms) |
| ---------- | ------------ | --------------- | ------------------------ |
| 149        | 294          | 138             | 53.7201                  |
| 149        | 313          | 158             | 55.3343                  |
| 154        | 252          | 90              | 37.7279                  |

###### 16 Agents

------

| Successful | Path Planned | Path Re-planned | Total Planning Time (ms) |
| ---------- | ------------ | --------------- | ------------------------ |
| 241        | 963          | 707             | 106.2626                 |
| 254        | 802          | 535             | 131.3696                 |
| 296        | 822          | 515             | 95.6203                  |

###### 32 Agents

------

| Successful | Path Planned | Path Re-planned | Total Time (ms) |
| ---------- | ------------ | --------------- | --------------- |
| 365        | 2622         | 2230            | 357.3556        |
| 366        | 2668         | 2274            | 313.1011        |
| 373        | 2684         | 2285            | 291.9881        |

Behavior is starting to become unacceptable after 32 Agents, although the frame rate is still high. Sometimes, more agents will have paths colliding with each other, and this issue is pronounced when you have a cluster of agents colliding in the same region. In such cases, since agents are both colliders, they kept pushing back and bumping into each other.

