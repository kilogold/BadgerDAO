from brownie import accounts, BrownieWrap_Token, TokenHolderThresholdValidator, Faucet, Wei

def main():
    wltBadgerTeam = accounts[0]   

    ctrBadgerLpToken = BrownieWrap_Token.deploy("Sett Vault Badger LP", "bBadger", Wei("180000 ether"), wltBadgerTeam, {'from': wltBadgerTeam})
    
    ctrBadgerHolderValidation = TokenHolderThresholdValidator.deploy(ctrBadgerLpToken.address, 10, 1, {'from': wltBadgerTeam})
    
    ctrFaucet = Faucet.deploy(10, Wei("250 gwei"), [ctrBadgerHolderValidation.address], {'from' : wltBadgerTeam, 'value': Wei("25 ether")})

    print('Contract chain deployment complete.')