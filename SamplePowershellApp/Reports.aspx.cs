using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Diagnostics;
using System.Net;

namespace SamplePowershellApp
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ExecuteInputClick(object sender, EventArgs e)
        {
            // Clean the TextBox from any previous output
            ReportResult.Text = string.Empty;
            DatasetResult.Text = string.Empty;

            // Create the InitialSessionState Object
            InitialSessionState iss = InitialSessionState.CreateDefault2();

            // Initialize PowerShell Engine
            var shell = PowerShell.Create(iss);

            shell.Commands.AddCommand("Connect-PowerBIServiceAccount");
            //shell.Commands.AddCommand("Get-PowerBIWorkspace");
            var userName = "biassistant@datasemantics.in";
            var pwd = "Dac21568";
            System.Security.SecureString theSecureString = new NetworkCredential(userName, pwd).SecurePassword;
            PSCredential cred = new PSCredential(userName, theSecureString);

            shell.Commands.AddParameter("Credential",cred);


            // Execute the script 
            try
            {
                var results = shell.Invoke();

                // display results, with BaseObject converted to string
                // Note : use |out-string for console-like output
                if (results.Count > 0)
                {

                    // We use a string builder to create our result text
                    var builder = new StringBuilder();

                    
                    var shell1 = PowerShell.Create(iss);
                    shell1.Commands.AddCommand("Get-PowerBIReport");
                    shell1.Commands.AddParameter("WorkspaceId","ea5ec2c2-def9-4b74-9133-305511d96fdf");
                    var res = shell1.Invoke();
                    if (res.Count > 0)
                    {
                        foreach (var psObject in res)
                        {
                            // Convert the Base Object to a string and append it to the string builder.
                            // Add \r\n for line breaks
                            var reportName = psObject.Properties["Name"].Value;


                            builder.Append(psObject.Properties["Name"].Value + "\r\n");
                            //builder.Append(psObject.BaseObject.ToString());
                        }
                        ReportResult.Text = Server.HtmlEncode(builder.ToString());
                    }

                    var builder1 = new StringBuilder();
                    var shell2 = PowerShell.Create(iss);
                    shell2.Commands.AddCommand("Get-PowerBIDataset");
                    shell2.Commands.AddParameter("WorkspaceId", "ea5ec2c2-def9-4b74-9133-305511d96fdf");
                    var resDataset = shell2.Invoke();
                    if (resDataset.Count > 0)
                    {
                        foreach (var psObject in resDataset)
                        {
                            // Convert the Base Object to a string and append it to the string builder.
                            // Add \r\n for line breaks
                            var reportName = psObject.Properties["Name"].Value;


                            builder1.Append(psObject.Properties["Name"].Value + "\r\n");
                            
                        }
                        DatasetResult.Text = Server.HtmlEncode(builder1.ToString());
                    }

                    var builder2 = new StringBuilder();
                    var shell3 = PowerShell.Create(iss);
                    shell3.Commands.AddCommand("Invoke-PowerBIRestMethod");
                    shell3.Commands.AddParameter("Url", "groups");
                    shell3.Commands.AddParameter("Method", "Get");
                    var resestMethod = shell3.Invoke();
                    if (resestMethod.Count > 0)
                    {
                        foreach (var psObject in resestMethod)
                        {
                            // Convert the Base Object to a string and append it to the string builder.
                            // Add \r\n for line breaks
                            var reportName = psObject.Properties["Name"].Value;


                            builder2.Append(psObject.Properties["Name"].Value + "\r\n");

                        }
                        DatasetResult.Text = Server.HtmlEncode(builder2.ToString());
                    }


                }
            }
            catch (ActionPreferenceStopException Error) { DatasetResult.Text = Error.Message; }
            catch (RuntimeException Error) { DatasetResult.Text = Error.Message; };
        }
    }
}