/*************************************************************************************************************
* By using brownie's package manager, we exclude package symbols into compilation,
* unless explicitly used by Solidity contracts codebase.
* 
* I suppose this is to optimize how many object get compiled into brownie's Python execution environment.
* As a side effect, we are unable to access package symbols, unless we use a specific fixture:
* https://eth-brownie.readthedocs.io/en/stable/package-manager.html#using-packages-in-tests
* 
* Although this resolves the limitation for tests, brownie scripts are still doomed.
* If brownie tests depend on brownie scripts, then we're back to square-one.
* To work around this latter limitation, we define our own symbols that wrap-around an existing packaged symbol
* in order to include it in the compilation process (as stated initially above).
*************************************************************************************************************/

// SPDX-License-Identifier: CC-BY-SA-4.0

// Version of Solidity compiler this program was written for
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/token/ERC20/presets/ERC20PresetMinterPauser.sol";

contract BrownieWrap_Token is ERC20PresetMinterPauser
{
        constructor(
        string memory name,
        string memory symbol
    ) ERC20PresetMinterPauser(name, symbol) {
    }

    function mint(address to, uint256 amount) public override {
        _mint(to, amount);
    }
}