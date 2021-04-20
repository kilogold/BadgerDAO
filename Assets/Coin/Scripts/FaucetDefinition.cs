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
    public partial class GetElapsedTimeFunction : GetElapsedTimeFunctionBase
    {
    }

    [Function("getElapsedTime", "uint256")]
    public class GetElapsedTimeFunctionBase : FunctionMessage
    {
        [Parameter("address", "participant", 1)]
        public virtual string Participant { get; set; }
    }

    public partial class GrantFunction : GrantFunctionBase
    {
    }

    [Function("grant")]
    public class GrantFunctionBase : FunctionMessage
    {
        [Parameter("address", "recipient", 1)] public virtual string Recipient { get; set; }
        [Parameter("uint8", "score", 2)] public virtual byte Score { get; set; }
        [Parameter("uint8", "fromTotal", 3)] public virtual byte FromTotal { get; set; }
    }

    public partial class MaxDistributionPerGrantFunction : MaxDistributionPerGrantFunctionBase
    {
    }

    [Function("maxDistributionPerGrant", "uint256")]
    public class MaxDistributionPerGrantFunctionBase : FunctionMessage
    {
    }

    public partial class OwnerFunction : OwnerFunctionBase
    {
    }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {
    }

    public partial class ParticipantRetryTimeFunction : ParticipantRetryTimeFunctionBase
    {
    }

    [Function("participantRetryTime", "uint256")]
    public class ParticipantRetryTimeFunctionBase : FunctionMessage
    {
    }

    public partial class RenounceOwnershipFunction : RenounceOwnershipFunctionBase
    {
    }

    [Function("renounceOwnership")]
    public class RenounceOwnershipFunctionBase : FunctionMessage
    {
    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase
    {
    }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "newOwner", 1)] public virtual string NewOwner { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase
    {
    }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "previousOwner", 1, true)]
        public virtual string PreviousOwner { get; set; }

        [Parameter("address", "newOwner", 2, true)]
        public virtual string NewOwner { get; set; }
    }

    public partial class GetElapsedTimeOutputDTO : GetElapsedTimeOutputDTOBase
    {
    }

    [FunctionOutput]
    public class GetElapsedTimeOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)] public virtual BigInteger ReturnValue1 { get; set; }
    }


    public partial class MaxDistributionPerGrantOutputDTO : MaxDistributionPerGrantOutputDTOBase
    {
    }

    [FunctionOutput]
    public class MaxDistributionPerGrantOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)] public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class OwnerOutputDTO : OwnerOutputDTOBase
    {
    }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "", 1)] public virtual string ReturnValue1 { get; set; }
    }

    public partial class ParticipantRetryTimeOutputDTO : ParticipantRetryTimeOutputDTOBase
    {
    }

    [FunctionOutput]
    public class ParticipantRetryTimeOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)] public virtual BigInteger ReturnValue1 { get; set; }
    }
}