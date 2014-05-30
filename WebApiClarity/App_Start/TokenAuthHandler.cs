using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using ClarityDB.T4;
using WebApiClarity.Configuration;
using WebApiClarity.Models;

namespace WebApiClarity.App_Start {
    public class TokenAuthHandler : DelegatingHandler {

        static public string DecodeFrom64(string encodedData) {
            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static void setCORSHeaders(HttpResponseMessage resp) {
            resp.Headers.Add("Access-Control-Allow-Origin", "*");
        }


        public static HttpResponseMessage authValidations(HttpRequestMessage request, CancellationToken cancellationToken) {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            bool authFail = false;
            setCORSHeaders(response);

            var authToken = request.Headers.GetValues("auth-token").FirstOrDefault();


            if (authToken == null || authToken.ToString() == "") {
                response.Content = new StringContent("No se detecto un token de auth");
                authFail = true;//VALIDACION TOKEN DE AUTH VACIO
            }
            AuthToken tokenObj = null;
            try {
                tokenObj = new JavaScriptSerializer().Deserialize<AuthToken>(DecodeFrom64(authToken));
            } catch (Exception) {
                response.Content = new StringContent("El token de auth es desconocido");
                authFail = true; //VALIDACION: Token desconocido
            }


            if (tokenObj.E < DateTime.Now) {
                response.Content = new StringContent("La fecha expiracion termino");
                authFail = true; //VALIDACION DE FECHA DE EXPIRACION DE TOKEN
            }


            //RECUPERA EL USUARIO DE LA BD
            var db = new PetaPoco.Database(Config.connectionStringName());
            var sql = PetaPoco.Sql.Builder
           .Append("SELECT top 1 *")
           .Append("FROM Usuario U")
           .Append("WHERE U.UsuarioID=@0", tokenObj.U);
            var user = db.Query<Usuario>(sql).First();


            if (tokenObj.G.ToString().ToLower() != user.GrupoID.ToString().ToLower()) {
                response.Content = new StringContent("Intento de cambio de grupo de impl detectado");
                authFail = true; //VALIDACION DE GRUPO
            }


            if (tokenObj.P.ToString().ToLower() != user.PerfilID.ToString().ToLower()) {
                response.Content = new StringContent("Intento de cambio de perfil detectado");
                authFail = true; //VALIDACION DE PERFIL
            }

            return authFail ? response : null;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            //BEFORE OPERATION;
            bool isPreflightRequest = request.Method == HttpMethod.Options;
            bool requestingLogin =
                 (request.RequestUri.LocalPath == "/api/auth/login");
            //    //VAL: Intentando primer auth para obtener token, se omite token auth.

            if (isPreflightRequest) {
                //CorsHandler !!
                //-----------------
                //------------------
                bool isCorsRequest = request.Headers.Contains("Origin");
                if (isCorsRequest) {
                    if (isPreflightRequest) {
                       //
                    } else {
                        return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(t => {
                            HttpResponseMessage resp = t.Result;
                            resp.Headers.Add("Access-Control-Allow-Origin", "*");
                            return resp;
                        });
                    }
                } else {
                    return base.SendAsync(request, cancellationToken);
                }
                //--------------------
                //-------------------------
            } else {
                //TokenAuth handler !!
                if (requestingLogin) {
                    
                    HttpResponseMessage authFailResponse = authValidations(request, cancellationToken);
                    if (authFailResponse != null) {
                        TaskCompletionSource<HttpResponseMessage> tcs = new TaskCompletionSource<HttpResponseMessage>();
                        tcs.SetResult(authFailResponse);
                        return tcs.Task;
                    }
                     

                } else {
                }
            }

            //Default MessageHandlers
            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(t => {
                //setCORSHeaders(t.Result);
                return t.Result;
            });
        }
    }
}