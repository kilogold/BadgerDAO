using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Contracts.Contracts.Faucet.ContractDefinition;

namespace Contracts.Contracts.Faucet
{
    public partial class FaucetService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, FaucetDeployment faucetDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<FaucetDeployment>().SendRequestAndWaitForReceiptAsync(faucetDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, FaucetDeployment faucetDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<FaucetDeployment>().SendRequestAsync(faucetDeployment);
        }

        public static async Task<FaucetService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, FaucetDeployment faucetDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, faucetDeployment, cancellationTokenSource);
            return new FaucetService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public FaucetService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> FAUCET_TOKENQueryAsync(FAUCET_TOKENFunction fAUCET_TOKENFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FAUCET_TOKENFunction, string>(fAUCET_TOKENFunction, blockParameter);
        }

        
        public Task<string> FAUCET_TOKENQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FAUCET_TOKENFunction, string>(null, blockParameter);
        }

        public Task<string> AuthorityQueryAsync(AuthorityFunction authorityFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AuthorityFunction, string>(authorityFunction, blockParameter);
        }

        
        public Task<string> AuthorityQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AuthorityFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetElapsedTimeQueryAsync(GetElapsedTimeFunction getElapsedTimeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetElapsedTimeFunction, BigInteger>(getElapsedTimeFunction, blockParameter);
        }

        
        public Task<BigInteger> GetElapsedTimeQueryAsync(string participant, BlockParameter blockParameter = null)
        {
            var getElapsedTimeFunction = new GetElapsedTimeFunction();
                getElapsedTimeFunction.Participant = participant;
            
            return ContractHandler.QueryAsync<GetElapsedTimeFunction, BigInteger>(getElapsedTimeFunction, blockParameter);
        }

        public Task<string> GrantRequestAsync(GrantFunction grantFunction)
        {
             return ContractHandler.SendRequestAsync(grantFunction);
        }

        public Task<TransactionReceipt> GrantRequestAndWaitForReceiptAsync(GrantFunction grantFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantFunction, cancellationToken);
        }

        public Task<string> GrantRequestAsync(string recipient, byte score, byte fromTotal)
        {
            var grantFunction = new GrantFunction();
                grantFunction.Recipient = recipient;
                grantFunction.Score = score;
                grantFunction.FromTotal = fromTotal;
            
             return ContractHandler.SendRequestAsync(grantFunction);
        }

        public Task<TransactionReceipt> GrantRequestAndWaitForReceiptAsync(string recipient, byte score, byte fromTotal, CancellationTokenSource cancellationToken = null)
        {
            var grantFunction = new GrantFunction();
                grantFunction.Recipient = recipient;
                grantFunction.Score = score;
                grantFunction.FromTotal = fromTotal;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantFunction, cancellationToken);
        }

        public Task<BigInteger> MaxDistributionPerGrantQueryAsync(MaxDistributionPerGrantFunction maxDistributionPerGrantFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MaxDistributionPerGrantFunction, BigInteger>(maxDistributionPerGrantFunction, blockParameter);
        }

        
        public Task<BigInteger> MaxDistributionPerGrantQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MaxDistributionPerGrantFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> ParticipantRetryTimeQueryAsync(ParticipantRetryTimeFunction participantRetryTimeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ParticipantRetryTimeFunction, BigInteger>(participantRetryTimeFunction, blockParameter);
        }

        
        public Task<BigInteger> ParticipantRetryTimeQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ParticipantRetryTimeFunction, BigInteger>(null, blockParameter);
        }
    }
}
