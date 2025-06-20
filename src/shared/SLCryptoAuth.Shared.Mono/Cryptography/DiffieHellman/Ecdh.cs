﻿using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement;
using SLCryptoAuth.Cryptography.Utils;

namespace SLCryptoAuth.Cryptography.DiffieHellman;

public class Ecdh : IDiffieHellmanProvider
{
    private readonly AsymmetricCipherKeyPair _keyPair;

    public Ecdh()
    {
        _keyPair = ECKeyManager.GenerateKeyPair();
        (PrivateKeyBytes, PublicKeyBytes) = ECKeyManager.GetBytesFromKeyPair(_keyPair);
    }

    /// <inheritdoc />
    public byte[] PrivateKeyBytes { get; }

    /// <inheritdoc />
    public byte[] PublicKeyBytes { get; }

    /// <inheritdoc />
    public byte[] ComputeSharedSecret(byte[] otherPublicKeyBytes)
    {
        var otherPublicKey = ECKeyManager.GetPublicKeyFromBytes(otherPublicKeyBytes);
        var agreement = new ECDHBasicAgreement();
        agreement.Init(_keyPair.Private);
        return agreement.CalculateAgreement(otherPublicKey).ToByteArrayUnsigned();
    }
}
