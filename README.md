Connect 4
You must develop an application that allows players to play Connect 4 online.
Functional Specifications
Your client provides a list of functional rules that must be implemented in your application.
Players
The application has a closed list of players (Players).
Each player has a username (Login) and a password (Password).
There is no need to provide a system for new player registration; the list can be predefined.
Games
A game (Game) is a match between two players.
A player can create a game and become the host of the game (Host).
A player can join a game created by another player and become the guest (Guest).
The game only starts when a guest has joined.
The application must allow the player to list:
• Games waiting for a guest
• Games waiting for the player's action
• Games in which the player has participated
A game can have a status (or state) with three possible values:
• Waiting for a guest (Awaiting Guest)
• In progress (In Progress)
• Finished (Finished)
Game Rules
The game follows the rules of Connect 4.
The game takes place on a rectangular grid (Grid) made up of cells (Cells).
The grid has 7 columns (Columns) in width.
The grid has 6 rows (Rows) in height.
Players take turns (Turn), choosing which column to drop their token (Token) into.
The guest plays first.
When a player manages to align four tokens, they win the game.
The game ends either when a player wins or when the grid is completely filled.
Once the game is finished, no further moves can be made.
If the grid is filled and no player has won, the game is a draw.
Access Control
Only the host and guest of a game can play in that game.
A player cannot play on behalf of another player.
Recap Diagrams
mermaid
Copier le code
classDiagram
class Player {
+String login
+String password
+List<Game> games
}

    class Game {
        +Player host
        +Player guest
        +Grid grid
        +String status
        +startGame()
        +joinGame(Player guest)
        +playTurn(Player player, int column)
        +checkWinCondition(): boolean
    }

    class Grid {
        +int rows
        +int columns
        +Cell[][] cells
        +dropToken(int column, Token token)
        +isFull(): boolean
    }

    class Cell {
        +int row
        +int column
        +Token token
    }

    class Token {
        +String color
    }

    Player "1" -- "0..*" Game : plays
    Game "1" -- "1" Grid : has
    Grid "1" -- "0..*" Cell : contains
    Cell "1" -- "1" Token : holds

mermaid
Copier le code
sequenceDiagram
participant Player
participant Frontend
participant Backend
participant Database

    Player->>Frontend: Log in
    Frontend->>Backend: Authentication
    Backend->>Database: Verify credentials
    Database-->>Backend: Authentication result
    Backend-->>Frontend: Authentication result
    Frontend-->>Player: Display the list of games

    Player->>Frontend: Create a game
    Frontend->>Backend: Request to create a game
    Backend->>Database: Save the new game
    Database-->>Backend: Confirmation of saving
    Backend-->>Frontend: Game creation confirmation
    Frontend-->>Player: Display the created game

    Player->>Frontend: Play a turn
    Frontend->>Backend: Send the move
    Backend->>Database: Update grid state
    Database-->>Backend: Confirmation of update
    Backend-->>Frontend: Update grid display
    Frontend-->>Player: Show the updated grid state

Technical Specifications
Your application must comply with the client's technical requirements.
Architecture
The client wants the application to be modular to allow future components, such as a mobile interface, to be added in addition to the Web interface.
Therefore, the application should follow a 3-tier architecture:
• An application tier containing the functional rules (business logic)
• A presentation tier, for now, with a Web interface
• A data access tier
There is no need to add other tiers or components.
Technologies
You must follow certain technological standards to ensure the delivered application meets the client's expectations and can be maintained by their teams.

1. Presentation Tier (Frontend)
   Technology: ASP.NET Core Blazor to create an interactive Web interface.
   Responsibilities:
   • Display login and game list pages.
   • Allow players to create, join, and play games.
   • Display the grid's state and possible actions.
2. Application Tier (Backend)
   Technology: ASP.NET Core for REST APIs.
   The APIs should be secure, follow REST conventions, and use JSON for serialization.
   Responsibilities:
   • Manage business logic (game creation, turn handling, win condition checking).
   • Authenticate players and manage sessions.
   • Communicate with the data access tier to store and retrieve information.
3. Data Access Tier (Database)
   Technology: SQLite for a lightweight, embedded database.
   Responsibilities:
   • Store information about players, games, and grid states.
   • Provide methods for accessing and manipulating this data.
   Deliverables and Testing
   The deliverables must include:
   • The code for the various components
   • A database initialized with the complete schema
   • A README file explaining how to run your application
   • A set of tests, including user credentials for testing the application
   • A recap diagram of business entities
