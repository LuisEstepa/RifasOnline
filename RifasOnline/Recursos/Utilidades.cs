﻿using System.Security.Cryptography;
using System.Text;

namespace RifasOnline.Recursos
{
    public class Utilidades
    {

        public static string EncriptarClave(string clave) { 
        
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create()) {
                Encoding enc = Encoding.UTF8;

                // obtener el hash del texto recibido
                byte[] result = hash.ComputeHash(enc.GetBytes(clave));
                // convertir el array byte en cadena de texto
                foreach(byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();

        }

        public static string GenerarToken()
        {
            string token = Guid.NewGuid().ToString("N");
            return token;
        }

    }
}
