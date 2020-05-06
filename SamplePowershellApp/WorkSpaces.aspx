<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkSpaces.aspx.cs" Inherits="SamplePowershellApp.WorkSpaces" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GetWorkSpaces</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <div><h1 align="center">Get-PowerBIWorkspace</h1></div>
            <p>Click on Execute to get the Work Spaces:
                <asp:TextBox ID="Input" runat="server" Width="100%" Height="20px" ></asp:TextBox>
            </p>
            <asp:Button ID="ExecuteInput" runat="server" Text="Execute" Width="200" onclick="ExecuteInputClick" />
 
            <p>Work Spaces
            <asp:TextBox ID="Result" TextMode="MultiLine" Width="100%" Height="100px" runat="server"></asp:TextBox>
            </p>

            <p>Add user to the Work Space:
                <p>Enter Email Address: <asp:TextBox ID="TxtEmailAddress" runat="server" Width="300" Height="20px" ></asp:TextBox>
                    Enter Access Right: <asp:TextBox ID="TxtAccessRight" runat="server"  Height="20px" ></asp:TextBox>
                    <asp:Button ID="AddUser" runat="server" Text="Add User"  onclick="AddUserClick" />
                    <asp:Button ID="RemoveUser" runat="server" Text="Remove User"  onclick="RemoveUserClick" />
                </p>
                <asp:TextBox ID="AddResult" TextMode="MultiLine" Width="100%" Height="100px" runat="server"></asp:TextBox>
            </p>
        </div>
    </form>
</body>
</html>
