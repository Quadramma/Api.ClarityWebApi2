



















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `ClarityDBSimpleConn`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=localhost;Initial Catalog=ClarityDB;Integrated Security=False;User Id=desa;password=**zapped**;MultipleActiveResultSets=True`
//     Schema:                 ``
//     Include Views:          `False`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace ClarityDB.T4
{

	public partial class ClarityDBSimpleConnDB : Database
	{
		public ClarityDBSimpleConnDB() 
			: base("ClarityDBSimpleConn")
		{
			CommonConstruct();
		}

		public ClarityDBSimpleConnDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			ClarityDBSimpleConnDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static ClarityDBSimpleConnDB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new ClarityDBSimpleConnDB();
        }

		[ThreadStatic] static ClarityDBSimpleConnDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static ClarityDBSimpleConnDB repo { get { return ClarityDBSimpleConnDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    
	[TableName("PerfilArbol")]


	[ExplicitColumns]
    public partial class PerfilArbol : ClarityDBSimpleConnDB.Record<PerfilArbol>  
    {



		[Column] public int PerfilArbolID { get; set; }





		[Column] public int PerfilID { get; set; }





		[Column] public int ModuloID { get; set; }





		[Column] public int? PadreID { get; set; }





		[Column] public string Descripcion { get; set; }





		[Column] public string ToolTipText { get; set; }





		[Column] public string ParametrosLink { get; set; }





		[Column] public int? WindowsCommandID { get; set; }





		[Column] public int? Orden { get; set; }





		[Column] public int? ExternWebsiteID { get; set; }





		[Column] public byte TipoExcepcion { get; set; }



	}

    
	[TableName("SYS_Modulo")]


	[ExplicitColumns]
    public partial class SYS_Modulo : ClarityDBSimpleConnDB.Record<SYS_Modulo>  
    {



		[Column] public int ModuloID { get; set; }





		[Column] public string Descripcion { get; set; }





		[Column] public string ToolTipText { get; set; }





		[Column] public string Link { get; set; }





		[Column] public string ArchivoAyuda { get; set; }





		[Column] public string ExplicacionParametrosPerfilArbol { get; set; }





		[Column] public int TipoModuloID { get; set; }



	}

    
	[TableName("UsuarioBkp")]


	[PrimaryKey("UsuarioID", autoIncrement=false)]

	[ExplicitColumns]
    public partial class UsuarioBkp : ClarityDBSimpleConnDB.Record<UsuarioBkp>  
    {



		[Column] public int UsuarioID { get; set; }





		[Column] public int PerfilID { get; set; }





		[Column] public string LogIn { get; set; }





		[Column] public string Nombre { get; set; }





		[Column] public string Apellido { get; set; }





		[Column] public int GrupoID { get; set; }





		[Column] public int? CuentaID_Caja { get; set; }





		[Column] public int? GrupoUsuarioID_Default { get; set; }





		[Column] public int Habilitado { get; set; }





		[Column] public string Codigo { get; set; }





		[Column] public int? IntentosFallidosLogin { get; set; }





		[Column] public DateTime? UltimoCambioPassword { get; set; }





		[Column] public byte? SoloAdmiteAccesoDesdeSusSucursales { get; set; }





		[Column] public byte? VerSoloRegistrosDeLaSucursalActual { get; set; }





		[Column] public int I18NLanguageID { get; set; }





		[Column] public int I18NCountryID { get; set; }





		[Column] public bool ClaveCaducada { get; set; }



	}

    
	[TableName("Empresa")]


	[PrimaryKey("DemoEmpresaID")]



	[ExplicitColumns]
    public partial class Empresa : ClarityDBSimpleConnDB.Record<Empresa>  
    {



		[Column] public int DemoEmpresaID { get; set; }





		[Column] public string RazonSocial { get; set; }





		[Column] public int DemoGrupoID { get; set; }



	}

    
	[TableName("Usuario")]


	[PrimaryKey("DemoUsuarioID")]



	[ExplicitColumns]
    public partial class Usuario : ClarityDBSimpleConnDB.Record<Usuario>  
    {



		[Column] public int DemoUsuarioID { get; set; }





		[Column] public string LoginName { get; set; }





		[Column] public int DemoGrupoID { get; set; }





		[Column] public int DemoPerfilID { get; set; }



	}

    
	[TableName("Grupo")]


	[PrimaryKey("GrupoID", autoIncrement=false)]

	[ExplicitColumns]
    public partial class Grupo : ClarityDBSimpleConnDB.Record<Grupo>  
    {



		[Column] public int GrupoID { get; set; }





		[Column] public string Descripcion { get; set; }



	}

    
	[TableName("Perfil")]


	[PrimaryKey("DemoPerfilID", autoIncrement=false)]

	[ExplicitColumns]
    public partial class Perfil : ClarityDBSimpleConnDB.Record<Perfil>  
    {



		[Column] public int DemoPerfilID { get; set; }





		[Column] public string Descripcion { get; set; }





		[Column] public int DemoGrupoID { get; set; }



	}

    
	[TableName("Tema")]


	[PrimaryKey("TemaID")]



	[ExplicitColumns]
    public partial class Tema : ClarityDBSimpleConnDB.Record<Tema>  
    {



		[Column] public int TemaID { get; set; }





		[Column] public string Descripcion { get; set; }





		[Column] public byte EsFijo { get; set; }





		[Column] public int GrupoID { get; set; }



	}


}



