﻿using System;
using System.Net;
using Providers;
using Services.Exceptions;

namespace BussinessLogic.Providers
{
    public class IpAddressProvider:IIpAddressProvider
    {
        public string GetIpAddress(string ipAddress)
        {
            try
            {
                var ip = IPAddress.Parse(ipAddress);
                return ip.ToString();
            }
            catch (ArgumentNullException e)
            {
                throw new ServiceException($"Ошибка в аргументе {nameof(ipAddress)} при получении IP", e);
            }
            catch (FormatException e)
            {
                throw new ServiceException($"Неверный формат данных параметра {nameof(ipAddress)}", e);
            }
            catch (Exception e)
            {
                throw new ServiceException($"Ошибка при получении IP из параметра {nameof(ipAddress)}", e);
            }
        }
    }
}