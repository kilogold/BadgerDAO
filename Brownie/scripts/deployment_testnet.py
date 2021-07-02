from brownie import accounts, BrownieWrap_Token, TokenHolderThresholdValidator, Faucet, Wei, network

def main():

    wltBadgerTeam = accounts.load('GasWallet')

    network.disconnect()
    network.connect('ropsten')
    print('----------------------------------------')
    print('Deploying Ethereum (Ropsten) contracts. ')
    print('----------------------------------------')
    ctrBadgerLpToken = BrownieWrap_Token.deploy("Sett Vault Badger LP", "bBadger", {'from': wltBadgerTeam}, publish_source=True)
    ctrBadgerHolderValidation = TokenHolderThresholdValidator.deploy(ctrBadgerLpToken.address, 10, 1, {'from': wltBadgerTeam}, publish_source=True)

    network.disconnect()
    network.connect('bsc-test')
    print('----------------------------------------')
    print('Deploying Binance Smart Chain contracts.')
    print('----------------------------------------')
    ctrBadgerLpToken = BrownieWrap_Token.deploy("Sett Vault Badger LP", "bBadger", {'from': wltBadgerTeam}, publish_source=True)
    ctrBadgerHolderValidation = TokenHolderThresholdValidator.deploy(ctrBadgerLpToken.address, 10, 1, {'from': wltBadgerTeam}, publish_source=True)
    ctrFaucet = Faucet.deploy(10, Wei("50 gwei"), [], {'from' : wltBadgerTeam, 'value': Wei("500 gwei")}, publish_source=True)

    print('Contract chain deployment complete.')