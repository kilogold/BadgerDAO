from brownie import accounts, BrownieWrap_Token, TokenHolderThresholdValidator, Faucet, Wei, network

def main():

    acct = accounts.load('TestUser')

    network.disconnect()
    network.connect('ropsten')
    print('----------------------------------------')
    print('Ethereum (Ropsten) Minting. ')
    print('----------------------------------------')
    BrownieWrap_Token.at('0x9143E3Aa6ccC7279713a204970eeCbdD1917c4B5').mint(acct, 20, {'from' : acct})

    network.disconnect()
    network.connect('bsc-test')
    print('----------------------------------------')
    print('Binance Smart Chain Minting')
    print('----------------------------------------')
    BrownieWrap_Token.at('0xAC762d89beCfaD230856491438321e1296E43960').mint(acct, 20, {'from' : acct})
