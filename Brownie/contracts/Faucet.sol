// SPDX-License-Identifier: CC-BY-SA-4.0

// Version of Solidity compiler this program was written for
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/access/Ownable.sol";
import "../interfaces/WalletValidator.sol";

contract Faucet is Ownable
{
    address[] public validators;
    uint256 immutable public participantRetryTime;
    uint256 immutable public maxDistributionPerGrant;

    mapping (address => uint256) private participants;
    
    event Funding(string scenario, uint256 amount);
    
    receive() external payable
    {
        // Allow contract to receive award funds.
    }
    
    constructor(uint256 participantRetryTimeIn, uint256 maxDistributionPerGrantIn, address[] memory inputValidators) payable
    {
        participantRetryTime = participantRetryTimeIn;
        maxDistributionPerGrant = maxDistributionPerGrantIn;
        configureValidators(inputValidators);
    }
    
    function configureValidators(address[] memory inputValidators) public onlyOwner
    {
        validators = inputValidators;
    }
    
    function calculatePayout(uint8 score, uint8 fromTotal) public view returns (uint256)
    {
        return (score * maxDistributionPerGrant) / fromTotal;
    }

    function grant(address payable recipient, uint8 score, uint8 fromTotal) public onlyOwner
    {
        validate(recipient);
        
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
    
    function validate(address recipient) view public
    {
        for(uint8 i = 0; i < validators.length; ++i)
        {
            WalletValidator val = WalletValidator(validators[i]);
            require(val.Validate(recipient), "Recipient does not qualify");
        }
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