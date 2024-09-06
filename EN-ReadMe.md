To play the game, you need to adjust the media paths correctly. Specifically, for images and sounds, ensure that they are set to the "resources" path during the build process. Currently, the only objective in the game is to collect all the coins, with no additional goals or challenges at this time. Have fun :)

![image](https://github.com/user-attachments/assets/fae7317f-76a9-4ade-b202-d7dbe4f11953)
 

My project started as a console application where I created a map based on a sketch and made this map accessible in the form of text. For example, you would enter a letter corresponding to a cardinal direction, move in that direction, then choose another direction, and either continue moving, find something, or hit a wall and be unable to proceed. The game featured items that needed to be collected to escape the maze.


I transitioned to a WPF application with a self-defined game field based on the previous console application. It could be controlled by typing into the console, using NSWO buttons, or arrow keys.
  
![image](https://github.com/user-attachments/assets/5ef5cebc-4771-4a3c-88fe-1ff578d88d6e)
![image](https://github.com/user-attachments/assets/03c6c82c-bdae-4119-bad1-9699b1e20dda)
![image](https://github.com/user-attachments/assets/08e450c5-ce3e-4b3d-8197-a6d68346d184)



The project progressed with a new version of the WPF app to create a larger map with enemies that moved in random directions.

![image](https://github.com/user-attachments/assets/dcb84d4c-5c9a-4f2d-b65d-04922c15c572)
![image](https://github.com/user-attachments/assets/05db0326-4a1d-4082-b52a-afe11f829986)


 
The game state was such that 10 enemies were spawned over time, depending on the fields that were entered. The enemies would reduce your health, but depending on which enemy it was, they had more health and dealt more damage. When leveling up, items would spawn depending on the level. These items were destroyed if enemies walked over them. Some items had specific functions, such as varying strength healing potions, swords, and shields, which influenced the player's stats. 

![image](https://github.com/user-attachments/assets/2038aab4-3c2d-4795-acbf-ce853ce3c01f)
![image](https://github.com/user-attachments/assets/750984ac-43f5-4ed6-a30f-52d087f39f63)

 

The items were displayed in the inventory; however, I had significant difficulties with the inventory system, so I had to proceed with further functionality. Making the inventory usable was a future goal. Additionally, there was a scoreboard that was only reached after defeating all 10 enemies, and a high score could be entered if you managed to achieve one. The biggest problem at that time was that the game would randomly freeze, requiring the application to be restarted. My plan was to simplify the generation and overall functionality in the future and to make the generation more reliable so that all fields could be accessed.

![image](https://github.com/user-attachments/assets/32de3195-8394-48e0-9198-f4fa78dc2620)
![image](https://github.com/user-attachments/assets/b8d67ee9-7b84-4210-bff7-e852fc4ae91c)
![image](https://github.com/user-attachments/assets/00c86f2a-3186-47ba-a0c8-6428e52797ab)
![image](https://github.com/user-attachments/assets/50119aec-67b7-4d42-a494-739c8757e4b9)

 
 
  
Revised version with algorithm adjustments. I researched various types of algorithms for games online and came across a video that explained it very well. The algorithm starts in the top-left corner and makes a random step in one direction. The next step is in a random direction, and if a new field is adjacent to an already known field, it should not be occupied, and a new one must be tried. If from a position no further field is accessible, the second field, which was initially selected in the algorithm, is checked to see if surrounding fields are already known. If so, it checks the third field from the top-left in one direction, and so on.

![image](https://github.com/user-attachments/assets/27f04a2c-4ab5-46d9-9a13-9294df580ccb)
![image](https://github.com/user-attachments/assets/4497456a-0c3e-440a-bca0-250ecd050745)




This led me to an example of a generation process that reliably creates a maze where every cell can be reached from the starting point. Now we just need functions, a game character, and a goal. Once these elements are in place, many variations can be created, new levels can be built, and stories can be developed around them.

![image](https://github.com/user-attachments/assets/e9853484-4276-43fd-80cc-4090cce48388)

 


From this point onward, I added a character that could be moved and used an online tool to generate a background. I also included collectible coins, which increased the counter upon collection. Looking ahead, I plan to introduce several new features to further enhance the game. These include adding multiple levels to create a more varied and challenging gameplay experience, implementing enemies that can be defeated, and incorporating a shop where players can purchase items using the collected coins.
Additionally, I aim to include various findable objects throughout the game, enrich the experience with a small narrative, and potentially integrate a time factor to reward players based on their performance within each level. I also plan to add a scoreboard for tracking high scores and introduce puzzles and riddles to add complexity and variety. These planned features will significantly expand the game's depth and player engagement.

![image](https://github.com/user-attachments/assets/a7f4f9ab-0a27-4aee-94f7-714ac77095c0)

 


