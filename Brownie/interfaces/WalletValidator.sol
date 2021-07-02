// SPDX-License-Identifier: CC-BY-SA-4.0
pragma solidity ^0.8.0;

interface WalletValidator
{
    function Validate(address wallet) external view returns(bool);
}