using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Contracts.WalletValidator.ContractDefinition
{
    public partial class WalletValidatorDeployment : WalletValidatorDeploymentBase
    {
        public WalletValidatorDeployment() : base(BYTECODE) { }
        public WalletValidatorDeployment(string byteCode) : base(byteCode) { }
    }

    public class WalletValidatorDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public WalletValidatorDeploymentBase() : base(BYTECODE) { }
        public WalletValidatorDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class ValidateFunction : ValidateFunctionBase { }

    [Function("Validate", "bool")]
    public class ValidateFunctionBase : FunctionMessage
    {
        [Parameter("address", "wallet", 1)]
        public virtual string Wallet { get; set; }
    }

    public partial class ValidateOutputDTO : ValidateOutputDTOBase { }

    [FunctionOutput]
    public class ValidateOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}
