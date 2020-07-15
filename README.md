# MobGe Unity Car Driving Assignment

## Information
The game has two levels. Each level has eight cars. You must be driving each car to their target positions.

### To play the game
You just run the scene named “PlayScene”.

### To create a level
You use the object named LevelCreator in the hierarchy. 
- When you are creating a level, don’t need to run the game.
- After creating a level, you can edit the level. Just pay attention, exists data may be deleted if you don’t update the level after loaded.
- After finishing the creation, you must update on a variable called Max Level on the GameController component in the Game Manager object.

### To change speeds
If you want to change the speed of the car, you just change some variables in the LevelController object, such as carForwardSpeed and CarRotateSpeed.

Have fun!
