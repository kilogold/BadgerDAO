from brownie import accounts, BrownieWrap_Token, TokenHolderThresholdValidator, Faucet, Wei, network

def main():

    wltBadgerTeam = accounts.load('GasWallet')

    network.disconnect()
    network.connect('ropsten')
    print('----------------------------------------')
    print('Deploying Ethereum (Ropsten) contracts. ')
    print('----------------------------------------')
    ctrBadgerLpToken = BrownieWrap_Token.deploy("Sett Vault Badger LP", "bBadger", Wei("180000 ether"), wltBadgerTeam, {'from': wltBadgerTeam})
    ctrBadgerHolderValidation = TokenHolderThresholdValidator.deploy(ctrBadgerLpToken.address, 10, 1, {'from': wltBadgerTeam})

    network.disconnect()
    network.connect('bsc-test')
    print('----------------------------------------')
    print('Deploying Binance Smart Chain contracts.')
    print('----------------------------------------')
    ctrBadgerLpToken = BrownieWrap_Token.deploy("Sett Vault Badger LP", "bBadger", Wei("180000 ether"), wltBadgerTeam, {'from': wltBadgerTeam})
    ctrBadgerHolderValidation = TokenHolderThresholdValidator.deploy(ctrBadgerLpToken.address, 10, 1, {'from': wltBadgerTeam})
    ctrFaucet = Faucet.deploy(10, Wei("50 gwei"), [], {'from' : wltBadgerTeam, 'value': Wei("500 gwei")})
    
    print('Contract chain deployment complete.')