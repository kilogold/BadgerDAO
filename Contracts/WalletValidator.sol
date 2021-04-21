interface WalletValidator
{
    function Validate(address wallet) external view returns(bool);
}