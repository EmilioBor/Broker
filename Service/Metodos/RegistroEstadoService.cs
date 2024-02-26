﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Service.Interface;

namespace Service.Metodos
{
    public class RegistroEstadoService : IRegistroEstadoService
    {
        private readonly BrokerDBContext _context;
        public RegistroEstadoService(BrokerDBContext context)
        {
            _context = context;
        }
        public async Task<bool> AgregarRegistroEstado(Transaccion transaccion)
        {
            if (transaccion == null)
            {
                return false;
            }
            else
            {
                var registroEstado = new Registroestado(); // creo registro y le seteo la info de transaccion
                registroEstado.FechaHora = DateTime.Now;  // !!! Al modelo registro Estado hay que cambiarle el tipo a DateTime
                registroEstado.IdTransaccion = transaccion.Id;
                registroEstado.IdValidadoEstado = transaccion.IdValidacionEstado = 7;
                registroEstado.IdAceptadoEstado = transaccion.IdAceptadoEstado;

                // Agrego el registro al  contexto de la base de datos
                _context.Registroestado.Add(registroEstado);

                await _context.SaveChangesAsync();

                return true;
            }
        }
    }
}
