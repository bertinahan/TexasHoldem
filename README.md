# IQ Take-home Testing - Texas Holdem

## Assumptions

- Only one deck(52) of card is used, no duplicated cards allowed
- Implement partial rules, which contains flush, there of a kind, one pair and
high card
- User can to enter name(non-empty) string to add player, or press enter key
directly to quit game
- When player is added, user needs to give valid card string for that player;
otherwise program will keep asking for valid card string
- Card used by another player can not be added again in one game round
- When poker hand ranks are tie for players, program will compare kickers.
- Card string must be seperated by comma, and C, D, H, S are used for suit

## Design

### TexaHoldem

The main project. It handles user interaction and use Api project to handle game
logic.

### Library

The Api project. It exposes interfaces:

- RegisterPlayer
- RegisterPlayerHand
- RegisterRules
- GetWinners

### Test

NUnit test project, to test functionality in library project.

## Tie Breaker Rules

### Flush

- If two or more players hold a flush, the flush with the highest card wins.
If more than one player has the same strength high card, then the strength of
the second highest card held wins. This continues through the five highest cards
 in the player's hands.

 ### Three Of A Kind

 - If more than one player holds three of a kind, then the higher value of the
 cards used to make the three of kind determines the winner. If two or more
 players have the same three of a kind, then a fourth card (and a fifth if
 necessary) can be used as kickers to determine the winner.

 ### One Pair

 -If two or more players hold a single pair, then highest pair wins. If the
 pairs are of the same value, the highest kicker card determines the winner.
 A second and even third kicker can be used if necessary.

 ### High Card

 When no player has even a pair, then the highest card wins. When both players
 have identical high cards, the next highest card wins, and so on until five
 cards have been used. In the unusual circumstance that two players hold the
 identical five cards, the pot would be split.
