To play the game, you need to adjust the media paths correctly. Specifically, for images and sounds, ensure that they are set to the "resources" path during the build process. Currently, the only objective in the game is to collect all the coins, with no additional goals or challenges at this time. Have fun :)

![WPFProjekt1](https://github.com/user-attachments/assets/bbc55520-a85b-4598-a33f-141e8ebac61e)


My project started as a console application where I created a map based on a sketch and made this map accessible in the form of text. For example, you would enter a letter corresponding to a cardinal direction, move in that direction, then choose another direction, and either continue moving, find something, or hit a wall and be unable to proceed. The game featured items that needed to be collected to escape the maze.


I transitioned to a WPF application with a self-defined game field based on the previous console application. It could be controlled by typing into the console, using NSWO buttons, or arrow keys.

![image](https://github.com/user-attachments/assets/168741d9-128a-42bc-8740-936766935fe8)
![image](https://github.com/user-attachments/assets/d41b7dd6-df5f-465d-be87-62471302ebf5)
![image](https://github.com/user-attachments/assets/52190b5e-3b33-4579-9ead-085177bd848e)




The project progressed with a new version of the WPF app to create a larger map with enemies that moved in random directions.

![image](https://github.com/user-attachments/assets/b68cdc1d-a868-4d21-80c2-cbb50ba1229b)
![image](https://github.com/user-attachments/assets/9aca8502-ec30-47fb-a073-051376c8bb3a)




The game state was such that 10 enemies were spawned over time, depending on the fields that were entered. The enemies would reduce your health, but depending on which enemy it was, they had more health and dealt more damage. When leveling up, items would spawn depending on the level. These items were destroyed if enemies walked over them. Some items had specific functions, such as varying strength healing potions, swords, and shields, which influenced the player's stats. 

![image](https://github.com/user-attachments/assets/44ce808a-f986-4f7f-952a-22c2bdf2d178)
![image](https://github.com/user-attachments/assets/1adfcbf8-0d7a-40b9-9ffd-10a56d740dd9)




The items were displayed in the inventory; however, I had significant difficulties with the inventory system, so I had to proceed with further functionality. Making the inventory usable was a future goal. Additionally, there was a scoreboard that was only reached after defeating all 10 enemies, and a high score could be entered if you managed to achieve one. The biggest problem at that time was that the game would randomly freeze, requiring the application to be restarted. My plan was to simplify the generation and overall functionality in the future and to make the generation more reliable so that all fields could be accessed.

![image](https://github.com/user-attachments/assets/54868033-5f53-467e-b18e-7c31736bd59f)
![image](https://github.com/user-attachments/assets/0d306eb0-c6e2-437f-91e3-ec4dd4e46c95)
![image](https://github.com/user-attachments/assets/79dcc6d7-a6ce-4e2e-a1a3-5ca841a0df3f)
![image](https://github.com/user-attachments/assets/4e55f7b5-26ff-47a0-9f7c-a58cbeab76db)




Revised version with algorithm adjustments. I researched various types of algorithms for games online and came across a video that explained it very well. The algorithm starts in the top-left corner and makes a random step in one direction. The next step is in a random direction, and if a new field is adjacent to an already known field, it should not be occupied, and a new one must be tried. If from a position no further field is accessible, the second field, which was initially selected in the algorithm, is checked to see if surrounding fields are already known. If so, it checks the third field from the top-left in one direction, and so on.

![image](https://github.com/user-attachments/assets/5760ea22-c19b-474d-9585-7b17ff1b2835)
![image](https://github.com/user-attachments/assets/c17f7993-2149-45ac-8274-8d9afbcbd05e)




This led me to an example of a generation process that reliably creates a maze where every cell can be reached from the starting point. Now we just need functions, a game character, and a goal. Once these elements are in place, many variations can be created, new levels can be built, and stories can be developed around them.

![image](https://github.com/user-attachments/assets/490e7c14-7b0c-44ad-a072-71e9354ec3ba)




From this point onward, I added a character that could be moved and used an online tool to generate a background. I also included collectible coins, which increased the counter upon collection. Looking ahead, I plan to introduce several new features to further enhance the game. These include adding multiple levels to create a more varied and challenging gameplay experience, implementing enemies that can be defeated, and incorporating a shop where players can purchase items using the collected coins.
Additionally, I aim to include various findable objects throughout the game, enrich the experience with a small narrative, and potentially integrate a time factor to reward players based on their performance within each level. I also plan to add a scoreboard for tracking high scores and introduce puzzles and riddles to add complexity and variety. These planned features will significantly expand the game's depth and player engagement.

![image](https://github.com/user-attachments/assets/a59f9024-c714-4fa5-a129-7cc48b105a3d)








