import pytest
import brownie
from brownie import run, accounts, Faucet, BrownieWrap_Token, TokenHolderThresholdValidator

@pytest.fixture(scope="module", autouse=True)
def deploy(module_isolation):
    run('deployment')
    wltBadgerTeam = accounts[0]
    wltUserA = accounts[1]
    wltUserB = accounts[2]
    ctrFaucet = Faucet[0]
    ctrBadgerLpToken = BrownieWrap_Token[0]


def test_only_owner_can_modify_validators(fn_isolation):
    wltBadgerTeam = accounts[0]
    wltUserA = accounts[1]
    ctrFaucet = Faucet[0]

    # User does not qualify
    with brownie.reverts("Recipient does not qualify"):
        ctrFaucet.grant(wltUserA, 10, 10, {'from' : wltBadgerTeam})

    # User attempts to disable validation, but fails because not the owner
    with brownie.reverts("Ownable: caller is not the owner"):
        ctrFaucet.configureValidators.transact((), {'from' : wltUserA})

    # Owner updates qualifications via validator change.
    ctrFaucet.configureValidators.transact((), {'from' : wltBadgerTeam})

def test_owner_disable_validators(fn_isolation):
    wltBadgerTeam = accounts[0]
    wltUserA = accounts[1]
    ctrFaucet = Faucet[0]

    # User does not qualify
    with brownie.reverts("Recipient does not qualify"):
        ctrFaucet.grant(wltUserA, 10, 10, {'from' : wltBadgerTeam})

    # Owner disables qualifications via validator change.
    ctrFaucet.configureValidators.transact((), {'from' : wltBadgerTeam})

    # User now receives grant.
    ctrFaucet.grant(wltUserA, 10, 10, {'from' : wltBadgerTeam})

def test_owner_update_validators(fn_isolation):
    wltBadgerTeam = accounts[0]
    wltUserA = accounts[1]
    ctrFaucet = Faucet[0]
    ctrBadgerLpToken = BrownieWrap_Token[0]

    # UserA does not qualify
    with brownie.reverts("Recipient does not qualify"):
        ctrFaucet.validate(wltUserA)

    # UserA receives tokens to qualify.
    ctrBadgerLpToken.approve(wltBadgerTeam, 1000)
    ctrBadgerLpToken.transferFrom(wltBadgerTeam, wltUserA, 1000)
    assert ctrBadgerLpToken.balanceOf(wltUserA) == 1000
    
    # UserA now qualifies.
    ctrFaucet.validate(wltUserA)

    # Owner creates new validation requirements to have an upper limit.
    newValidator = TokenHolderThresholdValidator.deploy(ctrBadgerLpToken.address, 500, 4, {'from': wltBadgerTeam})

    # Owner updates validation on faucet.
    originalValidator = ctrFaucet.validators(0)
    ctrFaucet.configureValidators.transact([originalValidator, newValidator], {'from' : wltBadgerTeam})

    # UserA has too many tokens to qualify.
    with brownie.reverts("Recipient does not qualify"):
        ctrFaucet.validate(wltUserA)

    # User spends enough tokens to qualify once more.
    ctrBadgerLpToken.approve(wltUserA, 500, {'from' : wltUserA})
    ctrBadgerLpToken.transferFrom(wltUserA, wltBadgerTeam, 500, {'from' : wltUserA})
    assert ctrBadgerLpToken.balanceOf(wltUserA) == 500

    # User barely qualifies for grant.
    ctrFaucet.validate(wltUserA)