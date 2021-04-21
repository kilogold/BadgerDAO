// SPDX-License-Identifier: CC-BY-SA-4.0

// Version of Solidity compiler this program was written for
pragma solidity ^0.8.2;

import "WalletValidator.sol";
import "openzeppelin-contracts/contracts/token/ERC20/IERC20.sol"; 

contract BadgerValidation is WalletValidator
{
    IERC20 immutable TOKEN;
    uint256 immutable threshold;
    
    constructor(address tokenAddress, uint256 balanceThreshold)
    {
        TOKEN = IERC20(tokenAddress);
        threshold = balanceThreshold;
    }
    
    function Validate(address wallet) external view override returns (bool)
    {
        return(TOKEN.balanceOf(wallet) >= threshold);
    }
}