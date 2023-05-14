using System;

namespace AddressGenerator
{
    /// <summary>
    /// Interfaccia per la generazione di indirizzi IP.
    /// </summary>
    public interface IAddress
    {
        /// <summary>
        /// Genera un indirizzo IP v4 valido.
        /// </summary>
        /// <returns>Indirizzo IP v4 generato.</returns>
        string generateIPv4();

        /// <summary>
        /// Genera una subnet mask valida.
        /// </summary>
        /// <returns>Subnet mask generata.</returns>
        string generateSubnet();
    }

    /// <summary>
    /// Classe che genera un indirizzo IP v4 valido a partire da 32 bit.
    /// </summary>
    public class AddressGenerator : IAddress
    {
        private int _bit32;

        /// <summary>
        /// Costruttore della classe AddressGenerator.
        /// </summary>
        /// <param name="bit32">32 bit da utilizzare per la generazione dell'indirizzo IP.</param>
        public AddressGenerator(int bit32)
        {
            _bit32 = bit32;
        }

        /// <summary>
        /// Genera un indirizzo IP v4 valido a partire dai 32 bit configurati.
        /// </summary>
        /// <returns>Indirizzo IP v4 generato.</returns>
        public string generateIPv4()
        {
            //Converte input intero a una stringa
            string ip = _bit32.ToString();
            // Array che conterrà i quattro ottetti dell'indirizzo IP.
            int[] otteto = new int[4];


            // Estrae i quattro ottetti dall'indirizzo a 32 bit.
            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    otteto[i] += (int)(Convert.ToInt32(ip[k]) * Math.Pow(2, 7 - j));
                    k++;
                }
               
            }

            // Costruisce l'indirizzo IP v4 come stringa.
            return $"{otteto[0]}.{otteto[1]}.{otteto[2]}.{otteto[3]}";
        }

        /// <summary>
        /// Genera una subnet mask valida.
        /// </summary>
        /// <returns>Subnet mask generata.</returns>
        public string generateSubnet()
        {
            string ip = _bit32.ToString();
            int[] SubMask = new int[4];
            // Calcola il numero di bit della subnet mask a partire dai primi bit a 1.
            int bits = 0;
            for (int i = 31; i >= 0; i--)
            {
                if (ip[i] != 0)
                {
                    bits++;
                }
                else
                {
                    break;
                }
            }
            int K, h = 1;
            for(int i = 0; i < 4; i++)
            {
                K = 7;
                for(int j = h*8; j < (32-bits) && j < (h + 1)*8; j++)
                {
                    SubMask[i] += (int)Math.Pow(2, K);
                    K--;
                }
                h++;
            }
            // Costruisce la subnet mask come stringa.
            return $"{SubMask[0]}.{SubMask[1]}.{SubMask[2]}.{SubMask[3]}";
        }
    }
}
