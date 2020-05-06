<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SamplePowershellApp.Reports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GetReports</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div><h1 align="center">Get-PowerBIReport</h1></div>
            <p>Click on Execute to get the Reports:
                <asp:TextBox ID="Input" runat="server" Width="100%" Height="20px" ></asp:TextBox>
            </p>
            <asp:Button ID="ExecuteInput" runat="server" Text="Execute" Width="200" onclick="ExecuteInputClick" />
 
            <p>Reports
            <asp:TextBox ID="ReportResult" TextMode="MultiLine" Width="100%" Height="100px" runat="server"></asp:TextBox>
            </p>

            <p>DataSets
            <asp:TextBox ID="DatasetResult" TextMode="MultiLine" Width="100%" Height="100px" runat="server"></asp:TextBox>
            </p>

            
        </div>
    </form>
</body>
</html>
