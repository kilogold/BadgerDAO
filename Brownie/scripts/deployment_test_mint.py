from brownie import accounts, BrownieWrap_Token, TokenHolderThresholdValidator, Faucet, Wei, network

def main():

    acct = accounts.load('TestUser')

    network.disconnect()
    network.connect('ropsten')
    print('----------------------------------------')
    print('Ethereum (Ropsten) Minting. ')
    print('----------------------------------------')
    BrownieWrap_Token.at('0xBD2231994722D8a47244C4166Bc6Ac4bF8Bbc110').mint(acct, 20, {'from' : acct})

    network.disconnect()
    network.connect('bsc-test')
    print('----------------------------------------')
    print('Binance Smart Chain Minting')
    print('----------------------------------------')
    BrownieWrap_Token.at('0x926A513fdd63e1010e6C0627EB12204ADA45d550').mint(acct, 20, {'from' : acct})
