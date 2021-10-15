using Ethereum_Test2.src.logger;
using Ipfs.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum_Test2.src {
    class IPFS_Interact {

        private readonly IpfsClient defaultGateway = new IpfsClient("https://ipfs.infura.io/ipfs/");
        private IpfsClient gateway;
        private string storePath;
        public IPFS_Interact() {
            this.gateway = defaultGateway;
            this.storePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
        }

        public IPFS_Interact(string gateway):this() {
            this.gateway = new IpfsClient(gateway);
        }

        public IPFS_Interact(string gateway, string storePath):this(gateway) {
            this.storePath = storePath;
        }

        public async Task<bool> GetIPFSFile() {//TODO: Finish, Gets a file from IPFS, likely needs to return an actual file or file buffer.
            string path = "Qmf412jQZiuVUtdgnB36FXFX7xg5V6KEbSJ4dpQuhkLyfD";
            using (var stream = await gateway.FileSystem.ReadFileAsync(path)) {
                // Do something with the data
            }
            return false;//todo: fix
        }

        public async Task<string> SetFileToIPFS(string path) {//TODO: Finish, Set a file in ipfs https://github.com/richardschneider/net-ipfs-http-client/blob/master/doc/articles/filesystem.md, should return a CID(hashcode) string or null
            Log.InfoLog("Setfiletoipfs");
            try{
                return (await gateway.FileSystem.AddFileAsync(path)).Id.ToString();//todo: investigate file options and cancellation tokens
            }catch(Exception e) {
                Log.ErrorLog(e.StackTrace.ToString());
                Log.ErrorLog(e.Message.ToString());
                return null;
            }
        }
    }
}
/*
        this.state = {
            account: '',
            buffer: null,
            defaultMemeHash: 'QmZHd1fbAsE4j281P69a9gR8UdoK3G8DsJ2G7oxVQ8osQ3',
            memeHash: '',
            memeHashLoc: () => {
                return '' + protocol + '://' + domain + '/ipfs/';
            },
            contract: null,
        };


*/
