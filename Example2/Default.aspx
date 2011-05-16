<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Example2.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Status check.</h2>
        User name:<br />
        <asp:TextBox runat="server" ID="UserNameStatusCheckTextBox" />
        <asp:Button runat="server" ID="CheckUserStatusButton" Text="Check user status" OnClick="CheckUserStatusButtonClicked" />
        <p><asp:Label runat="server" ID="UserStatusMessageLabel"/></p>
        <hr />
        <h2>Mail check.</h2>
        User name:<br />
        <asp:TextBox runat="server" ID="UserNameMailCheckTextBox" />
        <asp:Button runat="server" ID="CheckMessageButton" Text="Check my messages" OnClick="CheckMessagesButtonClicked" />
        <p>
            <asp:DataGrid runat="server" ID="Mails" AutoGenerateColumns="true" />
            <asp:Label runat="server" ID="MailCount" />
        </p>
    </div>
    </form>
</body>
</html>
