using UnityEngine;
using System.Security.Cryptography;

public class TrueRandom : MonoBehaviour
{

    public static byte Rnd()
    {
        RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
        byte[] randomNumber = new byte[1];
        rand.GetBytes(randomNumber);
        return (randomNumber[0]);
    }


}
