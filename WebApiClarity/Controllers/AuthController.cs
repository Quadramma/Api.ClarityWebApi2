using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApiClarity.Configuration;
using WebApiClarity.Models;

namespace WebApiClarity.Controllers {

    public class AuthController : ApiController {

        static public string DecodeFrom64(string encodedData) {
            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        static public string EncodeTo64(string toEncode) {
            byte[] toEncodeAsBytes
                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        // POST:  LOGIN
        [System.Web.Http.HttpPost]
        [System.Web.Http.AcceptVerbs("POST")]
        public object Login([FromBody] LoginRequest req) {


            

            string conn = Config.SQLConnectionFor(req.loginname, req.password);
            try {

                var db = new PetaPoco.Database(conn, Config.providerName);

                var sql = PetaPoco.Sql.Builder
               .Append("SELECT top 1 *")
               .Append("FROM Usuario U")
               .Append("WHERE U.LogIn=@0", req.loginname);
                var user = db.Query<dynamic>(sql).First();

                DateTime expirationDate = DateTime.Now.AddSeconds(Config.authSessionExpirationLengthInSeconds);
                var tokenObj = new {
                    E = expirationDate,
                    U = user.UsuarioID,
                    G = user.GrupoID,
                    P = user.PerfilID
                };

                var token = new JavaScriptSerializer().Serialize(tokenObj);
                token = EncodeTo64(token);

                
                var data = new {
                    UsuarioID = user.UsuarioID,
                    GrupoID = user.GrupoID,
                    PerfilID = user.PerfilID,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    expirationDate = expirationDate
                };

                return (new JsonResult() {
                    Data = new LoginResponse {
                        ok = true,
                        data = data,
                        token = token,
                        error = ""
                    }, JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }).Data;
            } catch (Exception e) {
                return (new JsonResult() {
                    Data = new LoginResponse() {
                        ok = false,
                        data = new { },
                        token = "",
                        error = e.Message + "[Conn=" + conn + "]"
                    }, JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }).Data;
            }
        }

    }
}
