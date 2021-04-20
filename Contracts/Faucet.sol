// SPDX-License-Identifier: CC-BY-SA-4.0

// Version of Solidity compiler this program was written for
pragma solidity ^0.8.2;

import "openzeppelin-contracts/contracts/access/Ownable.sol"; 

contract Faucet is Ownable
{
    uint256 immutable public participantRetryTime;
    uint256 immutable public maxDistributionPerGrant;

    mapping (address => uint256) private participants;
    
    event Funding(string scenario, uint256 amount);
    
    receive() external payable
    {
        // Allow contract to receive award funds.
    }
    
    constructor(uint256 participantRetryTimeIn, uint256 maxDistributionPerGrantIn) payable
    {
        participantRetryTime = participantRetryTimeIn;
        maxDistributionPerGrant = maxDistributionPerGrantIn;
    }
    
    function calculatePayout(uint8 score, uint8 fromTotal) public view returns (uint256)
    {
        return (score * maxDistributionPerGrant) / fromTotal;
    }

    function grant(address payable recipient, uint8 score, uint8 fromTotal) public onlyOwner
    {
        require(getElapsedTime(recipient) > participantRetryTime, "Retry cooldown not met.");

        uint256 transferAmount = calculatePayout(score, fromTotal);

        uint256 currentBalance = address(this).balance;
        
        require(currentBalance > 0, "Contract has zero token balance");
        
        require(transferAmount <= currentBalance, "Not enough balance available in the contract.");
        
        if(score > 0)
        {
            recipient.transfer(transferAmount);
        }

        participants[recipient]  = block.timestamp;
    }
    
    function getElapsedTime(address participant) public view returns (uint256)
    {
        return(block.timestamp - participants[participant]);
    }
    
    function recoverFunds() public onlyOwner
    {
        emit Funding("Contract drained and sent to contract owner.", address(this).balance);
        payable(address(msg.sender)).transfer(address(this).balance);
    }
}