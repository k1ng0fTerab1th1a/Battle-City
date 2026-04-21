# Battle City

A C# remake of the classic 1985 Namco arcade game. Comes in two flavours: a Windows Forms GUI and a terminal-based console version.

---

## Gameplay

### Console version

| Destroying a wall | Enemy behaviour | Player shooting enemy |
|:-:|:-:|:-:|
| *(gif here)* | *(gif here)* | *(gif here)* |

### GUI version



---

## Features

- Faithful recreation of the original Battle City map layout (level loaded from a text file)
- Enemy AI — tanks navigate around obstacles and shoot randomly
- Destructible brick walls, indestructible iron walls, and water tiles
- Protect your base: game over if it is destroyed
- 20 enemies per level; survive until all are eliminated to win
- Two independent front-ends sharing a single `CodeBase` library:
  - **Console** — runs in any terminal, arrow keys + Space to shoot
  - **GUI** — Windows Forms with pixel-art sprites and a start/game-over/you-win screen

---

## Project structure

```
Battle-City/
├── CodeBase/               # Shared game logic (field, entities, AI, level loader)
│   ├── Game_Elements/      # Cells (BrickWall, IronWall, Water…) and Entities (Player, Enemy, Bullet, Base)
│   ├── Fields/             # Field and EntitiesField grids
│   ├── Internal_Code/      # Globals, Build (level loader)
│   └── levels/             # Level text files (lvl1.txt, …)
├── BattleCityConsole/      # Console front-end
│   └── Engines/            # GameEngine, GraphicsEngine (console renderer), InputEngine
└── BattleCityGUI/          # Windows Forms front-end
    ├── Forms/              # StartMenu, GameForm, GameOver, YouWin, FormBase
    ├── Resources/          # Sprite images (PNG) and icon
    └── Internal_Code/      # GUIGlobals (cell pixel size)
```

---

## Requirements

- .NET 6 SDK
- Windows (the GUI project uses Windows Forms; the console project runs on any OS)

---

## How to run

### Option 1 — Visual Studio

1. Open `BattleCityConsole/Battle City.sln` in Visual Studio.
2. In the Solution Explorer, right-click the project you want to run (`BattleCityConsole` or `BattleCityGUI`) and choose **Set as Startup Project**.
3. Press **F5** (or **Ctrl+F5** to run without debugger).

### Option 2 — .NET CLI

```bash
# Clone
git clone https://github.com/k1ng0fTerab1th1a/Battle-City.git
cd Battle-City

# Console version
dotnet run --project BattleCityConsole/BattleCityConsole.csproj

# If the rendering looks broken, see "Console setup" below

# GUI version (Windows only)
dotnet run --project BattleCityGUI/BattleCityGUI.csproj
```

---

## Console setup

The console version renders a 52×52 character grid and is sensitive to your terminal configuration. If the program crashes or the output looks garbled, misaligned, or cut off, try the following:

**Font** — use a square (or near-square) monospace font so each cell renders as a proper tile. In Windows Terminal or the classic console host, right-click the title bar → *Properties* → *Font* and pick **Consolas**, **Courier New**, or a bitmap font at a small point size (e.g. 8–10 pt).

**Buffer / window size** — the buffer must be at least 52 columns wide and 52 rows tall. In the classic console host go to *Properties* → *Layout* and set both *Screen Buffer Size* and *Window Size* width/height accordingly. In Windows Terminal you can resize the window freely.

**Windows Terminal** is generally the easiest to work with — just make the window large enough before launching.

---

## Controls

| Action | Key |
|---|---|
| Move | Arrow keys |
| Shoot | Space |
| Confirm (menus) | Enter |
