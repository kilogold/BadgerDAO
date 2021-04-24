import pytest
import brownie
from brownie import run, accounts, Faucet, BrownieWrap_Token

@pytest.fixture(scope="module", autouse=True)
def deploy(module_isolation):
    run('deployment')
    wltBadgerTeam = accounts[0]
    wltUserA = accounts[1]
    wltUserB = accounts[2]
    ctrFaucet = Faucet[0]
    ctrBadgerLpToken = BrownieWrap_Token[0]

def test_deployment_account_balance_consumed():
    wltBadgerTeam = accounts[0]
    wltUserA = accounts[1]
    assert wltBadgerTeam.balance() < wltUserA.balance()

def test_only_owner_may_grant_faucet():   
    wltBadgerTeam = accounts[0]
    wltUserA = accounts[1]
    ctrFaucet = Faucet[0]

    # Revert for two reasons:
    # 1) Not the owner.
    # 2) Not qualified.
    with brownie.reverts():
        ctrFaucet.grant(wltUserA, 10, 10, {'from' : wltUserA})

    ctrFaucet.grant(wltBadgerTeam, 10, 10, {'from' : wltBadgerTeam})

def test_only_qualified_wallets_receive_grant(deploy):
    wltBadgerTeam = accounts[0]
    wltUserA = accounts[1]
    wltUserB = accounts[2]
    ctrFaucet = Faucet[0]
    ctrBadgerLpToken = BrownieWrap_Token[0]

    with brownie.reverts("Recipient does not qualify"):
        ctrFaucet.grant(wltUserA, 10, 10, {'from' : wltBadgerTeam})

    with brownie.reverts("Recipient does not qualify"):
        ctrFaucet.grant(wltUserB, 10, 10, {'from' : wltBadgerTeam})

    ctrBadgerLpToken.approve(wltBadgerTeam, 200, {'from' : wltBadgerTeam })
    ctrBadgerLpToken.transferFrom(wltBadgerTeam, wltUserA, 200, {'from' : wltBadgerTeam})

    ctrFaucet.grant(wltUserA, 10, 10, {'from' : wltBadgerTeam})
