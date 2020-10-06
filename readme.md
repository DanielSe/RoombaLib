# RoombaLib

Library to control iRobot Roomba Vacuums.

# Examples

Start roomba:

```c#
var factory = new RoombaFactory();
using var robot = factory.Connect("blid", "password", "192.168.0.20");
await robot.StartAsync();
```
