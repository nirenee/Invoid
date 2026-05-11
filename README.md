# Invoid
Videogame made for the Develop at Ubisoft Program. 
# Project Overview
For more in-depth details about the game's mechanics, lore, and complete design, please check our official Game Design Document:

[ Click here to read the Game Design Document (GDD) & Story Details](https://docs.google.com/document/d/1xHgj9lL50fSc0uN7FdnCe3ZCiEAKjDwNGhs0a7QFH5U/edit?usp=sharing)
# Controls

The game supports both **Keyboard & Mouse** and **Xbox Controllers**.

| Action            | Keyboard                | Xbox Controller            |
|-------------------|-------------------------|----------------------------|
| Movement          | WASD                    | Left Joystick              |
| Dash              | Left Shift              | B / Button East            |
| Jump              | Spacebar                | A / Button South           |
| Attack            | Left Click / Mouse 1   | RT (Right Trigger)         |
| Change Camera View | 1  |  |
| Interact / Pick up| E                       | X / Button West            |
|Pause Menu     | ESC                         |       Menu Button


> **Pause & Options**
-> Access the Pause Menu at any time to catch your breath. From there, you can enter the Options menu to adjust settings such as your **Camera Speed**.
---

#  Enemies & Mechanics
### Combat Basics
Use the crosshair in the center of your screen to aim your camera and shoot at enemies.

### Pulpitos (Octos)
- **Behavior:** They have a patrol state, moving randomly between different points.
- **Detection:** They have a 360º vision range. If you get too close, they will chase you. If you run and get far enough away, they will lose sight of you and return to their patrol.
- **Combat:** Perform melee attacks once they get close to you.
- **Loot:** They have the lowest drop rate for Gems.

### Picudos (Stars)
- **Behavior:** They are in a wandering state, walking randomly around the map.
- **Combat:** If you step in front of them and enter their vision range, they will shoot projectiles (bullets) at you.
- **Loot:** They have a higher Gem drop rate compared to the Octos.

### Procedural Spawning (Spaceship Room)
The last room of the game features a **procedural enemy spawning system** based on a point budget:
- The level is assigned a total pool of spawn points (100 in the first level).
- Enemies will continuously spawn until this point budget is fully depleted.
- **Spawn Costs:**
  - **Pulpitos (Octos):** 20 points per spawn.
  - **Picudos (Stars):** 10 points per spawn.
 
## Camera System

The game allows the player to switch between **First-Person** and **Third-Person** camera views at any time during gameplay.

This feature gives the player more freedom when exploring, fighting enemies, or navigating the environment, allowing different gameplay perspectives depending on the situation.


---

# Shop System and Upgrades

After completing the first level and collecting more than **100 Gems**, the player can return to the spaceship and access the
**Shop Area**.

Inside the ship, the player must interact with the **blue command table** to purchase upgrades and improve their abilities before continuing.

Once at least one upgrade has been purchased, the door to the next level will open.

Possible upgrades include:
- **Upgrade character stats:** Increase your Max Health, Attack Range, Damage output, and more.
- **Unlock new content:** Acquire brand-new abilities and weapons for your arsenal.

---
# Procedural Levels

The second level is fully generated using a **procedural generation system**.

The map is created dynamically by assigning:
- The number of chunks/platforms
- The type of chunks that can spawn
- Their possible connections and positions
- Enemy spawn distribution

For the current prototype:
- The level contains **10 procedural chunks/platforms**
- The player must collect **200 Gems** to complete the level
- The spaceship spawns at the end of the level.

- Difficulty increases progressively as the player advances

Because the game is built around modular procedural systems, it can easily be expanded with:
- New chunk variations
- New enemy types
- Larger levels
- Different level themes
- More complex procedural rules

The project was designed with scalability in mind, allowing the game to grow into much larger and more varied experiences.

---
# Your Mission

Your ship has crashed, and its vital components are scattered across the planet. 

## Current Gameplay Loop

### Level 1
1. Defeat enemies and collect more than **100 Gems**
2. Retrieve the Ship Piece
3. Return to your crashed spaceship

### Shop Area
1. Upgrades habilities by opurchasing objects

### Level 2
1. Explore the procedurally generated map
2. Survive increasingly difficult enemy encounters
3. Collect more than **200 Gems**
4. Win the game

# How to compile the project 

## 1. Clone the Repository
To open the project, you first need to clone the GitHub repository into an empty folder. 
Open your terminal and run the following command:

```bash
git clone https://github.com/nirenee/Invoid.git
```
## 2. Open in Unity
Open Unity Hub (or Unity 3D).

Click on Add project and select the folder you just cloned.
> [!IMPORTANT]  
> This project requires **Unity version 2022.3.62f3**.  
> If you don't have it installed, download it via **Unity Hub** to avoid compatibility issues.

## 3. Build
Once the project is loaded and ready in the Unity Editor, you have two options to test the game:

**Play directly in the Editor**: Simply press the Play button (▶) at the top center of the Unity Editor to play the game immediately without building.

**Build and Run**: Go to File → Build and Run to compile the game into a standalone executable. The game will automatically launch once the build process is complete.

# Credits
### Design and Programming:
Irene Onsurbe Casado
### Mentoring:
Luka Rolak
### Music:
- Cleyton Kauffman - Exploration Theme
- Zander Noriega - Blinding Lights
- Trevor Lentz - Lunar Echo
- Caryil - The Desert of Dreams
### 3D Art
- Maksim Bugrimov - Spaceship
- Dungeon Mason - RPG Monster Couple PBR Polyart
- cloudyette - Stylized Astronaut – Game-Ready
- Modular Sci-Fi Corridor - MagixBox
- Free Demo Of Low Poly Space Alien Worlds 3D Asset Pack - Draftpunk Studios
