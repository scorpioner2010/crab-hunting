using UnityEngine;
using Nethereum.Web3;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using System.Threading.Tasks;

public class BlockchainManager : MonoBehaviour
{
    // RPC URL для підключення до основної мережі Fantom
    private string rpcUrl = "https://rpc.ftm.tools/";
    
    // Адреса контракту (смарт-контракт, розгорнутий на Fantom)
    private string contractAddress = "0xDA4886f2A93759942C7D0A0AEe0EC93B1682Ebd4";
    
    // ABI контракту (замініть на реальну ABI вашого контракту)
    private string contractABI = @"[ABI контракту тут]"; // Додайте реальну ABI тут
    
    private Web3 web3;

    void Start()
    {
        // Ініціалізація підключення Web3 до Fantom
        web3 = new Web3(rpcUrl);
        Debug.Log("Підключення до мережі Fantom виконано.");
        
        // Приклад виклику GetBalance з тестовою адресою
        GetBalance("0xYourTestAddressHere");  // Замініть на реальну адресу для тестування
    }

    // Метод для отримання балансу криптовалюти користувача за його адресою
    public async void GetBalance(string userAddress)
    {
        try
        {
            var balance = await web3.Eth.GetBalance.SendRequestAsync(userAddress);
            Debug.Log("Баланс користувача: " + Web3.Convert.FromWei(balance.Value) + " FTM");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Помилка отримання балансу: " + e.Message);
        }
    }

    // Метод для відправки транзакції з криптовалютою
    public async void SendTransaction(string fromAddress, string privateKey, string toAddress, decimal amount)
    {
        try
        {
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var web3 = new Web3(account, rpcUrl);

            var transaction = new TransactionInput
            {
                From = fromAddress,
                To = toAddress,
                Value = new HexBigInteger(Web3.Convert.ToWei(amount)),
            };

            var transactionHash = await web3.Eth.Transactions.SendTransaction.SendRequestAsync(transaction);
            Debug.Log("Хеш транзакції: " + transactionHash);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Помилка відправки транзакції: " + e.Message);
        }
    }

    // Метод для виклику функції смарт-контракту для перевірки залишку криптовалюти (наприклад, балансу токенів)
    public async void GetRemainingCrypto()
    {
        try
        {
            // Завантаження контракту за допомогою ABI та адреси контракту
            var contract = web3.Eth.GetContract(contractABI, contractAddress);

            // Припустимо, що в контракті є функція getRemainingCrypto, яка повертає залишок
            var getRemainingCryptoFunction = contract.GetFunction("getRemainingCrypto");

            // Викликаємо функцію та отримуємо результат
            var remainingCrypto = await getRemainingCryptoFunction.CallAsync<int>();

            Debug.Log("Залишок криптовалюти: " + remainingCrypto);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Помилка отримання залишку криптовалюти: " + e.Message);
        }
    }
}