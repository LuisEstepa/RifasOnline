﻿using RifasOnline.Models.DTO;

namespace RifasOnline.Servicios.Contrato
{
    public interface IEmailService
    {
        void SendEmail(CorreoDTO request);
    }
}
