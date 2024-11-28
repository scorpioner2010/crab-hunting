using UnityEngine;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using System.Numerics;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Nethereum.Contracts;
using Nethereum.Web3.Accounts;

public class BlockchainManagerBase : MonoBehaviour
{
    protected Web3 _Web3;
    protected string _RPCUrl;
    protected string _ContractAddress = "0xDA4886f2A93759942C7D0A0AEe0EC93B1682Ebd4";
    protected string _ContractABI = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"Mint\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"allowance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"decimals\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"mint\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
    private readonly string _OperaRpcUrl = "https://rpc.ftm.tools/";

    private void Start()
    {
        _RPCUrl = _OperaRpcUrl;
        _Web3 = new Web3(_RPCUrl);
        Debug.Log("Підключення до мережі Fantom Opera виконано.");
    }

    public string fromAddressN = "0x134eb3bADd134e9f7Fd06DFB7F0C1c724CB13E5B";
    public string privateKeyN = "d6262cab48cca9106fafb102c8a7fe774733e8f7d80012c721171a1d69550eaa";
    public string toAddressN = "0x60A9fDC0619dA2ddBA4B2B9978b723ACFAe4315E";
    public decimal amountN = 50m;

    [Button]
    public void TransferTokensButton()
    {
        TransferTokens(fromAddressN, privateKeyN, toAddressN, amountN).Forget();
    }

    public async UniTask TransferTokens(string fromAddress, string privateKey, string toAddress, decimal amount)
    {
        try
        {
            Debug.Log($"Підготовка до надсилання токенів: з {fromAddress} до {toAddress}, сума: {amount} токенів.");

            // Ініціалізація облікового запису
            Account account = new Account(privateKey);
            Web3 web3 = new Web3(account, _RPCUrl);

            // Отримання контракту
            Contract contract = web3.Eth.GetContract(_ContractABI, _ContractAddress);
            Function transferFunction = contract.GetFunction("transfer");

            Debug.Log($"Виклик функції transfer з параметрами: toAddress={toAddress}, amount={Web3.Convert.ToWei(amount)}.");

            // Перетворення кількості токенів у BigInteger
            BigInteger amountWei = Web3.Convert.ToWei(amount);

            // Надсилання транзакції
            var gasLimit = new HexBigInteger(200000); // Збільшений ліміт газу
            var gasPrice = new HexBigInteger(await web3.Eth.GasPrice.SendRequestAsync()); // Отримання ціни газу

            // Перевірка балансу на оплату газу
            var balance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);
            if (balance.Value < gasPrice.Value * gasLimit.Value)
            {
                throw new System.Exception("Недостатньо коштів для покриття витрат на газ.");
            }

            string transactionHash = await transferFunction.SendTransactionAsync(
                account.Address,
                gasLimit,
                gasPrice,
                new HexBigInteger(0),
                toAddress,
                amountWei
            );

            Debug.Log($"Хеш транзакції: {transactionHash}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Помилка надсилання токенів: {e.Message}");
        }
    }
}