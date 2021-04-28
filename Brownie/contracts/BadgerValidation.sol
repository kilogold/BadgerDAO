// SPDX-License-Identifier: CC-BY-SA-4.0

// Version of Solidity compiler this program was written for
pragma solidity ^0.8.0;

import "../interfaces/WalletValidator.sol";
import "@openzeppelin/contracts/token/ERC20/IERC20.sol"; 

contract TokenHolderThresholdValidator is WalletValidator
{
    IERC20 immutable TOKEN;
    uint256 immutable threshold;
    Operator immutable operator;

    enum Operator
    {
        None,
        GreaterThan,
        GreaterEqualThan,
        LessThan,
        LessEqualThan,
        Equal,
        MAX_OPERATION
    }
    
    constructor(address tokenAddress, uint256 balanceThreshold, Operator operatorIn)
    {
        require(operatorIn > Operator.None && operatorIn < Operator.MAX_OPERATION);

        TOKEN = IERC20(tokenAddress);
        threshold = balanceThreshold;
        operator = operatorIn;
    }
    
    function Validate(address wallet) external view override returns (bool)
    {
        assert(operator != Operator.None && operator != Operator.MAX_OPERATION);

        if(operator == Operator.GreaterEqualThan)
            return(TOKEN.balanceOf(wallet) >= threshold);

        if(operator == Operator.GreaterThan)
            return(TOKEN.balanceOf(wallet) > threshold);

        if(operator == Operator.LessEqualThan)
            return(TOKEN.balanceOf(wallet) <= threshold);

        if(operator == Operator.LessThan)
            return(TOKEN.balanceOf(wallet) < threshold);

        if(operator == Operator.Equal)
            return(TOKEN.balanceOf(wallet) == threshold);

        assert(false); // should not reach here.
        return false; // satisfy compiler warning
    }
}