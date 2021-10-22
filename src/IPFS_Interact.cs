using Ethereum_Test2.src.logger;
using HeyRed.Mime;
using Ipfs.CoreApi;
using Ipfs.Engine;
using Ipfs.Http;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum_Test2.src {
    class IPFS_Interact {

        //private readonly IpfsClient defaultGateway = new IpfsClient("https://ipfs.infura.io/ipfs/");

        private readonly IpfsClient defaultGateway = new IpfsClient("https://ipfs.infura.io:5001");
        //static readonly IpfsClient defaultGateway = new IpfsClient();

        private IpfsClient gateway;
        private string storePath;
        public IPFS_Interact() {
            this.gateway = defaultGateway;
            this.storePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
        }

        public IPFS_Interact(string gateway) : this() {
            this.gateway = new IpfsClient(gateway);
        }

        public IPFS_Interact(string gateway, string storePath) : this(gateway) {
            this.storePath = storePath;
        }

        public async Task<bool> GetIPFSFile() {//TODO: Finish, Gets a file from IPFS, likely needs to return an actual file or file buffer.
            string path = "QmSgiPTvE9XZo6YvSs8Xw9HW311aAxLnz9qGqgZDNFj8xj";
            using (Stream stream = await gateway.FileSystem.ReadFileAsync(path)) {
                try {
                    //string bitString = stream.ReadByte().ToString();
                    Stack<byte> bytes = new Stack<byte>();
                    int bits = stream.ReadByte();
                    while (bits != -1) {
                        bytes.Push((byte)bits);
                        bits = stream.ReadByte();
                    }
                    byte[] file = new byte[bytes.Count];
                    //byte[] file2 = new byte[bytes.Count];
                    for (int i = 1; i <= file.Length; i++) {
                        file[^i] = bytes.Pop();
                    }
                    string fileExtension = MimeGuesser.GuessFileType(file).Extension;
                    string fileName = this.storePath + "StoredFile." + fileExtension;
                    Log.InfoLog(fileName);
                    using (Stream storeFile = File.Create(fileName)) {
                        try {
                            storeFile.Write(file);
                        }catch(Exception e) {
                            Log.ErrorLog(e);
                        }
                    }

                } catch(Exception e) {
                    Log.ErrorLog( e.Message.ToString() + "\r\n" + e.StackTrace.ToString());
                }
            }
            return false;//todo: fix
        }
        public async Task<string> SetFileToIPFS(string path) {//TODO: Finish, Set a file in ipfs https://github.com/richardschneider/net-ipfs-http-client/blob/master/doc/articles/filesystem.md, should return a CID(hashcode) string or null
            try {
                string output = (await gateway.FileSystem.AddFileAsync(path)).Id.ToString();
                return output;
                //todo: investigate file options and cancellation tokens
            } catch (Exception e) {
                Log.ErrorLog(e.GetType() + "\r\n" + e.Message.ToString() + "\r\n" + e.Message.ToString());
                return "";
            }
        }
    }
}