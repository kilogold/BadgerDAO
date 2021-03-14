// SPDX-License-Identifier: CC-BY-SA-4.0

// Version of Solidity compiler this program was written for
pragma solidity ^0.6.0;

// Our first contract is a faucet!
contract Faucet 
{
    mapping (address => uint256) private participants;
    uint256 private participantRetryTime = 3 seconds;
    
    constructor(uint256 retryAmount) public
    {
        participantRetryTime = retryAmount;
    }
    
    receive () external payable {}
    
    function donate() public payable{}
    
    // Give out ether to anyone who asks
    function withdraw(uint withdraw_amount) public 
    {
        require(canParticipate());
        
        // Limit withdrawal amount
        require(withdraw_amount <= address(this).balance);

        participants[msg.sender] = now;
        
        // Send the amount to the address that requested it
        msg.sender.transfer(withdraw_amount);
    }
    
    function getBalance() public view returns (uint256)
    {
        return address(this).balance;
    }
    
    function canParticipate() public view returns (bool)
    {
        return(participants[msg.sender] + participantRetryTime < now);
    }
    
    function getElapsedTime() public view returns (uint256)
    {
        return(now - participants[msg.sender]);
    }
}