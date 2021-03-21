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

namespace Contracts.Contracts.Standard_Token.ContractDefinition
{


    public partial class Standard_TokenDeployment : Standard_TokenDeploymentBase
    {
        public Standard_TokenDeployment() : base(BYTECODE) { }
        public Standard_TokenDeployment(string byteCode) : base(byteCode) { }
    }

    public class Standard_TokenDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040523480156200001157600080fd5b5060405162000a6f38038062000a6f8339810160408190526200003491620001e9565b336000908152602081815260409091208590556002859055835162000060916003919086019062000090565b506004805460ff191660ff841617905580516200008590600590602084019062000090565b5050505050620002c7565b8280546200009e9062000274565b90600052602060002090601f016020900481019282620000c257600085556200010d565b82601f10620000dd57805160ff19168380011785556200010d565b828001600101855582156200010d579182015b828111156200010d578251825591602001919060010190620000f0565b506200011b9291506200011f565b5090565b5b808211156200011b576000815560010162000120565b600082601f83011262000147578081fd5b81516001600160401b0380821115620001645762000164620002b1565b604051601f8301601f19908116603f011681019082821181831017156200018f576200018f620002b1565b81604052838152602092508683858801011115620001ab578485fd5b8491505b83821015620001ce5785820183015181830184015290820190620001af565b83821115620001df57848385830101525b9695505050505050565b60008060008060808587031215620001ff578384fd5b845160208601519094506001600160401b03808211156200021e578485fd5b6200022c8883890162000136565b94506040870151915060ff8216821462000244578384fd5b60608701519193508082111562000259578283fd5b50620002688782880162000136565b91505092959194509250565b6002810460018216806200028957607f821691505b60208210811415620002ab57634e487b7160e01b600052602260045260246000fd5b50919050565b634e487b7160e01b600052604160045260246000fd5b61079880620002d76000396000f3fe608060405234801561001057600080fd5b50600436106100a95760003560e01c8063313ce56711610071578063313ce567146101395780635c6581651461015857806370a082311461018357806395d89b4114610196578063a9059cbb1461019e578063dd62ed3e146101b1576100a9565b806306fdde03146100ae578063095ea7b3146100cc57806318160ddd146100ef57806323b872dd1461010657806327e235e314610119575b600080fd5b6100b66101ea565b6040516100c3919061068f565b60405180910390f35b6100df6100da366004610666565b610278565b60405190151581526020016100c3565b6100f860025481565b6040519081526020016100c3565b6100df61011436600461062b565b6102e4565b6100f86101273660046105d8565b60006020819052908152604090205481565b6004546101469060ff1681565b60405160ff90911681526020016100c3565b6100f86101663660046105f9565b600160209081526000928352604080842090915290825290205481565b6100f86101913660046105d8565b610490565b6100b66104af565b6100df6101ac366004610666565b6104bc565b6100f86101bf3660046105f9565b6001600160a01b03918216600090815260016020908152604080832093909416825291909152205490565b600380546101f790610711565b80601f016020809104026020016040519081016040528092919081815260200182805461022390610711565b80156102705780601f1061024557610100808354040283529160200191610270565b820191906000526020600020905b81548152906001019060200180831161025357829003601f168201915b505050505081565b3360008181526001602090815260408083206001600160a01b038716808552925280832085905551919290917f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925906102d39086815260200190565b60405180910390a350600192915050565b6001600160a01b03831660008181526001602090815260408083203384528252808320549383529082905281205490919083118015906103245750828110155b61039b5760405162461bcd60e51b815260206004820152603960248201527f746f6b656e2062616c616e6365206f7220616c6c6f77616e6365206973206c6f60448201527f776572207468616e20616d6f756e74207265717565737465640000000000000060648201526084015b60405180910390fd5b6001600160a01b038416600090815260208190526040812080548592906103c39084906106e2565b90915550506001600160a01b038516600090815260208190526040812080548592906103f09084906106fa565b9091555050600019811015610438576001600160a01b0385166000908152600160209081526040808320338452909152812080548592906104329084906106fa565b90915550505b836001600160a01b0316856001600160a01b03167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef8560405161047d91815260200190565b60405180910390a3506001949350505050565b6001600160a01b0381166000908152602081905260409020545b919050565b600580546101f790610711565b336000908152602081905260408120548211156105335760405162461bcd60e51b815260206004820152602f60248201527f746f6b656e2062616c616e6365206973206c6f776572207468616e207468652060448201526e1d985b1d59481c995c5d595cdd1959608a1b6064820152608401610392565b33600090815260208190526040812080548492906105529084906106fa565b90915550506001600160a01b0383166000908152602081905260408120805484929061057f9084906106e2565b90915550506040518281526001600160a01b0384169033907fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef906020016102d3565b80356001600160a01b03811681146104aa57600080fd5b6000602082840312156105e9578081fd5b6105f2826105c1565b9392505050565b6000806040838503121561060b578081fd5b610614836105c1565b9150610622602084016105c1565b90509250929050565b60008060006060848603121561063f578081fd5b610648846105c1565b9250610656602085016105c1565b9150604084013590509250925092565b60008060408385031215610678578182fd5b610681836105c1565b946020939093013593505050565b6000602080835283518082850152825b818110156106bb5785810183015185820160400152820161069f565b818111156106cc5783604083870101525b50601f01601f1916929092016040019392505050565b600082198211156106f5576106f561074c565b500190565b60008282101561070c5761070c61074c565b500390565b60028104600182168061072557607f821691505b6020821081141561074657634e487b7160e01b600052602260045260246000fd5b50919050565b634e487b7160e01b600052601160045260246000fdfea264697066735822122036300200790f2937eccff24e83ae43909ca3e5f5283d8047d98aa6a8e284598c64736f6c63430008020033";
        public Standard_TokenDeploymentBase() : base(BYTECODE) { }
        public Standard_TokenDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("uint256", "_initialAmount", 1)]
        public virtual BigInteger InitialAmount { get; set; }
        [Parameter("string", "_tokenName", 2)]
        public virtual string TokenName { get; set; }
        [Parameter("uint8", "_decimalUnits", 3)]
        public virtual byte DecimalUnits { get; set; }
        [Parameter("string", "_tokenSymbol", 4)]
        public virtual string TokenSymbol { get; set; }
    }

    public partial class AllowanceFunction : AllowanceFunctionBase { }

    [Function("allowance", "uint256")]
    public class AllowanceFunctionBase : FunctionMessage
    {
        [Parameter("address", "_owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("address", "_spender", 2)]
        public virtual string Spender { get; set; }
    }

    public partial class AllowedFunction : AllowedFunctionBase { }

    [Function("allowed", "uint256")]
    public class AllowedFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 { get; set; }
    }

    public partial class ApproveFunction : ApproveFunctionBase { }

    [Function("approve", "bool")]
    public class ApproveFunctionBase : FunctionMessage
    {
        [Parameter("address", "_spender", 1)]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class BalanceOfFunction : BalanceOfFunctionBase { }

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunctionBase : FunctionMessage
    {
        [Parameter("address", "_owner", 1)]
        public virtual string Owner { get; set; }
    }

    public partial class BalancesFunction : BalancesFunctionBase { }

    [Function("balances", "uint256")]
    public class BalancesFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class DecimalsFunction : DecimalsFunctionBase { }

    [Function("decimals", "uint8")]
    public class DecimalsFunctionBase : FunctionMessage
    {

    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
    {

    }

    public partial class SymbolFunction : SymbolFunctionBase { }

    [Function("symbol", "string")]
    public class SymbolFunctionBase : FunctionMessage
    {

    }

    public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

    [Function("totalSupply", "uint256")]
    public class TotalSupplyFunctionBase : FunctionMessage
    {

    }

    public partial class TransferFunction : TransferFunctionBase { }

    [Function("transfer", "bool")]
    public class TransferFunctionBase : FunctionMessage
    {
        [Parameter("address", "_to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class TransferFromFunction : TransferFromFunctionBase { }

    [Function("transferFrom", "bool")]
    public class TransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "_from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "_to", 2)]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 3)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class ApprovalEventDTO : ApprovalEventDTOBase { }

    [Event("Approval")]
    public class ApprovalEventDTOBase : IEventDTO
    {
        [Parameter("address", "_owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("address", "_spender", 2, true )]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "_value", 3, false )]
        public virtual BigInteger Value { get; set; }
    }

    public partial class TransferEventDTO : TransferEventDTOBase { }

    [Event("Transfer")]
    public class TransferEventDTOBase : IEventDTO
    {
        [Parameter("address", "_from", 1, true )]
        public virtual string From { get; set; }
        [Parameter("address", "_to", 2, true )]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 3, false )]
        public virtual BigInteger Value { get; set; }
    }

    public partial class AllowanceOutputDTO : AllowanceOutputDTOBase { }

    [FunctionOutput]
    public class AllowanceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "remaining", 1)]
        public virtual BigInteger Remaining { get; set; }
    }

    public partial class AllowedOutputDTO : AllowedOutputDTOBase { }

    [FunctionOutput]
    public class AllowedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class BalanceOfOutputDTO : BalanceOfOutputDTOBase { }

    [FunctionOutput]
    public class BalanceOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "balance", 1)]
        public virtual BigInteger Balance { get; set; }
    }

    public partial class BalancesOutputDTO : BalancesOutputDTOBase { }

    [FunctionOutput]
    public class BalancesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class DecimalsOutputDTO : DecimalsOutputDTOBase { }

    [FunctionOutput]
    public class DecimalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class SymbolOutputDTO : SymbolOutputDTOBase { }

    [FunctionOutput]
    public class SymbolOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

    [FunctionOutput]
    public class TotalSupplyOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }




}
