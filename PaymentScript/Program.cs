using System;
using NBitcoin;
// ReSharper disable All

namespace PaymentScript
{
    class Program
    {
        static void Main()
        {
            //There is no know "bitcoin Address",all it goes to
            //it actually all they care is ScriptPubKey
            var publicKeyHash = new KeyId("14836dbe7f38c5ac3d49e8d790af808a4ee9edcf");

            var testNetAddress = publicKeyHash.GetAddress(Network.TestNet);
            var mainNetAddress = publicKeyHash.GetAddress(Network.Main);

            //** Here is what ScriptPubKey look like
            Console.WriteLine("The MainNetAddress's ScriptPubKey "+mainNetAddress.ScriptPubKey); // OP_DUP OP_HASH160 14836dbe7f38c5ac3d49e8d790af808a4ee9edcf OP_EQUALVERIFY OP_CHECKSIG
            Console.WriteLine("The TestNetAddress's ScriptPubKey " + testNetAddress.ScriptPubKey); // OP_DUP OP_HASH160 14836dbe7f38c5ac3d49e8d790af808a4ee9edcf OP_EQUALVERIFY OP_CHECKSIG
            //They look the same, Are they really the same?
            
            var paymentScript = publicKeyHash.ScriptPubKey;
            var sameMainNetAddress = paymentScript.GetDestinationAddress(Network.Main);
            Console.WriteLine("Generate bitcoin address from ScriptPubKey "mainNetAddress == sameMainNetAddress); // True
            //generate bitcoin address from ScriptPubKey

            //Retrieve the harsh from the ScriptPubKey and generate Bitcoin Address
            //The ScriptPubKey does not contains the hashed public key permitted to spend the bitcoin
            var samePublicKeyHash = (KeyId)paymentScript.GetDestination();
            Console.WriteLine(publicKeyHash == samePublicKeyHash); // True
            var sameMainNetAddress2 = new BitcoinPubKeyAddress(samePublicKeyHash, Network.Main);
            Console.WriteLine("Retrieve the harsh from the ScriptPubKey, Are they the same "mainNetAddress == sameMainNetAddress2); // True

            //From now on, we only exclusively user ScriptPubKey, Address is only a user interface concept

            Console.ReadLine();
        }
    }
}
