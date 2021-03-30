# Badger Lucky Faucet
This is an entry for [Gitcoin Round 9: Badger Honey Pot](https://gitcoin.co/issue/Badger-Finance/badger-system/70/100025037)
![Shot1](https://user-images.githubusercontent.com/1028926/112773689-bfa0b900-8feb-11eb-854d-c1fa4c0f2520.png)


### (Ropsten) Testnet Demo
![Demo](https://user-images.githubusercontent.com/1028926/112793193-b0d0fb00-9019-11eb-9a89-34286952cbaf.gif)

**Playable**: http://kraniumtivity.com/Extra/BadgerDAO/

**Full Gameplay**: https://youtu.be/VAFihQhIPsw

**Faucet Contract**: [0xa880C88249FA893271caAa46181f371Bb5875F1C](https://ropsten.etherscan.io/address/0xa880C88249FA893271caAa46181f371Bb5875F1C) ([source](Contracts/Faucet.sol))

## Summary
The Badger Lucky Faucet (BLF) is a mini-game template that can be used to add gamification to the classic boring faucet. With BLF, we can make claiming tokens competitive, cooperative, or anything in between (but mostly fun!). The BLF template provides an initial sample game where players must try catching coins falling from the clouds before time runs out, all while running across the edge of a cliff without falling off. Successful badgers will claim token amounts based on their performance. Unfortunate badgers will forfeit any collected coins. The coins represent a configurable amount of ERC20 tokens granted to the players wallet upon payout.

| Win | Lose |
|--|--|
| ![Win](https://user-images.githubusercontent.com/1028926/112774940-1e683180-8ff0-11eb-9f0d-8acbbe4a86bd.png) | ![Lose](https://user-images.githubusercontent.com/1028926/112774974-3fc91d80-8ff0-11eb-819d-ff8874ad2de7.png) |

## Features
### Support for any ERC20
The game can be deployed with different tokens and even multiple instances, providing a slew of creative uses, such as faucet draining competitions or "faucet mining".

### Configurable tokenomics
When deploying the smart contract, you can specify:
- ERC20 token contract.
- Maximum payout amount per play session.
- Delay between play sessions per user.

### Modular game state integration
The Unity project is organized so that you can replace the existing game activity with another of your own; the game state is simply a GameObject. You only need to ensure that event hooks are maintained. Look out for [UnityEvent](https://docs.unity3d.com/ScriptReference/Events.UnityEvent.html) fields on GameObject children.
![image](https://user-images.githubusercontent.com/1028926/112786574-69dc0900-900b-11eb-9122-b9a0f891d31f.png)

## Technical Overview

### Deployment Layout
![Alt text here](Documentation/Diagrams.svg)


### Token Grant Calculation
You can specify the maximum payout amount when creating the smart contract instance on the blockchain. In the current BLF template, the game design has a variable score and total where `score <= total`. The difference between score and total form the ratio that is factored into the max payout:
```
FinalPayout = (Score/Total) * MaxPayout
```

> Due to how fixed point math works in EVM, [the actual implementation](https://github.com/kilogold/BadgerDAO/blob/c711033d526fa48a5fe2d55c356d150b98932592/Contracts/Faucet.sol#L117) in Solidity is refactored.

### Game To  Smart Contract Config
Once the smart contract has been manually deployed, the game must be built with the updated smart contract info:

 1. Mainnet/Testnet infura API address.
 2. Faucet smart contract address.
 3. Designated gas wallet address to cover expenses.
 4. Designated gas wallet private key to sign faucet grant transactions.
 > Needless to say, don't save the production private key into the project. You should only enter it manually prior to building the WebGL project.

![](https://user-images.githubusercontent.com/1028926/112789719-934c6300-9012-11eb-95f3-21e9aa4825f9.png)


## Limitations / Pending Improvements

### Game only runs on Chrome
I'm not exactly a web dev so I had to rely on miscelaneous tutorials to get the WebGL properly bootstrapped with Web3 plugins such as Metamask. Brave Browser doesn't work, for example. The Metamask button on the start screen has only worked when running on Chrome with Metamask being the sole Web3 provider installed. More details on this issue to come. 

### Instant Replay Vulnerability
After playing a session (win or lose), the player can bypass the retry delay period simply by reloading the page immediately. A user could do this consecutively to illegitimately boost their rewards, potentially draining the faucet. This happens because time tracking for each player is done via block timestamps in Solidity. The delay period for any player is only updated on the next block. The solution here is to either:
- Query the mempool to validate whether a player has pending payouts (including zero amounts).
- Centralize the deployment by saving some of the game session data on a persistent backend, possibly even the website's own database.

### Overdraft grant transaction failure
There may be multiple players interacting with the faucet simultaneously. Because we only check for balance before starting a game, the faucet may become depleted by another player's payout before your game ends. When requesting your payout, the transaction will fail because there is not enough balance remaining. Similar to above, the solution here is to either:
- Query the mempool for pending payouts and pre-calculate the maximum collective payout to determine if there is enough for a 100% payout game session.
- Centralize the deployment by saving some of the game session data on a persistent backend, possibly even the website's own database.

### Expensive gas consumption
The reward payout is granted on-demand. At the time of writing, we would need to deploy this project on somewhere like Binance Smart Chain in order to take advantage of the low gas fees. A better solution here might be to explore Optimistic Rollups.

## Practical Applications
### DAO Grant
Depending on the DAO activities, sometimes there may be funds left over from some event, kind of like a no-show at a raffle. Instead of trying to figure out how to redistribute to the community, we can play for it.

### NFT Trophies
The game an easily incorporate multiplayer to have multiple badgers duking it out for the largest coin pile. We can modify the payout logic to deliver a trophy NFT. This can be a recurring annual event.  

### Airdrop alternative
Instead of a passive deployment of funds to multiple wallets, we can foster engagement by bringing would-be airdrop participants to the BadgerDAO app to play for their drops, which drives more traffic and exposure to all other Badger offerings on the site such as Digg, Claws, and Setts. 

# Accepting pull requests!
![logo badgers never hide](https://user-images.githubusercontent.com/1028926/112795519-7cf7d480-901d-11eb-9ed4-0c7fe2605bb7.jpg)

<!--stackedit_data:
eyJoaXN0b3J5IjpbNDM5MDg4MDU1LDYyMTk5ODg2NywtMTM0Mz
k5MTg5OCwtMTE5MTAwNDU1NCwxODY2NDE1MjU2LDI1ODAzNjQ5
MCwtMTg0OTg2NTgyOSwtMjAxMTM5NjQxNCwtNTg4MzI3NDQ0LD
IwOTE0MDY1MjYsLTE1NzY4MTQ5NjQsLTEzODMyMTk3MTAsMTEy
MDM5NzI2NiwtMTA4NjM1ODYwMl19
-->
