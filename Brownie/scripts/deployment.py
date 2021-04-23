from brownie import accounts, ERC20PresetFixedSupply, BadgerValidation, Faucet, Wei
# p = project.load("/Users/kelvin.bonilla/MyBrownie", name="BrownieProj")
# p.load_config()
# from brownie.project.BrownieProj import *
# network.connect('development')


def main():
    wltBadgerTeam = accounts[0]   

    ctrBadgerLpToken = ERC20PresetFixedSupply.deploy("Sett Vault Badger LP", "bBadger", Wei("180000 ether"), wltBadgerTeam, {'from': wltBadgerTeam})
    
    ctrBadgerHolderValidation = BadgerValidation.deploy(ctrBadgerLpToken.address, 10, {'from': wltBadgerTeam})
    
    ctrFaucet = Faucet.deploy(10, Wei("250 gwei"), [ctrBadgerHolderValidation.address], {'from' : wltBadgerTeam, 'value': Wei("25 ether")})

    print('Contract chain deployment complete.')