using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum_Test2.src {
    class Ethernet {

        public string AccBal { get; private set; }
        //public Contract _Contract { get; private set; }

        private readonly string localAbi = "[{\"constant\":false,\"inputs\":[{\"name\":\"_memeHash\",\"type\":\"string\"}],\"name\":\"set\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\",\"signature\":\"0x4ed3885e\"},{\"constant\":true,\"inputs\":[],\"name\":\"get\",\"outputs\":[{\"name\":\"\",\"type\":\"string\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\",\"signature\":\"0x6d4ce63c\"}]";
        private readonly string testAbi = "[{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"bool\",\"name\":\"success\",\"type\":\"bool\"},{\"indexed\":true,\"internalType\":\"bytes\",\"name\":\"result\",\"type\":\"bytes\"}],\"name\":\"ExecutionResult\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"target\",\"type\":\"address\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"foo\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        private readonly string prodAbi = null;

        private readonly string testAccount = "0xd0fdc799d8125AAb9992e4c7470efB873d1d57dB";
        private readonly string testContractAccount = "0x4a1171207E34b11d002B64332D0c7F7D4ED86AEa";

        private readonly string localAccount = "0xe0d9F6E40f8c3fd3b121F54d09E069d51Ba64D96";
        private readonly string localContractAccount = "0xe0d9F6E40f8c3fd3b121F54d09E069d51Ba64D96";

        private readonly string prodAccount = null;
        private readonly string prodContractAccount = null;

        private readonly Web3 localWeb3;
        private readonly Web3 testNet;
        private readonly Web3 prodNet;

        private Web3 envWeb3;
        private string envAbi;
        public string EnvAccount { get; private set; }
        public string EnvContractAccount { get; private set; }


        public enum Crypto { LOCAL, ROPSTEN, PROD }

        private Ethernet() {
            try {
                this.localWeb3 = new Web3("HTTP://127.0.0.1:8545");
            } catch (Exception e) { }
            try {
                this.testNet = new Web3("https://ropsten.infura.io/v3/c403a4afb4f5439588595f1f242e7c75");
            } catch (Exception e) { }
            try {
                this.prodNet = null;
            } catch (Exception e) { }
        }

        public Ethernet(Crypto env) : this() {
            this.SetEnvironment(env);
        }

        public void SetEnvironment(Crypto env) {
            switch (env) {
                case Crypto.LOCAL:
                    this.envWeb3 = this.localWeb3;
                    this.envAbi = this.localAbi;
                    this.EnvAccount = this.localAccount;
                    this.EnvContractAccount = this.localContractAccount;
                    break;
                case Crypto.ROPSTEN:
                    this.envWeb3 = this.testNet;
                    this.envAbi = this.testAbi;
                    this.EnvAccount = this.testAccount;
                    this.EnvContractAccount = this.testContractAccount;
                    break;
                case Crypto.PROD:
                    this.envWeb3 = this.prodNet;
                    this.envAbi = this.prodAbi;
                    this.EnvAccount = this.prodAccount;
                    this.EnvContractAccount = this.prodContractAccount;
                    break;
            }
        }


        public async Task GetAccountBalance(string account) {
            this.AccBal = "Balence = " + (await this.envWeb3.Eth.GetBalance.SendRequestAsync(account)).Value.ToString();
        }

        public Contract GetContract(string account) {
            return this.envWeb3.Eth.GetContract(this.envAbi, account);
        }
    }
}