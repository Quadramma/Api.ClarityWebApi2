using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using ClarityDB.T4;

namespace WebApiClarity.Controllers {

    public class ContactoController : ApiController {


        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object GET(int pageNumber, int itemsPerPage
               , int EstadoContactoID
              , int EmpresaID
              , string ComienzoDesde
              , string ComienzoHasta
            //   , string Codigo
            //   , string RazonSocial
            ) {
                JsonResult rta;
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append("SELECT ")
                    .Append("C.*")
                    //.Append("C.Asunto, C.FechaAlta, C.TipoContactoID")
                    .Append(",Emp.RazonSocial,T.Descripcion as TemaDescripcion, Emp.Codigo as EmpresaCodigo")
                    .Append(",GU.Descripcion as GrupoUsuarioDescripcion, GU.GrupoID, G.Descripcion as GrupoDescripcion")
                    .Append(",EC.Descripcion as EstadoContactoDescripcion,SEC.Descripcion as SubEstadoContactoDescripcion")
                    //.Append(",C.EmpresaID")
                    .Append("FROM Contacto C")
                    .Append("LEFT JOIN Tema T on T.TemaID = C.TemaID")
                    .Append("LEFT JOIN EstadoContacto EC on EC.EstadoContactoID = C.EstadoContactoID")
                    //JLA>Pregunta>Para enganchar el subestado hay que tener en cuenta el GrupoID?, y el TipoCuentaID? de donde lo saco?
                    .Append("LEFT JOIN SubEstadoContacto SEC on SEC.EstadoContactoID = C.EstadoContactoID")
                    .Append("LEFT JOIN GrupoUsuario GU on GU.GrupoUsuarioID = C.GrupoUsuarioID")
                    .Append("LEFT JOIN Grupo G on G.GrupoID = GU.GrupoID")
                    .Append("LEFT JOIN Empresa Emp on Emp.EmpresaID = C.EmpresaID");

                sql.Append("WHERE 1 = 1");
                if (EmpresaID != -1) {
                    sql.Append(" AND C.EmpresaID = @0", EmpresaID);
                }
                if (EstadoContactoID != -1) {
                    sql.Append(" AND C.EstadoContactoID = @0", EstadoContactoID);
                }
                if (ComienzoDesde != null && ComienzoDesde != "") {
                    sql.Append(" AND C.Comienzo >= @0", ComienzoDesde);
                }
                if (ComienzoHasta != null && ComienzoHasta != "") {
                    sql.Append(" AND C.Comienzo <= @0", ComienzoHasta);
                }
                /*
                if (Codigo != null) {
                    sql.Append(" AND Emp.Codigo = @0", Codigo);
                }

                if (RazonSocial != null) {
                    sql.Append(" AND Emp.RazonSocial = @0", RazonSocial);
                }
                */
                var items = db.Page<dynamic>(pageNumber, itemsPerPage, sql);
                rta = new JsonResult() { Data = new { data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            } catch (Exception e) {
                rta = new JsonResult() { Data = new { data = "", error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return rta.Data;
        }

        // GET: 
        [System.Web.Http.HttpGet]
        [System.Web.Http.AcceptVerbs("GET")]
        public object GET(int id) {
            var db = new PetaPoco.Database("jlapc");
            var sql = PetaPoco.Sql.Builder
                .Append("SELECT C.*")
                .Append("FROM Contacto C")
                .Append("WHERE C.ContactoID=@0", id);
            var items = db.Query<Contacto>(sql);
            return (new JsonResult() { Data = new { data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
        }


        // POST:  CREATE Y UPDATE
        [System.Web.Http.HttpPost]
        [System.Web.Http.AcceptVerbs("POST")]
        public object POST([FromBody] Contacto item) {
            try {
                var db = new PetaPoco.Database("jlapc");
                if (item.ContactoID == 0) {
                    db.Insert(item);
                } else {
                    db.Update(item);
                }
                return (new JsonResult() { Data = new { ok = true, error = "" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }

        /*
        // PUT:  UPDATE
        [System.Web.Http.HttpPut]
        [System.Web.Http.AcceptVerbs("PUT")]
        public object PUT([FromBody] Contacto item) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append("UPDATE Contacto")
                    .Append(" SET EmpresaID= @0", item.EmpresaID)
                    .Append(" SET Asunto= @0", item.Asunto)
                    .Append(" , Comienzo= @0", item.Comienzo)
                    .Append(" , Finalizacion= @0", item.Finalizacion)
                    .Append(" , TipoContactoID= @0", item.TipoContactoID)
                    .Append(" , EstadoContactoID= @0", item.EstadoContactoID)
                    .Append(" , AvisoID= @0", item.AvisoID)
                    .Append(" , FechaAlta= @0", item.FechaAlta)
                    .Append(" , UsuarioAlta= @0", item.UsuarioAlta)
                    .Append(" , FechaModi= @0", item.FechaModi)
                    .Append(" , UsuarioModi= @0", item.UsuarioModi)
                    .Append(" , AltaAutomatica= @0", item.AltaAutomatica)
                    .Append(" , TipoModuloOrigenID= @0", item.TipoModuloOrigenID)
                    .Append(" , ModuloOrigenID= @0", item.ModuloOrigenID)
                    .Append(" , TriggerCRMID= @0", item.TriggerCRMID)
                    .Append(" , GrupoUsuarioID= @0", item.GrupoUsuarioID)
                    .Append(" , ContactoID_Padre= @0", item.ContactoID_Padre)
                    .Append(" , SucursalID= @0", item.SucursalID)
                    .Append(" , EmpresaID_Vendedor= @0", item.EmpresaID_Vendedor)
                    .Append(" , SubEstadoContactoID= @0", item.SubEstadoContactoID)
                    .Append(" , ProximaAccion= @0", item.ProximaAccion)
                    .Append(" , NovedadPrioridadID= @0", item.NovedadPrioridadID)
                    .Append("WHERE ContactoID = @0", item.ContactoID);
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }

        // POST:  CREATE
        [System.Web.Http.HttpPost]
        [System.Web.Http.AcceptVerbs("POST")]
        public object POST([FromBody] Contacto item) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append(" INSERT INTO Contacto (EmpresaID,Asunto,Comienzo,Finalizacion,TipoContactoID,EstadoContactoID,AvisoID")
                    .Append(",FechaAlta,UsuarioAlta,FechaModi,UsuarioModi,AltaAutomatica,TipoModuloOrigenID,ModuloOrigenID")
                    .Append(",TriggerCRMID,GrupoUsuarioID,ContactoID_Padre,SucursalID,EmpresaID_Vendedor,SubEstadoContactoID")
                    .Append(",ProximaAccion,NovedadPrioridadID)")
                    .Append(" VALUES(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22)"
                    , item.EmpresaID
                    , item.Asunto
                    , item.Comienzo
                    , item.Finalizacion
                    , item.TipoContactoID
                    , item.EstadoContactoID
                    , item.AvisoID
                    , item.FechaAlta
                    , item.UsuarioAlta
                    , item.FechaModi
                    , item.UsuarioModi
                    , item.AltaAutomatica
                    , item.TipoModuloOrigenID
                    , item.ModuloOrigenID
                    , item.TriggerCRMID
                    , item.GrupoUsuarioID
                    , item.ContactoID_Padre
                    , item.SucursalID
                    , item.EmpresaID_Vendedor
                    , item.SubEstadoContactoID
                    , item.ProximaAccion
                    , item.NovedadPrioridadID
                    );
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, error = "", sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }
        */
        // DELETE:  DELETE
        [System.Web.Http.HttpDelete]
        [System.Web.Http.AcceptVerbs("DELETE")]
        public object DELETE(int id) {
            try {
                var db = new PetaPoco.Database("jlapc");
                var sql = PetaPoco.Sql.Builder
                    .Append("DELETE FROM Contacto")
                    .Append("WHERE ContactoID= @0", id);
                db.Execute(sql);
                return (new JsonResult() { Data = new { ok = true, error = "", sql = sql }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;

            } catch (Exception e) {
                return (new JsonResult() { Data = new { ok = false, error = e.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }).Data;
            }
        }
    }
}
