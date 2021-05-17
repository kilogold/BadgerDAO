from brownie import accounts, Faucet, Wei

def main():

    # Be sure to unlock a valid bsc wallet on brownie.
    wltBadgerTeam = accounts['FaucetWallet']

    print('----------------------------------------')
    print('Deploying Binance Smart Chain contract.')
    print('----------------------------------------')
    ctrFaucet = Faucet.deploy(86400, Wei("0.005 ether"), [], {'from' : wltBadgerTeam, 'value': Wei("1 ether")}, publish_source=True)

    print('Contract chain deployment complete.')