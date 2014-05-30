using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ClarityWebApi.App_Start {
    public class CorsHandler : DelegatingHandler {

        const string Origin = "Origin";
        const string methods = "POST, GET, OPTIONS, DELETE, PUT";
        const string headers = "Content-Type, X-Requested-With, auth-token, Authorization";
        const string AccessControlRequestMethod = "Access-Control-Request-Method";
        const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

        /*
        public static Task<HttpResponseMessage> CorsHandler(HttpRequestMessage request, CancellationToken cancellationToken){
        
        }
         * */

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            bool isCorsRequest = request.Headers.Contains(Origin);
            bool isPreflightRequest = request.Method == HttpMethod.Options;
            if (isCorsRequest) {
                if (isPreflightRequest) {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    //string requestOrigin = request.Headers.GetValues(Origin).First();
                    //response.Headers.Add(AccessControlAllowOrigin, requestOrigin);
                    response.Headers.Add(AccessControlAllowOrigin, "*");
                    string accessControlRequestMethod = methods;
                    response.Headers.Add(AccessControlAllowMethods, accessControlRequestMethod);
                    string requestedHeaders = headers;
                    if (!string.IsNullOrEmpty(requestedHeaders)) {
                        response.Headers.Add(AccessControlAllowHeaders, requestedHeaders);
                    }
                    TaskCompletionSource<HttpResponseMessage> tcs = new TaskCompletionSource<HttpResponseMessage>();
                    tcs.SetResult(response);
                    return tcs.Task;
                } else {
                    return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(t => {
                        HttpResponseMessage resp = t.Result;
                        resp.Headers.Add(AccessControlAllowOrigin, "*");
                        return resp;
                    });
                }
            } else {
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}

