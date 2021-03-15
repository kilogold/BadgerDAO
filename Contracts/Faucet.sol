// SPDX-License-Identifier: CC-BY-SA-4.0

// Version of Solidity compiler this program was written for
pragma solidity ^0.8.2;

import "https://github.com/OpenZeppelin/openzeppelin-contracts/blob/master/contracts/token/ERC20/ERC20.sol";

// My first contract is a faucet! :D
contract Faucet 
{
    ERC20 constant internal FAUCET_TOKEN = ERC20(0xFab46E002BbF0b4509813474841E0716E6730136);

    mapping (address => uint256) private participants;
    uint256 private participantRetryTime = 3 seconds;
    
    constructor(uint256 retryAmount) payable
    {
        participantRetryTime = retryAmount;
    }
    
    receive () external payable {}
    
    function grant(address recipient, uint256 amount) public
    {
        require(canParticipate());
        
        require(amount <= FAUCET_TOKEN.balanceOf(address(this)));
        
        //TODO:Check for gas
                
        require(FAUCET_TOKEN.transfer(recipient, amount));
        
        participants[msg.sender]  = block.timestamp;
    }
    
    function canParticipate() public view returns (bool)
    {
        return(participants[msg.sender] + participantRetryTime < block.timestamp);
    }
    
    function getElapsedTime() public view returns (uint256)
    {
        return(block.timestamp - participants[msg.sender]);
    }
}