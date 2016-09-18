namespace Blacktau.OpenAuth.VersionOneA
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Blacktau.OpenAuth.Interfaces.VersionOneA;

    public class NonceFactory : INonceFactory
    {
        public string GenerateNonce()
        {
            var bytes = new byte[32];
            var first = Guid.NewGuid().ToByteArray();
            var second = Guid.NewGuid().ToByteArray();
            for (var i = 0; i < 16; i++)
            {
                bytes[i] = first[i];
            }

            for (var i = 16; i < 32; i++)
            {
                bytes[i] = second[i - 16];
            }

            var result = Convert.ToBase64String(bytes);
            result = new string(result.ToCharArray().Where(char.IsLetter).ToArray());
            return result;
        }
    }
}