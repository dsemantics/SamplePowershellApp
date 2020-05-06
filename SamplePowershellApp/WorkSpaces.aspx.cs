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
    public partial class WorkSpaces : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ExecuteInputClick(object sender, EventArgs e)
        {
            // Clean the TextBox from any previous output
            Result.Text = string.Empty;

            // Create the InitialSessionState Object
            InitialSessionState iss = InitialSessionState.CreateDefault2();

            // Initialize PowerShell Engine
            var shell = PowerShell.Create(iss);

            // Add the command to the Powershell Object, then add the parameter from the text box with ID Input
            //The first one is the command we want to run with the input, so Get-ChildItem is all we need
            //shell.Commands.AddCommand("Get-ChildItem");

            //Now we're adding the variable (so the directory) chosen by the user of the web application
            //Note that "Path" below comes from Get-ChildItem -Directory and Input.Text it's what the user typed
            //shell.Commands.AddParameter("Path", Input.Text);
            shell.Commands.AddCommand("Connect-PowerBIServiceAccount");
            
            var userName = "biassistant@datasemantics.in";
            var pwd = "Dac21568";
            System.Security.SecureString theSecureString = new NetworkCredential(userName, pwd).SecurePassword;
            PSCredential cred = new PSCredential(userName, theSecureString);

            shell.Commands.AddParameter("Credential", cred);
            //shell.Commands.AddParameter("ServicePrincipal");

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

                    //PSObject pSObject = results[0];
                    //var objBase= pSObject.BaseObject;

                    var shell1 = PowerShell.Create(iss);
                    shell1.Commands.AddCommand("Get-PowerBIWorkspace");
                    //shell1.Commands.AddParameter("Scope", "Organization");
                    //shell1.Commands.AddParameter("Id", "ea5ec2c2-def9-4b74-9133-305511d96fdf");
                    var res = shell1.Invoke();
                    if (res.Count > 0)
                    {
                        foreach (var psObject in res)
                        {
                            // Convert the Base Object to a string and append it to the string builder.
                            // Add \r\n for line breaks
                            var workSpaceName = psObject.Properties["Name"].Value;
                            
                            //var workSpaceUser = psObject.Properties["User"].Value;

                            builder.Append(psObject.Properties["Name"].Value + "\r\n");
                            //builder.Append(psObject.BaseObject.ToString());
                        }
                        Result.Text = Server.HtmlEncode(builder.ToString());
                    }

                    //var shell2 = PowerShell.Create(iss);
                    //shell2.Commands.AddCommand("Get-UnifiedGroup");
                    ////shell2.Commands.AddParameter("Id", "ea5ec2c2-def9-4b74-9133-305511d96fdf");
                    ////shell2.Commands.AddParameter("UserEmailAddress", "venkata.murakunda@datasemantics.in");
                    ////shell2.Commands.AddParameter("AccessRight", "Member");
                    //var resaddUser = shell2.Invoke();
                    //for(int i = 0; i < results.Count; i++)
                    //{
                    //    PSObject pSObject = results[i];
                    //}


                    //foreach (var psObject in results)
                    //{
                    //    // Convert the Base Object to a string and append it to the string builder.
                    //    // Add \r\n for line breaks
                    //    builder.Append(psObject.BaseObject.ToString() + "\r\n");
                    //    //builder.Append(psObject.BaseObject.ToString());
                    //}

                    //// Encode the string in HTML (prevent security issue with 'dangerous' caracters like < >
                    //Result.Text = Server.HtmlEncode(builder.ToString());
                }
            }
            catch (ActionPreferenceStopException Error) { Result.Text = Error.Message; }
            catch (RuntimeException Error) { Result.Text = Error.Message; };
        }

        protected void AddUserClick(object sender, EventArgs e)
        {
            // Clean the TextBox from any previous output
            AddResult.Text = string.Empty;

            // Create the InitialSessionState Object
            InitialSessionState iss = InitialSessionState.CreateDefault2();

            // Initialize PowerShell Engine
            var shell2 = PowerShell.Create(iss);
            shell2.Commands.AddCommand("Add-PowerBIWorkspaceUser");
            shell2.Commands.AddParameter("Id", "ea5ec2c2-def9-4b74-9133-305511d96fdf");
            shell2.Commands.AddParameter("UserEmailAddress", "venkata.murakunda@datasemantics.in");
            shell2.Commands.AddParameter("AccessRight", "Member");
            var resaddUser = shell2.Invoke();
            AddResult.Text = "User added to workspace successfully";
        }

        protected void RemoveUserClick(object sender, EventArgs e)
        {
            // Clean the TextBox from any previous output
            AddResult.Text = string.Empty;

            // Create the InitialSessionState Object
            InitialSessionState iss = InitialSessionState.CreateDefault2();

            // Initialize PowerShell Engine
            var shell2 = PowerShell.Create(iss);
            shell2.Commands.AddCommand("Remove-PowerBIWorkspaceUser");
            shell2.Commands.AddParameter("Id", "ea5ec2c2-def9-4b74-9133-305511d96fdf");
            shell2.Commands.AddParameter("UserEmailAddress", "venkata.murakunda@datasemantics.in");
            var resaddUser = shell2.Invoke();
            AddResult.Text = "User Removed from workspace successfully";
        }
    }
}