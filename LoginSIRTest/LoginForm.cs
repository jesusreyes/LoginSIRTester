using LoginSIRTest.Models;
using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace LoginSIRTest
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            rtxtResponse.Text = "";
            string url = "https://sir.sonora.gob.mx/api/access";

            LoginCheck oLogin = GetRequestObject();
            
            LoginCheckResponse response = Send(url, oLogin, "POST");

            ShowResponse(response);
        }

        private void ShowResponse(LoginCheckResponse response)
        {
            rtxtResponse.Text = $"result: {response.result}. " + "\n" +
                $"rol: { response.rol}." + "\n" +
                $"message: {response.message}.";
        }

        private LoginCheck GetRequestObject()
        {
            LoginCheck oLogin = new LoginCheck();

            if (txtUser.Text.Trim().Length > 0 && txtPassword.Text.Trim().Length > 0)
            {
                oLogin.user = txtUser.Text.Trim();
                oLogin.password = txtPassword.Text.Trim();
            }
            else
            { 
                oLogin.user = "username";
                oLogin.password = "password";
            }
            oLogin.token = "1d13a80ea9204cs89f9ae3600f1e614r";

            return oLogin;
        }

        private LoginCheckResponse Send<LoginCheck>(string url, LoginCheck requestObject, string method = "POST")
        {
            string result = "";
            LoginCheckResponse oReply = new LoginCheckResponse();

            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();

                string json = JsonConvert.SerializeObject(requestObject);

                WebRequest request = WebRequest.Create(url);
                request.Method = method;
                request.ContentType = "application/json;charset=utf-8'";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                oReply = JsonConvert.DeserializeObject<LoginCheckResponse>(result);

            }
            catch (Exception e)
            {

                oReply.result = false;
                oReply.rol = "";
                oReply.message = e.Message;

            }

            return oReply;
        }
    }
}

