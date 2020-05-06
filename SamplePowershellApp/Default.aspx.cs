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

namespace SamplePowershellApp
{
    public partial class Default : System.Web.UI.Page
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
            shell.Commands.AddCommand("Get-ChildItem");

            //Now we're adding the variable (so the directory) chosen by the user of the web application
            //Note that "Path" below comes from Get-ChildItem -Directory and Input.Text it's what the user typed
            shell.Commands.AddParameter("Path", Input.Text);
                        
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

                    foreach (var psObject in results)
                    {
                        // Convert the Base Object to a string and append it to the string builder.
                        // Add \r\n for line breaks
                        builder.Append(psObject.BaseObject.ToString() + "\r\n");
                    }

                    //// Encode the string in HTML (prevent security issue with 'dangerous' caracters like < >
                    Result.Text = Server.HtmlEncode(builder.ToString());
                }
            }
            catch (ActionPreferenceStopException Error) { Result.Text = Error.Message; }
            catch (RuntimeException Error) { Result.Text = Error.Message; };
        }
    }
}