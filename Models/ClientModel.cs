using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProductsAPI.Data.Request;

namespace ProductsAPI.Models
{

    public class ClientModel 
    {
        private ClientDataAccess _clientDataAccess; 
        public ClientModel()
        {
            _clientDataAccess = new ClientDataAccess();
        }   


        #region GET


        public GetClientResponse GetById(GetClientRequest request)
        {
            GetClientResponse getClientResponse = new GetClientResponse();
            try
            {
                getClientResponse = _clientDataAccess.GetById(request.IdClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.GetById : ERROR : "+ex.Message);
                throw;
            }
            return getClientResponse;
        }
        
        public GetClientResponse GetByEmail(GetClientRequest request)
        {
            GetClientResponse getClientResponse = new GetClientResponse();
            try
            {
                getClientResponse = _clientDataAccess.GetByEmail(request.Email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.GetByEmail : ERROR : "+ex.Message);
                throw;
            }
            return getClientResponse;
        }

        public GetClientsResponse GetClients()
        {   
            GetClientsResponse getClientsResponse = new GetClientsResponse();
            try
            {
                getClientsResponse = _clientDataAccess.GetClients();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.GetClients : ERROR : "+ex.Message);
                throw;
            }
            return getClientsResponse;
        }

        public GetIdenTypesResponse GetIdenTypes()
        {   
            GetIdenTypesResponse getIdenTypesResponse = new GetIdenTypesResponse();
            try
            {
                getIdenTypesResponse = _clientDataAccess.GetIdenTypes();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProductModel.GetIdenTypes : ERROR : "+ex.Message);
                throw;
            }
            return getIdenTypesResponse;
        }


        #endregion


        #region POST


        public int Post(LoadClientRequest request)
        {
            try
            {
                _clientDataAccess.PostClient(request);
                //Retorna 204: La peticion ha sido manejada con exito y la respuesta no tiene contenido
                return 204;   
            }
            catch (Exception ex)
            {
                Console.WriteLine("ClientModel.Post : ERROR : "+ex.Message);
                //Error interno del servidor
                return 500;
            }
        }

        public int LoadNewsLetter(LoadNewsLetterRequest request)
        {
            try
            {
                _clientDataAccess.LoadNewsLetter(request);
                //Retorna 204: La peticion ha sido manejada con exito y la respuesta no tiene contenido
                return 204;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ClientModel.LoadNewsLetter : ERROR : "+ex.Message);
                //Error interno del servidor
                return 500;
            }
        }


        #endregion
    }
}
