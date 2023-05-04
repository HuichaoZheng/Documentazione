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
        private uint _bit32;

        /// <summary>
        /// Costruttore della classe AddressGenerator.
        /// </summary>
        /// <param name="bit32">32 bit da utilizzare per la generazione dell'indirizzo IP.</param>
        public AddressGenerator(uint bit32)
        {
            _bit32 = bit32;
        }

        /// <summary>
        /// Genera un indirizzo IP v4 valido a partire dai 32 bit configurati.
        /// </summary>
        /// <returns>Indirizzo IP v4 generato.</returns>
        public string generateIPv4()
        {
            // Maschera per estrarre i primi 8 bit.
            uint mask = 0xff000000;

            // Array che conterrà i quattro ottetti dell'indirizzo IP.
            byte[] octets = new byte[4];

            // Estrae i quattro ottetti dall'indirizzo a 32 bit.
            for (int i = 0; i < octets.Length; i++)
            {
                octets[i] = (byte)((_bit32 & mask) >> ((3 - i) * 8));
                mask = mask >> 8;
            }

            // Costruisce l'indirizzo IP v4 come stringa.
            return $"{octets[0]}.{octets[1]}.{octets[2]}.{octets[3]}";
        }

        /// <summary>
        /// Genera una subnet mask valida.
        /// </summary>
        /// <returns>Subnet mask generata.</returns>
        public string generateSubnet()
        {
            // Calcola il numero di bit della subnet mask a partire dai primi bit a 1.
            int bits = 0;
            uint mask = 0x80000000;
            while ((_bit32 & mask) != 0)
            {
                bits++;
                mask = mask >> 1;
            }

            // Costruisce la subnet mask come stringa.
            uint value = 0xffffffff << (32 - bits);
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return new System.Net.IPAddress(bytes).ToString();
        }
    }
}