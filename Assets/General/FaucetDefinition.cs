using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Contracts.Contracts.Faucet.ContractDefinition
{


    public partial class FaucetDeployment : FaucetDeploymentBase
    {
        public FaucetDeployment() : base(BYTECODE) { }
        public FaucetDeployment(string byteCode) : base(byteCode) { }
    }

    public class FaucetDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405260036001556040516103913803806103918339810160408190526100279161002f565b600155610047565b600060208284031215610040578081fd5b5051919050565b61033b806100566000396000f3fe6080604052600436106100385760003560e01c8063125d7020146100445780636370920e1461006c578063a7d257f61461008e5761003f565b3661003f57005b600080fd5b34801561005057600080fd5b506100596100b3565b6040519081526020015b60405180910390f35b34801561007857600080fd5b5061008c61008736600461024b565b6100d2565b005b34801561009a57600080fd5b506100a3610224565b6040519015158152602001610063565b336000908152602081905260408120546100cd90426102d8565b905090565b6100da610224565b6100e357600080fd5b6040516370a0823160e01b815230600482015273fab46e002bbf0b4509813474841e0716e6730136906370a082319060240160206040518083038186803b15801561012d57600080fd5b505afa158015610141573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525081019061016591906102a8565b81111561017157600080fd5b60405163a9059cbb60e01b81526001600160a01b03831660048201526024810182905273fab46e002bbf0b4509813474841e0716e67301369063a9059cbb90604401602060405180830381600087803b1580156101cd57600080fd5b505af11580156101e1573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906102059190610281565b61020e57600080fd5b5050336000908152602081905260409020429055565b600154336000908152602081905260408120549091429161024591906102c0565b10905090565b6000806040838503121561025d578182fd5b82356001600160a01b0381168114610273578283fd5b946020939093013593505050565b600060208284031215610292578081fd5b815180151581146102a1578182fd5b9392505050565b6000602082840312156102b9578081fd5b5051919050565b600082198211156102d3576102d36102ef565b500190565b6000828210156102ea576102ea6102ef565b500390565b634e487b7160e01b600052601160045260246000fdfea2646970667358221220cf6f42ff1fb92dc9df8197d8db1517068097f9b643d7c4b818707e9affa8ab8f64736f6c63430008020033";
        public FaucetDeploymentBase() : base(BYTECODE) { }
        public FaucetDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("uint256", "retryAmount", 1)]
        public virtual BigInteger RetryAmount { get; set; }
    }

    public partial class CanParticipateFunction : CanParticipateFunctionBase { }

    [Function("canParticipate", "bool")]
    public class CanParticipateFunctionBase : FunctionMessage
    {

    }

    public partial class GetElapsedTimeFunction : GetElapsedTimeFunctionBase { }

    [Function("getElapsedTime", "uint256")]
    public class GetElapsedTimeFunctionBase : FunctionMessage
    {

    }

    public partial class GrantFunction : GrantFunctionBase { }

    [Function("grant")]
    public class GrantFunctionBase : FunctionMessage
    {
        [Parameter("address", "recipient", 1)]
        public virtual string Recipient { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class CanParticipateOutputDTO : CanParticipateOutputDTOBase { }

    [FunctionOutput]
    public class CanParticipateOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class GetElapsedTimeOutputDTO : GetElapsedTimeOutputDTOBase { }

    [FunctionOutput]
    public class GetElapsedTimeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }


}
