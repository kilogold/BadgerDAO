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
        public static string BYTECODE = "61010060405234801561001157600080fd5b5060405161070438038061070483398101604081905261003091610056565b6001600160601b0319606091821b1660805260a09290925260c05233901b60e05261009a565b60008060006060848603121561006a578283fd5b83516020850151604086015191945092506001600160a01b038116811461008f578182fd5b809150509250925092565b60805160601c60a05160c05160e05160601c61060b6100f96000396000818160f5015261018c01526000818160ce015261046f015260008181606c01526101b80152600081816101340152818161026101526103c2015261060b6000f3fe608060405234801561001057600080fd5b50600436106100625760003560e01c806347efeb5a146100675780636bb76ce4146100a1578063746f5299146100b4578063b464f42a146100c9578063bf7e214f146100f0578063f07fa1811461012f575b600080fd5b61008e7f000000000000000000000000000000000000000000000000000000000000000081565b6040519081526020015b60405180910390f35b61008e6100af3660046104d5565b610156565b6100c76100c23660046104ef565b610181565b005b61008e7f000000000000000000000000000000000000000000000000000000000000000081565b6101177f000000000000000000000000000000000000000000000000000000000000000081565b6040516001600160a01b039091168152602001610098565b6101177f000000000000000000000000000000000000000000000000000000000000000081565b6001600160a01b03811660009081526020819052604081205461017990426105a8565b90505b919050565b336001600160a01b037f000000000000000000000000000000000000000000000000000000000000000016146101b657600080fd5b7f00000000000000000000000000000000000000000000000000000000000000006101e084610156565b116102325760405162461bcd60e51b815260206004820152601760248201527f526574727920636f6f6c646f776e206e6f74206d65742e00000000000000000060448201526064015b60405180910390fd5b600061023e8383610467565b6040516370a0823160e01b81523060048201529091506000906001600160a01b037f000000000000000000000000000000000000000000000000000000000000000016906370a082319060240160206040518083038186803b1580156102a357600080fd5b505afa1580156102b7573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906102db9190610551565b90506000811161032d5760405162461bcd60e51b815260206004820152601f60248201527f436f6e747261637420686173207a65726f20746f6b656e2062616c616e6365006044820152606401610229565b808211156103935760405162461bcd60e51b815260206004820152602d60248201527f4e6f7420656e6f7567682062616c616e636520617661696c61626c6520696e2060448201526c3a34329031b7b73a3930b1ba1760991b6064820152608401610229565b60ff8416156104475760405163a9059cbb60e01b81526001600160a01b038681166004830152602482018490527f0000000000000000000000000000000000000000000000000000000000000000169063a9059cbb90604401602060405180830381600087803b15801561040657600080fd5b505af115801561041a573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525081019061043e9190610531565b61044757600080fd5b505050506001600160a01b03166000908152602081905260409020429055565b60008160ff167f00000000000000000000000000000000000000000000000000000000000000008460ff1661049c9190610589565b6104a69190610569565b9392505050565b80356001600160a01b038116811461017c57600080fd5b803560ff8116811461017c57600080fd5b6000602082840312156104e6578081fd5b6104a6826104ad565b600080600060608486031215610503578182fd5b61050c846104ad565b925061051a602085016104c4565b9150610528604085016104c4565b90509250925092565b600060208284031215610542578081fd5b815180151581146104a6578182fd5b600060208284031215610562578081fd5b5051919050565b60008261058457634e487b7160e01b81526012600452602481fd5b500490565b60008160001904831182151516156105a3576105a36105bf565b500290565b6000828210156105ba576105ba6105bf565b500390565b634e487b7160e01b600052601160045260246000fdfea26469706673582212204b788687a68e5fab14aea8a6098d4f1a3154055d99c72d0dddc664bd199832a964736f6c63430008030033";
        public FaucetDeploymentBase() : base(BYTECODE) { }
        public FaucetDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("uint256", "participantRetryTimeIn", 1)]
        public virtual BigInteger ParticipantRetryTimeIn { get; set; }
        [Parameter("uint256", "maxDistributionPerGrantIn", 2)]
        public virtual BigInteger MaxDistributionPerGrantIn { get; set; }
        [Parameter("address", "tokenContract", 3)]
        public virtual string TokenContract { get; set; }
    }

    public partial class FAUCET_TOKENFunction : FAUCET_TOKENFunctionBase { }

    [Function("FAUCET_TOKEN", "address")]
    public class FAUCET_TOKENFunctionBase : FunctionMessage
    {

    }

    public partial class AuthorityFunction : AuthorityFunctionBase { }

    [Function("authority", "address")]
    public class AuthorityFunctionBase : FunctionMessage
    {

    }

    public partial class GetElapsedTimeFunction : GetElapsedTimeFunctionBase { }

    [Function("getElapsedTime", "uint256")]
    public class GetElapsedTimeFunctionBase : FunctionMessage
    {
        [Parameter("address", "participant", 1)]
        public virtual string Participant { get; set; }
    }

    public partial class GrantFunction : GrantFunctionBase { }

    [Function("grant")]
    public class GrantFunctionBase : FunctionMessage
    {
        [Parameter("address", "recipient", 1)]
        public virtual string Recipient { get; set; }
        [Parameter("uint8", "score", 2)]
        public virtual byte Score { get; set; }
        [Parameter("uint8", "fromTotal", 3)]
        public virtual byte FromTotal { get; set; }
    }

    public partial class MaxDistributionPerGrantFunction : MaxDistributionPerGrantFunctionBase { }

    [Function("maxDistributionPerGrant", "uint256")]
    public class MaxDistributionPerGrantFunctionBase : FunctionMessage
    {

    }

    public partial class ParticipantRetryTimeFunction : ParticipantRetryTimeFunctionBase { }

    [Function("participantRetryTime", "uint256")]
    public class ParticipantRetryTimeFunctionBase : FunctionMessage
    {

    }

    public partial class FAUCET_TOKENOutputDTO : FAUCET_TOKENOutputDTOBase { }

    [FunctionOutput]
    public class FAUCET_TOKENOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class AuthorityOutputDTO : AuthorityOutputDTOBase { }

    [FunctionOutput]
    public class AuthorityOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetElapsedTimeOutputDTO : GetElapsedTimeOutputDTOBase { }

    [FunctionOutput]
    public class GetElapsedTimeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class MaxDistributionPerGrantOutputDTO : MaxDistributionPerGrantOutputDTOBase { }

    [FunctionOutput]
    public class MaxDistributionPerGrantOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class ParticipantRetryTimeOutputDTO : ParticipantRetryTimeOutputDTOBase { }

    [FunctionOutput]
    public class ParticipantRetryTimeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}
